using BabyCareX.Application.Contracts;
using BabyCareX.Application.Models;
using BabyCareX.Application.Models.Error;
using BabyCareX.Application.Models.Babas;
using BabyCareX.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using BabyCareX.Domain.Enums;
using BabyCareX.Application.Models.BabaCapacities;
using BabyCareX.Application.Models.BabaCourses;
using BabyCareX.Application.Models.BabaProvideServices;

namespace BabyCareX.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BabasController : ControllerBase
    {
        #region Baba

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Baba>> PostBaba([FromServices] IBabaService babaService, [FromBody] BabaPostModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add Baba.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var babaToSave = new Baba()
                {
                    BabaCapacities = model.BabaCapacities,
                    BabaCourses = model.BabaCourses,
                    BabaProvideServices = model.BabaProvideServices,
                    Description = model.Description,
                    Email = model.Email!,
                    Name = model.Name!,
                    Password = model.Password!,
                    PhoneNumber = model.PhoneNumber!,
                    Schedules = model.Schedules,
                };

                var baba = await babaService.AddBabaAsync(babaToSave);
                if (baba == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to add Baba.",
                    DeveloperMessage = "An error during process to add Baba."
                });

                return Ok(baba);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add Baba.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Baba>> GetBabaByEmailAndPasswordAsync([FromServices] IBabaService babaService, [FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to login",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!,
                    });

                var baba = await babaService.GetBabaByEmailAndPasswordAsync(model.Email, model.Password);

                if (baba == null)
                {
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Email or Password does not persist on the database",
                        UserMessage = "An error during the request to login."
                    });
                }

                return Ok(baba);
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
        [Route("")]
        public async Task<ActionResult<IEnumerable<Baba>>> GetAllBabas([FromServices] IBabaService babaService)
        {
            try
            {
                var babas = await babaService.GetAllBabasAsync();
                if (babas == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get all Babas.",
                    DeveloperMessage = "There aren't any Babas on the database.",
                });

                return Ok(babas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all Babas.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Baba>> GetBabaByIdAsyncAsync([FromServices] IBabaService babaService, [FromRoute] int id)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);

                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.id does not persist on the database.",
                        UserMessage = "An error during the request to get Baba."
                    });

                return Ok(baba);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get Baba.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdateBabaAsync([FromServices] IBabaService babaService, [FromBody] BabaUpdateModel model, [FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update Baba.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });
                }

                var babaToUpdate = new Baba()
                {
                    BabaCapacities = model.BabaCapacities,
                    BabaCourses = model.BabaCourses,
                    BabaProvideServices = model.BabaProvideServices,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    Description = model.Description,
                    Email = model.Email!,
                    Name = model.Name!,
                    Password = model.Name!,
                    PhoneNumber = model.PhoneNumber!,
                    Schedules = model.Schedules,
                    StatusId = (EStatus)model.StatusId!,
                    UpdatedAt = model.UpdatedAt,
                };

                var baba = await babaService.UpdateBabaAsync(babaToUpdate, id);
                if (baba == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to update Baba.",
                    DeveloperMessage = "An error during process to update Baba."
                });

                return Ok(baba);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update Baba.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult> DeleteAllBabas([FromServices] IBabaService babaService)
        {
            try
            {
                return !await babaService.DeleteAllBabasAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all Babas.",
                    DeveloperMessage = "An error during process to delete all Babas."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all Babas.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteBabaByIdAsync([FromServices] IBabaService babaService, [FromRoute] int id)
        {
            try
            {
                return !await babaService.DeleteBabaByIdAsync(id) ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete Baba.",
                    DeveloperMessage = "An error during process to delete Baba."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete Baba.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        #endregion

        #region BabaCapacities

        [HttpPost]
        [Route("{id:int}/BabaCapacities")]
        public async Task<ActionResult<BabaCapacity>> PostBabaCapacity([FromRoute] int id, [FromBody] BabaCapacityPostModel model, [FromServices] IBabaCapacitiesService babaCapacitiesService, IBabaService babaService)
        {
            try
            {
                if (model.BabaId != id)
                    ModelState.AddModelError(nameof(model.BabaId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to save BabaCapacity",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!,
                    });

                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to add BabaCapacity."
                    });

                var babaCapacityToSave = new BabaCapacity()
                {
                    Name = model.Name!,
                    BabaId = (int)model.BabaId!,
                };

                var babaCapacity = await babaCapacitiesService.AddBabaCapacityAsync(babaCapacityToSave);

                if (babaCapacity == null)
                    return BadRequest(new ErrorHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        DeveloperMessage = "An error to save BabaCapacity.",
                        UserMessage = "An error during the request to save BabaCapacity"
                    });

                return Ok(babaCapacity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to save BabaCapacity",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}/BabaCapacities/{babaCapacitiesId:int}")]
        public async Task<ActionResult<BabaCapacity>> GetBabaCapacityById([FromRoute] int id, [FromRoute] int babaCapacitiesId, [FromServices] IBabaCapacitiesService babaCapacitiesService, IBabaService babaService)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to get BabaCapacity."
                    });

                var babaCapacity = await babaCapacitiesService.GetBabaCapacityByIdAsync(babaCapacitiesId);
                if (babaCapacity == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "BabaCapacity.Id does not persist on the database.",
                        UserMessage = "An error during the request to get BabaCapacity."
                    });

                return Ok(babaCapacity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get BabaCapacity by id.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("BabaCapacities")]
        public async Task<ActionResult<IEnumerable<BabaCapacity>>> GetAllBabaCapacities([FromServices] IBabaCapacitiesService babaCapacitiesService)
        {
            try
            {
                var babaCapacities = await babaCapacitiesService.GetAllBabaCapacitiesAsync();
                if (babaCapacities == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    DeveloperMessage = "There aren't any BabaCapacities on the database.",
                    UserMessage = "An error during the request to get all BabaCapacities."
                });

                return Ok(babaCapacities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all BabaCapacities.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}/BabaCapacities/{babaCapacityId:int}")]
        public async Task<ActionResult<BabaCapacity>> UpdateBabaCapacity([FromRoute] int babaCapacityId, [FromBody] BabaCapacityUpdateModel model, [FromRoute] int id, [FromServices] IBabaCapacitiesService babaCapacitiesService, IBabaService babaService)
        {
            try
            {
                if (model.BabaId != id)
                    ModelState.AddModelError(nameof(model.BabaId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!,
                        UserMessage = "An error during the request to update BabaCapacity"
                    });

                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to update BabaCapacity."
                    });

                var babaCapacityToUpdate = new BabaCapacity()
                {
                    Name = model.Name!,
                    BabaId = (int)model.BabaId!,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    UpdatedAt = model.UpdatedAt,
                    StatusId = (EStatus)model.StatusId!,
                    Baba = model.Baba
                };

                var babaCapacity = await babaCapacitiesService.UpdateBabaCapacityAsync(babaCapacityToUpdate, babaCapacityId);

                if (babaCapacity == null)
                    return BadRequest(new ErrorHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        DeveloperMessage = "BabaCapacity.Id does not persist on the database",
                        UserMessage = "An error during the request to update BabaCapacity."
                    });

                return Ok(babaCapacity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update BabaCapacity.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}/BabaCapacities/{babaCapacitiesId:int}")]
        public async Task<ActionResult<string>> DeleteBabaCapacity([FromRoute] int id, [FromRoute] int babaCapacitiesId, [FromServices] IBabaCapacitiesService babaCapacitiesService, IBabaService babaService)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to delete BabaCapacity."
                    });

                return await babaCapacitiesService.DeleteBabaCapacityByIdAsync(babaCapacitiesId) ?
                    Ok("Deleted") :
                    BadRequest(new ErrorHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        DeveloperMessage = "An error during process to delete BabaCapacity.",
                        UserMessage = "An error during the request to delete BabaCapacity."
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete BabaCapacity.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("BabaCapacities")]
        public async Task<ActionResult<string>> DeleteAllBabaCapacities([FromServices] IBabaCapacitiesService babaCapacitiesService)
        {
            try
            {
                return !await babaCapacitiesService.DeleteAllBabaCapacitiesAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete BabaCapacity.",
                    DeveloperMessage = "An error during process to delete BabaCapacity."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete BabaCapacity.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        #endregion

        #region BabaCourses

        [HttpPost]
        [Route("{id:int}/BabaCourses", Name = "criar curso")]
        public async Task<ActionResult<BabaProvideService>> PostBabaCourses([FromRoute] int id, [FromBody] BabaCoursePostModel model, [FromServices] IBabaCoursesService babaCoursesService, IBabaService babaService)
        {
            try
            {
                if (model.BabaId != id)
                    ModelState.AddModelError(nameof(model.BabaId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        UserMessage = "An error during the request to add BabaCourse.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to add BabaCourse."
                    });

                var babaCourseToSave = new BabaCourse()
                {
                    Name = model.Name!,
                    EndPeriod = model.EndPeriod,
                    StartPeriod = (DateTime)model.StartPeriod!,
                    IsCompleted = (bool)model.IsCompleted!,
                    BabaId = (int)model.BabaId!,
                };

                var babaCourse = await babaCoursesService.AddBabaCourseAsync(babaCourseToSave);
                if (babaCourse == null)
                    return BadRequest(new ErrorHandling()
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        UserMessage = "An error during the request to add BabaCourse.",
                        DeveloperMessage = "An error during process to save BabaCourse.",
                    });

                return Ok(babaCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add BabaCourse.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}/BabaCourses/{BabaCourseId:int}", Name = "Pegar curso por id")]
        public async Task<ActionResult<BabaProvideService>> GetBabaCoursesById([FromRoute] int id, [FromRoute] int BabaCourseId, [FromServices] IBabaCoursesService babaCoursesService, IBabaService babaService)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to get BabaCourse."
                    });

                var babaCourse = await babaCoursesService.GetBabaCourseByIdAsync(BabaCourseId);
                if (babaCourse == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        UserMessage = "An error during the request to get  BabaCourse.",
                        DeveloperMessage = "BabaCourse.Id does not persist on the database."
                    });

                return Ok(babaCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete BabaCourse.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("BabaCourses", Name = "pegar todos os cursos cadastrados")]
        public async Task<ActionResult<BabaProvideService>> GetAllBabaCourses([FromServices] IBabaCoursesService babaCoursesService)
        {
            try
            {
                var babaCourses = await babaCoursesService.GetAllBabaCoursesAsync();
                if (babaCourses == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        UserMessage = "An error during the request to get all BabaCourse.",
                        DeveloperMessage = "There isn't any BabaCourse on the database."
                    });

                return Ok(babaCourses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all BabaCourse.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}/BabaCourses/{BabaCourseId:int}", Name = "atualizar curso")]
        public async Task<ActionResult<BabaProvideService>> UpdateBabaCoursesById([FromRoute] int id, [FromBody] BabaCourseUpdateModel model, [FromRoute] int BabaCourseId, [FromServices] IBabaCoursesService babaCoursesService, IBabaService babaService)
        {
            try
            {
                if (model.BabaId != id)
                    ModelState.AddModelError(nameof(model.BabaId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update BabaCourse.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to update BabaCourse."
                    });

                var babaCourseToSave = new BabaCourse()
                {
                    CreatedAt = (DateTime)model.CreatedAt!,
                    UpdatedAt = model.UpdatedAt,
                    StatusId = (EStatus)model.StatusId!,
                    BabaId = (int)model.BabaId!,
                    StartPeriod = (DateTime)model.StartPeriod!,
                    EndPeriod = model.EndPeriod,
                    IsCompleted = (bool)model.IsCompleted!,
                    Name = model.Name!,
                };

                var babaCourse = await babaCoursesService.UpdateBabaCourseAsync(babaCourseToSave, BabaCourseId);
                if (babaCourse == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    DeveloperMessage = "An error during process to update BabaCourse.",
                    UserMessage = "An error during the request to update BabaCourse."
                });

                return Ok(babaCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete BabaCourse.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}/BabaCourses/{BabaCourseId:int}", Name = "deletar curso")]
        public async Task<ActionResult> DeleteBabaCoursesById([FromRoute] int id, [FromRoute] int BabaCourseId, [FromServices] IBabaCoursesService babaCoursesService, IBabaService babaService)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to delete BabaCourse."
                    });

                return !await babaCoursesService.DeleteBabaCourseAsync(BabaCourseId) ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete Baba Course.",
                    DeveloperMessage = "An error during process to delete Baba Course."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete BabaCourse.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("BabaCourses", Name = "deletar todos")]
        public async Task<ActionResult> DeleteAllBabaCourses([FromServices] IBabaCoursesService babaCoursesService)
        {
            try
            {
                return !await babaCoursesService.DeleteAllBabaCoursesAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all BabaCourses.",
                    DeveloperMessage = "An error during process to delete all BabaCourses."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all BabaCourses.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        #endregion

        #region BabaProvideServices

        [HttpPost]
        [Route("{id:int}/BabaProvideServices")]
        public async Task<ActionResult<BabaProvideService>> PostBabaProvideServices([FromRoute] int id, [FromBody] BabaProvideServicesPostModel model, [FromServices] IBabaProvideSService babaProvideSService, IBabaService babaService)
        {
            try
            {
                if (model.BabaId != id)
                    ModelState.AddModelError(nameof(model.BabaId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to add BabaProvideServices.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to get BabaProvideService."
                    });

                var babaProvideServicesToSave = new BabaProvideService()
                {
                    BabaId = (int)model.BabaId!,
                    KindNannyId = (int)model.KindNannyId!
                };

                var babaProvideServices = await babaProvideSService.AddBabaProvideServiceAsync(babaProvideServicesToSave);
                if (babaProvideServices == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to add BabaProvideServices.",
                    DeveloperMessage = "An error during process to add BabaProvideServices."
                });

                return Ok(babaProvideServices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to add BabaProvideServices.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("BabaProvideServices")]
        public async Task<ActionResult<IEnumerable<BabaProvideService>>> GetAllBabaProvideServices([FromServices] IBabaProvideSService babaProvideSService)
        {
            try
            {
                var babaProvideServices = await babaProvideSService.GetAllBabaProvideServicesAsync();
                if (babaProvideServices == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get all BabaProvideServices.",
                    DeveloperMessage = "An error during process to get all BabaProvideServices."
                });

                return Ok(babaProvideServices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get all BabaProvideServices.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpGet]
        [Route("{id:int}/BabaProvideServices/{babaProvideServiceId:int}")]
        public async Task<ActionResult<BabaProvideService>> GetBabaProvideServiceById([FromRoute] int id, [FromRoute] int babaProvideServiceId, [FromServices] IBabaProvideSService babaProvideSService, IBabaService babaService)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to get BabaProvideService."
                    });

                var babaProvideServices = await babaProvideSService.GetBabaProvideServiceByIdAsync(babaProvideServiceId);
                if (babaProvideServices == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to get BabaProvideServices.",
                    DeveloperMessage = "BabaProvideService.Id does not persist on the database.",
                });

                return Ok(babaProvideServices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to get BabaProvideServices.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpPut]
        [Route("{id:int}/BabaProvideServices/{babaProvideServiceId:int}")]
        public async Task<ActionResult<BabaProvideService>> UpdateBabaProvideServicesById([FromRoute] int id, [FromBody] BabaProvideServicesUpdateModel model, [FromRoute] int babaProvideServiceId, [FromServices] IBabaProvideSService babaProvideSService, IBabaService babaService)
        {
            try
            {
                if (model.BabaId != id)
                    ModelState.AddModelError(nameof(model.BabaId), "Id does not match. Check if id from route and body is equal.");

                if (!ModelState.IsValid)
                    return BadRequest(new ErrorModelHandling()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        UserMessage = "An error during the request to update BabaProvideServices.",
                        DeveloperMessage = "Model isn't right",
                        Errors = UnprocessableEntity(ModelState).Value!
                    });

                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to get BabaProvideService."
                    });

                var babaProvideServicesToSave = new BabaProvideService()
                {
                    BabaId = (int)model.BabaId!,
                    KindNannyId = (int)model.KindNannyId!,
                    UpdatedAt = model.UpdatedAt,
                    CreatedAt = (DateTime)model.CreatedAt!,
                    StatusId = (EStatus)model.StatusId!,
                };

                var babaProvideServices = await babaProvideSService.UpdateBabaProvideServiceAsync(babaProvideServicesToSave, babaProvideServiceId);
                if (babaProvideServices == null) return BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to update BabaProvideServices.",
                    DeveloperMessage = "BabaProvideService.Id does not persist on the database.",
                });

                return Ok(babaProvideServices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to update BabaProvideServices.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}/BabaProvideServices/{babaProvideServiceId:int}")]
        public async Task<ActionResult<string>> DeleteBabaProvideServices([FromRoute] int id, [FromRoute] int babaProvideServiceId, [FromServices] IBabaProvideSService babaProvideSService, IBabaService babaService)
        {
            try
            {
                var baba = await babaService.GetBabaByIdAsync(id);
                if (baba == null)
                    return NotFound(new ErrorHandling()
                    {
                        Status = StatusCodes.Status404NotFound,
                        DeveloperMessage = "Baba.Id does not persist on the database.",
                        UserMessage = "An error during the request to delete BabaProvideService."
                    });

                return !await babaProvideSService.DeleteBabaProvideServiceAsync(babaProvideServiceId) ?
                NotFound(new ErrorHandling()
                {
                    Status = StatusCodes.Status404NotFound,
                    UserMessage = "An error during the request to delete BabaProvideServices.",
                    DeveloperMessage = "An error during process to delete BabaProvideServices."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete BabaProvideServices.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        [HttpDelete]
        [Route("BabaProvideServices")]
        public async Task<ActionResult> DeleteAllBabaProvideServices([FromServices] IBabaProvideSService babaProvideSService)
        {
            try
            {
                return !await babaProvideSService.DeleteAllBabaProvideServicesAsync() ?
                BadRequest(new ErrorHandling()
                {
                    Status = StatusCodes.Status400BadRequest,
                    UserMessage = "An error during the request to delete all BabaProvideServices.",
                    DeveloperMessage = "An error during process to delete all BabaProvideServices."
                }) :
                Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandling()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    UserMessage = "An error during the request to delete all BabaProvideServices.",
                    DeveloperMessage = $"Exception Message: {ex.Message}",
                    ErrorCode = ex.HResult.ToString()
                });
            }
        }

        #endregion

    }
}