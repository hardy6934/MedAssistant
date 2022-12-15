using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;


        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            this.doctorService = doctorService;
            this.mapper = mapper;
        }
          

        /// <summary>
        /// Add new doctor  
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost("CreateDoctor")]
        [Authorize]
        [ProducesResponseType(typeof(DoctorRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMedicineType([FromBody] DoctorRequestModel doctorRequestModel)
        {
            try
            {
                if (doctorRequestModel != null)
                {
                    int entity = await doctorService.AddDoctorAsync(mapper.Map<DoctorDTO>(doctorRequestModel));

                    if (entity > 0)
                    {
                        return Ok(doctorRequestModel);
                    }
                    else return BadRequest();
                }
                else return BadRequest();


            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update doctor  
        /// </summary>
        /// <returns>204</returns>
        [HttpPut("UpdateDoctor")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDoctor([FromQuery] int id, [FromBody] DoctorRequestModel doctorRequestModel)
        {
            try
            {
                if (doctorRequestModel != null)
                {
                    var model = mapper.Map<DoctorDTO>(doctorRequestModel);
                    model.Id = id;
                    await doctorService.UpdateDoctorAsync(model);
                }
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete doctor type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpDelete("DeleteDoctor")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                if (id != 0)
                {
                    DoctorDTO model = new();
                    model.Id = id;
                    await doctorService.RemoveDoctorAsync(model);
                }

                return StatusCode(204);
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
        [HttpGet("GetDoctorById")]
        [Authorize]
        [ProducesResponseType(typeof(DoctorResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = mapper.Map<DoctorResponseModel>(await doctorService.GetDoctorByIdAsync(id));
                    return Ok(model);
                }

                return NotFound();
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
        [HttpGet("GetAllDoctorsForUser")]
        [Authorize]
        [ProducesResponseType(typeof(List<DoctorResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                //var role = e.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role:").Select(x => x.Value).FirstOrDefault();
                //if(role == "User" || role == "Moderator" || role == "Admin")

                var email = claims.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Select(x => x.Value).FirstOrDefault();

                var models = await doctorService.GetAllDoctorsByEmailAsync(email);
                return Ok(models.Select(x => mapper.Map<DoctorResponseModel>(x)));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
