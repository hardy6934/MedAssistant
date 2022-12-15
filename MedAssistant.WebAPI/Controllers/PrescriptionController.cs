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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService prescriptionService;
        private readonly IMapper mapper;


        public PrescriptionController(IPrescriptionService prescriptionService, IMapper mapper)
        {
            this.prescriptionService = prescriptionService;
            this.mapper = mapper;
        }

          

        /// <summary>
        /// Add new doctor type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost("CreatePrescription")]
        [Authorize]
        [ProducesResponseType(typeof(PrescriptionRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestModel prescriptionRequestModel)
        {
            try
            {
                if (prescriptionRequestModel != null)
                {
                    int entity = await prescriptionService.CreatePrescriptionAsync(mapper.Map<PrescriptionDTO>(prescriptionRequestModel));

                    if (entity > 0)
                    {
                        return Ok(prescriptionRequestModel);
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
        [HttpPut("UpdatePrescription")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePrescription([FromQuery] int id, [FromBody] PrescriptionRequestModel prescriptionRequestModel)
        {
            try
            {
                if (prescriptionRequestModel != null)
                {
                    var model = mapper.Map<PrescriptionDTO>(prescriptionRequestModel);
                    model.Id = id;
                    await prescriptionService.UpdatePrescriptionAsync(model);
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
        [HttpDelete("DeletePrescription")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            try
            {
                if (id != 0)
                { 
                    await prescriptionService.RemovePrescriptionAsync(id);
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
        [HttpGet("GetPrescriptionById")]
        [Authorize]
        [ProducesResponseType(typeof(PrescriptionsResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = mapper.Map<PrescriptionsResponseModel>(await prescriptionService.GetPrescriptionByIdAsync(id));
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
        [HttpGet("GetAllPrescriptionsForUser")]
        [Authorize] 
        [ProducesResponseType(typeof(List<PrescriptionsResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPrescriptionsForUser()
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var email = claims.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Select(x => x.Value).FirstOrDefault();

                var models = await prescriptionService.GetPrescriptionsbyUserEmailAsync(email);
                return Ok(models.Select(x => mapper.Map<PrescriptionsResponseModel>(x)));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }
         
    }
}
