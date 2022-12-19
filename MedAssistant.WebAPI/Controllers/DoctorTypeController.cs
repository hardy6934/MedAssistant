using AutoMapper;
using Hangfire;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDoctorTypeService doctorTypeService;

        public DoctorTypeController(IMapper mapper, IDoctorTypeService doctorTypeService)
        {
            this.doctorTypeService = doctorTypeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Add new doctor types from https://med-tutorial.ru/med-doctors Monthly
        /// </summary>
        /// <returns>OK</returns>
        [HttpPost]
        public async Task<IActionResult> AddDoctorTypes()
        {
            try
            {
                RecurringJob.AddOrUpdate(() => doctorTypeService.GetAllDoctorTypesFromMedTutorial(), Cron.Monthly);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Add new doctor type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost("DoctorType")]
        [ProducesResponseType(typeof(DoctorTypeRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDoctorType([FromBody] DoctorTypeRequestModel doctorTypeRequestModel)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (doctorTypeRequestModel != null)
                    {
                        int entity = await doctorTypeService.AddDoctorTypeAsync(mapper.Map<DoctorTypeDTO>(doctorTypeRequestModel));

                        if (entity > 0)
                        {
                            return Ok(doctorTypeRequestModel);
                        }
                        else return BadRequest();
                    }
                    else return BadRequest();
                }
                else
                    return StatusCode(403);
                 
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update doctor type 
        /// </summary>
        /// <returns>204</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDoctorType([FromQuery]int id, [FromBody]DoctorTypeRequestModel doctorTypeRequestModel)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (doctorTypeRequestModel != null)
                    {
                        var model = mapper.Map<DoctorTypeDTO>(doctorTypeRequestModel);
                        model.Id = id;
                        await doctorTypeService.UpdateDoctorTypeAsync(model);
                    }
                    return StatusCode(204);
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete doctor type by id
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDoctorType(int id)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (id != 0)
                    {
                     var model = await doctorTypeService.GetDoctorTypeByIdAsync(id);
                     await doctorTypeService.RemoveDoctorTypeAsync(model); 
                    } 
                    return StatusCode(204);
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Get doctor type by id
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDoctorTypeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTypeById(int id)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (id != 0)
                    {
                        var model = mapper.Map<ResponseDoctorTypeModel>(await doctorTypeService.GetDoctorTypeByIdAsync(id));
                        return Ok(model);
                    }

                    return NotFound();
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }


        /// <summary>
        /// Get all doctor types 
        /// </summary>
        /// <returns>OK(models)</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseDoctorTypeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTypes()
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    var models = await doctorTypeService.GetAllDoctorTypes();  
                    return Ok(models.Select(x=> mapper.Map<ResponseDoctorTypeModel>(x)));
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
