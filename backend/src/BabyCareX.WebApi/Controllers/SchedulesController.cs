using BabyCareX.Application.Contracts;
using BabyCareX.Application.Models.Error;
using BabyCareX.Application.Models.Schedules;
using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BabyCareX.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedule([FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                var schedules = await _schedulesService.GetAllSchedulesAsync();
                if (schedules == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get all Schedules.",
                    DeveloperMessage = "An error during process to get all Schedules."
                });

                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all Schedules.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Schedule>> PostSchedule([FromBody] SchedulePostModel model, [FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add Schedule.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var scheduleToSave = new Schedule()
                {
                    BabaId = (int)model.BabaId!,
                    FamilyId = (int)model.FamilyId!,
                    TimesAWeek = (int)model.TimesAWeek!,
                };

                var schedule = await _schedulesService.AddScheduleAsync(scheduleToSave);
                if (schedule == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to add Schedule.",
                    DeveloperMessage = "An error during process to add Schedule."
                });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Schedule>> UpdateScheduleById([FromBody] ScheduleUpdateModel model, [FromRoute] int id, [FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to ",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var scheduleToUpdate = new Schedule()
                {
                    BabaId = (int)model.BabaId!,
                    FamilyId = (int)model.FamilyId!,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    UpdatedAt = model.UpdatedAt,
                    TimesAWeek = (int)model.TimesAWeek!,
                    StatusId = (EStatus)model.StatusId!,
                };

                var schedule = await _schedulesService.UpdateScheduleAsync(scheduleToUpdate, id);
                if (schedule == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to update Schedule.",
                    DeveloperMessage = "An error during process to update Schedule."
                });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteSchedule([FromRoute] int id, [FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                return !await _schedulesService.DeleteScheduleByIdAsync(id) ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete Schedule.",
                    DeveloperMessage = "An error during process to delete Schedule."
                }) :
                Ok("Deleted");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetScheduleById([FromRoute] int id, [FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                var schedule = await _schedulesService.GetScheduleByIdAsync(id);
                if (schedule == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get Schedule.",
                    DeveloperMessage = "Schedule.Id does not persist on the database."
                });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{familyId:int}/family")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllScheduleByFamilyId([FromRoute] int familyId, [FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                var schedule = await _schedulesService.GetAllSchedulesByFamilyId(familyId);
                if (schedule == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get Schedule.",
                    DeveloperMessage = "There aren't any Schedules registered by this FamilyId."
                });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{babaId:int}/baba")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllScheduleByBabaId([FromRoute] int babaId, [FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                var schedule = await _schedulesService.GetAllSchedulesByBabaId(babaId);
                if (schedule == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get Schedule.",
                    DeveloperMessage = "There aren't any Schedules registered by this BabaId."
                });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult> DeleteAllSchedules([FromServices] ISchedulesService _schedulesService)
        {
            try
            {
                return !await _schedulesService.DeleteAllSchedulesAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all Schedule.",
                    DeveloperMessage = "An error during process to delete all Schedule."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all Schedule.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }


    }
}