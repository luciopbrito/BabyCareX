using BabyCareX.Application.Contracts;
using BabyCareX.Application.Models.StatusModels;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IBaseRepository _baseRepository;

        public StatusService(IStatusRepository statusRepository, IBaseRepository baseRepository)
        {
            _statusRepository = statusRepository;
            _baseRepository = baseRepository;
        }

        public async Task<StatusResponse> AddStatusAsync(Status statusToSave)
        {
            try
            {
                _baseRepository.Add(statusToSave);

                if (await _baseRepository.SaveChangesAsync())
                {
                    var status = await _statusRepository.GetStatusByIdAsync(statusToSave.Id);
                    return new StatusResponse()
                    {
                        Description = status.Description,
                        CreatedAt = status.CreatedAt,
                        UpdatedAt = status.UpdatedAt,
                        Id = status.Id,
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllStatusAsync()
        {
            var status = await _statusRepository.GetAllStatusAsync();
            if (!status.Any()) throw new Exception("There aren't any Status on the database.");

            _baseRepository.DeleteRange(status.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteStatusByIdAsync(int id)
        {
            try
            {
                var statusFromDB = await _statusRepository.GetStatusByIdAsync(id)
                ??
                throw new Exception("Status.Id does not persist on the database.");

                _baseRepository.Delete(statusFromDB);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<StatusResponse>> GetAllStatusAsync()
        {
            try
            {
                var statusFromDB = await _statusRepository.GetAllStatusAsync();
                if (statusFromDB == null) return null;

                List<StatusResponse> status = new();

                foreach (var item in statusFromDB)
                {
                    status.Add(new StatusResponse()
                    {
                        Description = item.Description,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        Id = item.Id,
                    });
                }

                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StatusResponse> GetStatusByIdAsync(int id)
        {
            try
            {
                var statusFromDB = await _statusRepository.GetStatusByIdAsync(id);
                if (statusFromDB == null) return null;

                return new StatusResponse()
                {
                    Description = statusFromDB.Description,
                    CreatedAt = statusFromDB.CreatedAt,
                    UpdatedAt = statusFromDB.UpdatedAt,
                    Id = statusFromDB.Id,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StatusResponse> UpdateStatusAsync(Status statusToUpdate, int id)
        {
            try
            {
                var statusFromDB = await _statusRepository.GetStatusByIdAsync(id)
                ??
                throw new Exception("Status.Id does not persist on the database.");

                statusToUpdate.Id = statusFromDB.Id;

                _baseRepository.Update(statusToUpdate);

                if (await _baseRepository.SaveChangesAsync())
                {
                    var status = await _statusRepository.GetStatusByIdAsync(statusToUpdate.Id);

                    return new StatusResponse()
                    {
                        Description = status.Description,
                        CreatedAt = status.CreatedAt,
                        UpdatedAt = status.UpdatedAt,
                        Id = status.Id,
                    };
                }


                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}