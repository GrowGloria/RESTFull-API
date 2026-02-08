using Microsoft.AspNetCore.Mvc.Formatters;
using RESTFull_API.DTO;
using RESTFull_API.DTOs;
using RESTFull_API.Models;
using RESTFull_API.Repositories.Interface;
using RESTFull_API.Services.Interface;

namespace RESTFull_API.Services
{
    public class RollService : IRollService
    {
        private readonly IRollRepository _repo;

        public RollService(IRollRepository repo)
        {
            _repo = repo;
        }

        public async Task<RollDto> CreateAsync(CreateRollDto dto, CancellationToken ct)
        {
            var roll = new Roll
            {
                Length = dto.Lenght,
                Weight = dto.Weight,
                AddedAt = DateTimeOffset.UtcNow,
                RemovedAt = null
            };

            var created = await _repo.AddAsync(roll, ct);

            return ToDto(created);
        }

        public async Task<RollDto> RemoveAsync(Guid id, CancellationToken ct)
        {
            var roll = await _repo.GetByIdAsync(id, ct);

            if (roll is null)
            {
                throw new KeyNotFoundException("Рулон не найден.");
            }

            if (roll.RemovedAt is not null)
            {
                throw new InvalidOperationException("Рулон уже удален.");
            }

            roll.RemovedAt = DateTimeOffset.UtcNow;

            var updated = await _repo.UpdateAsync(roll, ct);

            return ToDto(updated);
        }

        public async Task<List<RollDto>> GetAsync(RollQuery query, CancellationToken ct)
        {
            var list = await _repo.GetListAsync(query, ct);

            return list.Select(ToDto).ToList();
        }

        public async Task<RollStatsDto> GetStatsAsync(RollStatsQuery query, CancellationToken ct)
        {
            var from = query.From;
            var to = query.To;

            if (from > to)
            {
                throw new ArgumentException("Не корректный период.");
            }

            var rolls = await _repo.GetAllAsync(ct);

            var addedCount = rolls.Count(x => x.AddedAt >= from && x.AddedAt <= to);

            var removedCount = rolls.Count(x => 
                x.RemovedAt != null &&
                x.RemovedAt >= from && 
                x.RemovedAt <= to);

            var activeInPeriod = rolls.Where(x => 
                x.AddedAt <= to &&
                (x.RemovedAt == null || x.RemovedAt >= from))
                .ToList();

            decimal? avgLength = activeInPeriod.Any() ? activeInPeriod.Average(x => x.Length) : null;
            decimal? avgWeight = activeInPeriod.Any() ? activeInPeriod.Average(x => x.Weight) : null;

            decimal? minLength = activeInPeriod.Any() ? activeInPeriod.Min(x => x.Length) : null;
            decimal? maxLength = activeInPeriod.Any() ? activeInPeriod.Max(x => x.Length) : null;

            decimal? minWeight = activeInPeriod.Any() ? activeInPeriod.Min(x => x.Weight) : null;
            decimal? maxWeight = activeInPeriod.Any() ? activeInPeriod.Max(x => x.Weight) : null;

            var totalWeight = activeInPeriod.Sum(x => x.Weight);

            var lifetimes = rolls
                .Where(x => x.RemovedAt != null)
                .Select(x => x.RemovedAt.Value - x.AddedAt)
                .ToList();

            TimeSpan? minLifetime = lifetimes.Any() ? lifetimes.Min() : null;
            TimeSpan? maxLifetime = lifetimes.Any() ? lifetimes.Max() : null;

            return new RollStatsDto
            {
                AddedCount = addedCount,
                RemovedCount = removedCount,
                AverageLength = avgLength,
                AverageWeight = avgWeight,
                MinLength = minLength,
                MaxLength = maxLength,
                MinWeight = minWeight,
                MaxWeight = maxWeight,
                TotalWeight = totalWeight,
                MinLifetime = minLifetime,
                MaxLifetime = maxLifetime
            };
        }

        private static RollDto ToDto(Roll x) => new()
        {
            Id = x.Id,
            Length = x.Length,
            Weight = x.Weight,
            AddedAt = x.AddedAt,
            RemovedAt = x.RemovedAt
        };
    }
}
