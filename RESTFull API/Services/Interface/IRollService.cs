using RESTFull_API.DTO;
using RESTFull_API.DTOs;
using RESTFull_API.Models;

namespace RESTFull_API.Services.Interface
{
    public interface IRollService
    {
        Task<RollDto> CreateAsync(CreateRollDto dto, CancellationToken ct);
        Task<RollDto> RemoveAsync(Guid id, CancellationToken ct);
        Task<List<RollDto>> GetAsync(RollQuery query, CancellationToken ct);
        Task<RollStatsDto> GetStatsAsync(RollStatsQuery query, CancellationToken ct);
    }
}
