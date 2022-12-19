using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
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
    public class VaccinationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IVaccinationService vaccinationService;

        public VaccinationController(IMapper mapper, IVaccinationService vaccinationService)
        {

            this.mapper = mapper;
            this.vaccinationService = vaccinationService;

        }

        /// <summary>
        /// Add new vaccination 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(VaccinationRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVaccination([FromBody] VaccinationRequestModel vaccinationRequestModel)
        {
            try
            {
                if (vaccinationRequestModel != null)
                {
                    int entity = await vaccinationService.CreateVaccinationAsync(mapper.Map<VaccinationDTO>(vaccinationRequestModel));

                    if (entity > 0)
                    {
                        return Ok(vaccinationRequestModel);
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
        /// Update doctor type 
        /// </summary>
        /// <returns>204</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVaccination([FromQuery] int id, [FromBody] VaccinationRequestModel vaccinationRequestModel)
        {
            try
            {
                if (vaccinationRequestModel != null)
                {
                    var model = mapper.Map<VaccinationDTO>(vaccinationRequestModel);
                    model.Id = id;
                    await vaccinationService.UpdateVaccinationAsync(model);
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
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVaccination(int id)
        {
            try
            {
                if (id != 0)
                { 
                    await vaccinationService.RemoveVaccinationAsync(id);
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
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(MedecinesResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVaccinationById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = mapper.Map<VaccinationsResponseModel>(await vaccinationService.GetVaccinationByIdAsync(id));
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
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<VaccinationsResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVaccinations()
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var email = claims.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Select(x => x.Value).FirstOrDefault();

                var models = await vaccinationService.GetVaccinationsbyUserEmailAsync(email);
                return Ok(models.Select(x => mapper.Map<VaccinationsResponseModel>(x)));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

         
    }
} 