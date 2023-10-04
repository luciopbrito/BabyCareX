using BabyCareX.Application.Contracts;
using BabyCareX.Application.Models;
using BabyCareX.Application.Models.Children;
using BabyCareX.Application.Models.Error;
using BabyCareX.Application.Models.Family;
using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BabyCareX.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamiliesController : ControllerBase
    {
        #region Families

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Family>> PostAsync(FamilyPostModel model, [FromServices] IFamilyService familyService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add family.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var modelToSave = new Family()
                {
                    Children = model.Children,
                    Email = model.Email!,
                    MotherName = model.MotherName,
                    Password = model.Password!,
                    Schedules = model.Schedules,
                    FatherName = model.FatherName,
                    FamilyName = model.FamilyName!,
                    PhoneNumber = model.PhoneNumber!
                };

                var family = await familyService.AddFamilyAsync(modelToSave);

                if (family == null)
                {
                    return BadRequest(new ErrorHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add family.",
                        DeveloperMessage = "An error during process to add family."
                    });
                }

                return Ok(family);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add family.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Family>> GetFamilyByEmailAndPasswordAsync(LoginModel model, [FromServices] IFamilyService familyService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to login.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var family = await familyService.GetFamilyByEmailAndPasswordAsync(model.Email, model.Password);

                if (family == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Email or Password does not persist on the database",
                        UserMessage = "An error during the request to login."
                    });

                return Ok(family);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to login.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Family>> GetFamilyByIdAsync([FromRoute] int id, [FromServices] IFamilyService familyService)
        {
            try
            {
                var family = await familyService.GetFamilyByIdAsync(id);

                if (family == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request get Family.",
                        DeveloperMessage = "Family.id does not persist on the database."
                    });

                return Ok(family);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request get Family.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Family>>> GetAllFamiliesAsync([FromServices] IFamilyService familyService)
        {
            try
            {
                var families = await familyService.GetAllFamiliesAsync();

                if (families == null)
                    return BadRequest(new ErrorHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to get all families.",
                        DeveloperMessage = "There isn't any Family on the database."
                    });

                return Ok(families);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all the families. ",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdateFamilyAsync(FamilyUpdateModel model, [FromRoute] int id, [FromServices] IFamilyService familyService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update family.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var familyToPersist = new Family()
                {
                    FamilyName = model.FamilyName!,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    Password = model.Password!,
                    PhoneNumber = model.PhoneNumber!,
                    Email = model.Email!,
                    Children = model.Children,
                    Schedules = model.Schedules,
                    StatusId = (EStatus)model.StatusId!,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    UpdatedAt = model.UpdatedAt,
                };

                var family = await familyService.UpdateFamilyAsync(familyToPersist, id);

                if (family == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        UserMessage = "An error during the request to update family",
                        DeveloperMessage = "An error during process to update family."
                    });
                }

                return Ok(family);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update family.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteFamilyByIdAsync([FromRoute] int id, [FromServices] IFamilyService familyService)
        {
            try
            {
                return !await familyService.DeleteFamilyByIdAsync(id) ?
                NotFound(new ErrorHandling()
                {
                    Status = StatusCodes.Status404NotFound,
                    DeveloperMessage = "An error during process to delete family.",
                    UserMessage = "An error during the request to delete family."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete family.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult> DeleteAllFamilies([FromServices] IFamilyService familyService)
        {
            try
            {
                return !await familyService.DeleteAllFamiliesAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all Families.",
                    DeveloperMessage = "An error during process to delete all Families."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all Families.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        #endregion

        #region Children

        [HttpGet]
        [Route("Children")]
        public async Task<ActionResult<IEnumerable<Child>>> GetAllChildren([FromServices] IChildrenService childrenService)
        {
            try
            {
                var children = await childrenService.GetAllChildrenAsync();
                if (children == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get all Children.",
                    DeveloperMessage = "There isn't any Children on the database."
                });

                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all Children.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}/Children/{childrenId:int}")]
        public async Task<ActionResult<IEnumerable<Child>>> GetChildrenById([FromRoute] int id, [FromRoute] int childrenId, [FromServices] IChildrenService childrenService, [FromServices] IFamilyService familyService)
        {
            try
            {
                var family = await familyService.GetFamilyByIdAsync(id);
                if (family == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Family.Id does not persist on the database.",
                        UserMessage = "An error during the request to get Child."
                    });

                var children = await childrenService.GetChildByIdAsync(childrenId);
                if (children == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get Child.",
                    DeveloperMessage = "Child.Id does not persist on the database."
                });

                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get Children.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPost]
        [Route("{id:int}/Children")]
        public async Task<ActionResult<Child>> PostChild([FromRoute] int id, [FromBody] ChildrenPostModel model, [FromServices] IChildrenService childrenService, [FromServices] IFamilyService familyService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add Child.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var family = await familyService.GetFamilyByIdAsync(id);
                if (family == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Family.Id does not persist on the database.",
                        UserMessage = "An error during the request to add Child."
                    });

                var childToSave = new Child()
                {
                    Age = (int)model.Age!,
                    FamilyId = (int)model.FamilyId!,
                    IsSpecialChild = (bool)model.IsSpecialChild!,
                    Name = model.Name!,
                    Sex = (ESex)model.Sex!,
                };

                var child = await childrenService.AddChildAsync(childToSave);
                if (child == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to add Child.",
                    DeveloperMessage = "An error during process to add Child."
                });

                return Ok(child);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add Child.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}/Children/{childrenId:int}")]
        public async Task<ActionResult<Child>> UpdateChildById([FromBody] ChildrenUpdateModel model, [FromRoute] int id, [FromRoute] int childrenId, [FromServices] IChildrenService childrenService, [FromServices] IFamilyService familyService)
        {
            try
            {
                if (model.FamilyId != id)
                    ModelState.AddModelError(nameof(model.FamilyId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update Child.",
                        DeveloperMessage = "Model isn't right.",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var family = await familyService.GetFamilyByIdAsync(id);
                if (family == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Family.Id does not persist on the database.",
                        UserMessage = "An error during the request to update Child."
                    });

                var childToUpdate = new Child()
                {
                    Age = (int)model.Age!,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    UpdatedAt = model.UpdatedAt,
                    FamilyId = (int)model.FamilyId!,
                    IsSpecialChild = (bool)model.IsSpecialChild!,
                    Name = model.Name!,
                    Sex = (ESex)model.Sex!,
                    StatusId = (EStatus)model.StatusId!,
                };

                var child = await childrenService.UpdateChildAsync(childToUpdate, childrenId);
                if (child == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to update Child.",
                    DeveloperMessage = "An error during process to update Child."
                });

                return Ok(child);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update Child.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}/Children/{childrenId:int}")]
        public async Task<ActionResult<string>> DeleteChildById([FromRoute] int id, [FromRoute] int childrenId, [FromServices] IChildrenService childrenService, [FromServices] IFamilyService familyService)
        {
            try
            {
                var family = await familyService.GetFamilyByIdAsync(id);
                if (family == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Family.Id does not persist on the database.",
                        UserMessage = "An error during the request to delete Child."
                    });

                return !await childrenService.DeleteChildByIdAsync(childrenId) ? BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete Child.",
                    DeveloperMessage = "An error during process to delete Child."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete Child.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("Children")]
        public async Task<ActionResult<string>> DeleteAllChildren([FromServices] IChildrenService childrenService)
        {
            try
            {
                return !await childrenService.DeleteAllChildrenAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all Children.",
                    DeveloperMessage = "An error during process to delete all Children."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all Children.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        #endregion

    }
}