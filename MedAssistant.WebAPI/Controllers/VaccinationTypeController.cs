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
    public class VaccinationTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IVaccinationTypeService vaccinationTypeService;

        public VaccinationTypeController(IMapper mapper, IVaccinationTypeService vaccinationTypeService)
        {
            this.vaccinationTypeService = vaccinationTypeService;
            this.mapper = mapper;
        }

          
        /// <summary>
        /// Add new Vaccination type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(VaccinationTypeRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddVaccinationType([FromBody] VaccinationTypeRequestModel vaccinationTypeRequestModel)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                { 
                    if (vaccinationTypeRequestModel != null)
                    {
                        int entity = await vaccinationTypeService.AddVaccinationTypeAsync(mapper.Map<VaccinationTypeDTO>(vaccinationTypeRequestModel));

                        if (entity > 0)
                        {
                            return Ok(vaccinationTypeRequestModel);
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
        /// Update vaccination type 
        /// </summary>
        /// <returns>204</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVaccinationType([FromQuery] int id, [FromBody] VaccinationTypeRequestModel vaccinationTypeRequestModel)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (vaccinationTypeRequestModel != null)
                    {
                        var model = mapper.Map<VaccinationTypeDTO>(vaccinationTypeRequestModel);
                        model.Id = id;
                        await vaccinationTypeService.UpdateVaccinationTypeAsync(model);
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
        /// Delete doctor type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVaccinationType(int id)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (id != 0)
                    {
                        var model = await vaccinationTypeService.GetVaccinationTypeByIdAsync(id);
                        await vaccinationTypeService.RemoveVaccinationTypeAsync(model);
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
        /// Get vaccination type by id
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(VaccinationTypeResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVaccinationTypeById(int id)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (id != 0)
                    {
                        var model = mapper.Map<VaccinationTypeResponseModel>(await vaccinationTypeService.GetVaccinationTypeByIdAsync(id));
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
        /// Get all vaccination types 
        /// </summary>
        /// <returns>OK(models)</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<VaccinationTypeResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVaccinationTypes()
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    var models = await vaccinationTypeService.GetAllVaccinationTypes();
                    return Ok(models.Select(x => mapper.Map<VaccinationTypeResponseModel>(x)));
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
