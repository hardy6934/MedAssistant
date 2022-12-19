using AutoMapper;
using Hangfire;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalInstitutionsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMedicalInstitutionService medicalInstitutionService;

        public MedicalInstitutionsController(IMapper mapper, IMedicalInstitutionService medicalInstitutionService)
        { 
            this.mapper = mapper;
            this.medicalInstitutionService = medicalInstitutionService;
        }


        /// <summary>
        /// Add new medical institutions from site Monthly
        /// </summary>
        /// <returns>OK</returns>
        [HttpPost]
        public async Task<IActionResult> AddMedInstitutionAsync()
        {
            try
            {
                RecurringJob.AddOrUpdate(() => medicalInstitutionService.AddMedicalInstitutionsAsync(), Cron.Monthly); 
                return Ok();
            } 
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
              
        }

        /// <summary>
        /// Add new Medical institution 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost("MedicalInstitutions")]
        [Authorize]
        [ProducesResponseType(typeof(MedicalInstitutionRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMedicalInstitution([FromBody] MedicalInstitutionRequestModel medicalInstitutionRequestModel)
        {
            try
            {
                if (medicalInstitutionRequestModel != null)
                {
                    int entity = await medicalInstitutionService.AddMedicalInstitutionAsync(mapper.Map<MedicalInstitutionDTO>(medicalInstitutionRequestModel));

                    if (entity > 0)
                    {
                        return Ok(medicalInstitutionRequestModel);
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
        /// Update Medical institution 
        /// </summary>
        /// <returns>204</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMedicalInstitution([FromQuery] int id, [FromBody] MedicalInstitutionRequestModel medicalInstitutionRequestModel)
        {
            try
            {
                if (medicalInstitutionRequestModel != null)
                {
                    var model = mapper.Map<MedicalInstitutionDTO>(medicalInstitutionRequestModel);
                    model.Id = id;
                    await medicalInstitutionService.UpdateMedicalInstitutionAsync(model);
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
        /// Delete Medical institution 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedicalInstitution(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = await medicalInstitutionService.GetMedicalInstitutionByIdAsync(id);
                    await medicalInstitutionService.RemoveMedicalInstitutionAsync(model);
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
        /// Get Medical institution by id
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(MedicalInstitutionResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedicalInstitutionById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = mapper.Map<MedicalInstitutionResponseModel>(await medicalInstitutionService.GetMedicalInstitutionByIdAsync(id));
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
        /// Get all Medical institutions 
        /// </summary>
        /// <returns>OK(models)</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<MedicalInstitutionResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMedicalInstitution()
        {
            try
            {
                var models = await medicalInstitutionService.GetAllMedicalInstitutionsFromDataBaseAsync();
                return Ok(models.Select(x => mapper.Map<MedicalInstitutionResponseModel>(x)));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
