using RESTFull_API.DTOs;
using RESTFull_API.Models;

namespace RESTFull_API.Repositories.Interface
{
    public interface IRollRepository
    {
        Task<Roll> AddAsync(Roll roll, CancellationToken ct);
        Task<Roll?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Roll> UpdateAsync(Roll roll, CancellationToken ct);

        Task<List<Roll>> GetAllAsync(CancellationToken ct);
        Task<List<Roll>> GetListAsync(RollQuery query, CancellationToken ct);
    }
}
