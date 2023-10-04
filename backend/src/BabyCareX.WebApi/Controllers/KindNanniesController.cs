using BabyCareX.Application.Contracts;
using BabyCareX.Application.Models.Error;
using BabyCareX.Application.Models.KindNannies;
using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BabyCareX.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KindNanniesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<KindNanny>> GetAllKindNannies([FromServices] IKindNanniesService _kindNanniesService)
        {
            try
            {
                var kindNanny = await _kindNanniesService.GetAllKindNanniesAsync();
                if (kindNanny == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get all KindNannies.",
                    DeveloperMessage = "There aren't any KindNannies on the database."
                });

                return Ok(kindNanny);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all KindNannies.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<KindNanny>> GetKindNannyById([FromRoute] int id, [FromServices] IKindNanniesService _kindNanniesService)
        {
            try
            {
                var kindNanny = await _kindNanniesService.GetKindNannyByIdAsync(id);
                if (kindNanny == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get KindNanny.",
                    DeveloperMessage = "KindNanny.Id does not exits on the database."
                });

                return Ok(kindNanny);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get KindNanny.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> PostKindNanny([FromBody] KindNanniesPostModel model, [FromServices] IKindNanniesService _kindNanniesService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add KindNanny.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var kindNannyToSave = new KindNanny()
                {
                    Description = model.Description!,
                    Name = model.Name!,
                };

                var kindNanny = await _kindNanniesService.AddKindNannyAsync(kindNannyToSave);
                if (kindNanny == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to add KindNanny.",
                    DeveloperMessage = "An error during process to add KindNanny."
                });

                return Ok(kindNanny);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add KindNanny.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<KindNanny>> UpdateKindNannyById([FromBody] KindNanniesUpdateModel model, [FromRoute] int id, [FromServices] IKindNanniesService _kindNanniesService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update KindNanny.",
                        DeveloperMessage = "Model isn't right.",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var kindNannyToUpdate = new KindNanny()
                {
                    Description = model.Description!,
                    Name = model.Name!,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    UpdatedAt = model.UpdatedAt,
                    StatusId = (EStatus)model.StatusId!,
                };

                var kindNanny = await _kindNanniesService.UpdateKindNannyAsync(kindNannyToUpdate, id);
                if (kindNanny == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to update KindNanny.",
                    DeveloperMessage = "An error during process to update KindNanny."
                });

                return Ok(kindNanny);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update KindNanny.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteKindNannyById([FromRoute] int id, [FromServices] IKindNanniesService _kindNanniesService)
        {
            try
            {
                return !await _kindNanniesService.DeleteKindNannyByIdAsync(id) ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete KindNanny.",
                    DeveloperMessage = "An error during process to delete KindNanny."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete KindNanny.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                });
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult> DeleteAllKindNannies([FromServices] IKindNanniesService _kindNanniesService)
        {
            try
            {
                return !await _kindNanniesService.DeleteAllKindNanniesAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all KindNannies.",
                    DeveloperMessage = "An error during process to delete all KindNannies."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all KindNannies.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

    }
}