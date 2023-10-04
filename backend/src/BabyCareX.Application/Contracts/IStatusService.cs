using BabyCareX.Application.Models.StatusModels;
using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IStatusService
    {
        Task<StatusResponse> AddStatusAsync(Status statusToSave);
        Task<StatusResponse> UpdateStatusAsync(Status statusToUpdate, int id);
        Task<bool> DeleteStatusByIdAsync(int id);
        Task<bool> DeleteAllStatusAsync();
        Task<StatusResponse> GetStatusByIdAsync(int id);
        Task<IEnumerable<StatusResponse>> GetAllStatusAsync();
    }
}