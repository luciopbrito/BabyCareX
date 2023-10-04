using BabyCareX.Application.Contracts;
using BabyCareX.Application.Models.Error;
using BabyCareX.Application.Models.StatusModels;
using BabyCareX.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BabyCareX.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<StatusResponse>>> GetAllStatus([FromServices] IStatusService _statusService)
        {
            try
            {
                var status = await _statusService.GetAllStatusAsync();
                if (status == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get all Status.",
                    DeveloperMessage = "An error during process to get all Status."
                });

                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all Status.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<StatusResponse>> PostStatus(StatusPostModel model, [FromServices] IStatusService _statusService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add Status.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var statusToSave = new Status()
                {
                    Description = model.Description!,
                    StatusId = Domain.Enums.EStatus.CANCELADO,

                };

                var status = await _statusService.AddStatusAsync(statusToSave);
                if (status == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to add Status.",
                    DeveloperMessage = "An error during process to add Status."
                });


                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add Status.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<StatusResponse>> UpdateStatusById(StatusUpdateModel model, [FromRoute] int id, [FromServices] IStatusService _statusService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update Status.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var statusToUpdate = new Status()
                {
                    CreatedAt = (DateTime)model.CreatedAt!,
                    Description = model.Description!,
                    UpdatedAt = model.UpdatedAt,
                };

                var status = await _statusService.UpdateStatusAsync(statusToUpdate, id);
                if (status == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to update Status.",
                    DeveloperMessage = "An error during process to update Status."
                });

                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update Status.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteStatusById([FromRoute] int id, [FromServices] IStatusService _statusService)
        {
            try
            {
                return !await _statusService.DeleteStatusByIdAsync(id) ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete Status.",
                    DeveloperMessage = "An error during process to delete Status."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete Status.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetStatusById([FromRoute] int id, [FromServices] IStatusService _statusService)
        {
            try
            {
                var status = await _statusService.GetStatusByIdAsync(id);
                if (status == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get Status.",
                    DeveloperMessage = "Status.Id does not persist on the database."
                });

                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get Status.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult> DeleteAllStatus([FromServices] IStatusService _statusService)
        {
            try
            {
                return !await _statusService.DeleteAllStatusAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete Status.",
                    DeveloperMessage = "An error during process to delete Status."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete Status.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

    }
}