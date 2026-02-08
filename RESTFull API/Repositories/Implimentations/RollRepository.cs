using Microsoft.EntityFrameworkCore;
using RESTFull_API.Data;
using RESTFull_API.DTOs;
using RESTFull_API.Models;
using RESTFull_API.Repositories.Interface;

namespace RESTFull_API.Repositories.Implimentations
{
    public sealed class RollRepository : IRollRepository
    {
        private readonly AppDbContext _db;

        public RollRepository(AppDbContext db) => _db = db;

        public async Task<Roll> AddAsync(Roll roll, CancellationToken ct)
        {
            _db.Rolls.Add(roll);
            await _db.SaveChangesAsync();
            return roll;
        }

        public Task<Roll?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return _db.Rolls.FirstOrDefaultAsync(x => x.Id == id, ct);
        }
            
        public async Task<Roll> UpdateAsync(Roll roll, CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
            return roll;
        }

        public async Task<List<Roll>> GetListAsync(RollQuery q, CancellationToken ct)
        {
            IQueryable<Roll> query = _db.Rolls.AsNoTracking();

            if (q.Id is not null)
            {
                query = query.Where(x => x.Id == q.Id.Value);
            }

            if (q.OnlyInStock == true)
            {
                query = query.Where(x => x.RemovedAt == null);
            }
            else if (q.OnlyInStock == false)
            {
                query = query.Where(x => x.RemovedAt != null);
            }

            if (q.LengthFrom is not null)
            {
                query = query.Where(x => x.Length >= q.LengthFrom.Value);
            }

            if (q.LengthTo is not null)
            {
                query = query.Where(x => x.Length <= q.LengthTo.Value);
            }

            if (q.WeightFrom is not null)
            {
                query = query.Where(x => x.Weight >= q.WeightFrom.Value);
            }

            if (q.WeightTo is not null)
            {
                query = query.Where(x => x.Weight <= q.WeightTo.Value);
            }

            if (q.AddedFrom is not null)
            {
                query = query.Where(x => x.AddedAt >= q.AddedFrom.Value);
            }

            if (q.AddedTo is not null)
            {
                query = query.Where(x => x.AddedAt <= q.AddedTo.Value);
            }

            if (q.RemovedFrom is not null)
            {
                query = query.Where(x => x.RemovedAt != null && x.RemovedAt >= q.RemovedFrom.Value);
            }

            if (q.RemovedTo is not null)
            {
                query = query.Where(x => x.RemovedAt != null && x.RemovedAt <= q.RemovedTo.Value);
            }

            return await query
                .OrderByDescending(x => x.AddedAt)
                .ToListAsync(ct);
        }

        public Task<List<Roll>> GetAllAsync(CancellationToken ct)
        {
            return _db.Rolls.AsNoTracking().ToListAsync(ct);
        }
    }
}
