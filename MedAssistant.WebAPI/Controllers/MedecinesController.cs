using AutoMapper;
using Hangfire;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedecinesController : ControllerBase
    {
        private readonly IMedicineService medicineService;
        private readonly IMapper mapper;


        public MedecinesController(IMedicineService medicineService, IMapper mapper)
        {
            this.medicineService = medicineService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Add new medecines from tabletka.by Monthly
        /// </summary>
        /// <returns>OK</returns>
        [HttpPost]
        public async Task<IActionResult> AddMedicines()
        { 
            try
            {
                RecurringJob.AddOrUpdate(()=> medicineService.GetAllMedecinesFromTabletkaAsync(),Cron.Monthly);
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
        [HttpPost("CreateMedecine")]
        [ProducesResponseType(typeof(MedicinesRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMedicineType([FromBody] MedicinesRequestModel medicinesRequestModel)
        {
            try
            {
                if (medicinesRequestModel != null)
                {
                    int entity = await medicineService.AddMedecineAsync(mapper.Map<MedicineDTO>(medicinesRequestModel));

                    if (entity > 0)
                    {
                        return Ok(medicinesRequestModel);
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
        [HttpPut("UpdateMedicine")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMedicineType([FromQuery] int id, [FromBody] MedicinesRequestModel medicinesRequestModel)
        {
            try
            {
                if (medicinesRequestModel != null)
                {
                    var model = mapper.Map<MedicineDTO>(medicinesRequestModel);
                    model.Id = id;
                    await medicineService.UpdateMedecineAsync(model);
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
        [HttpDelete("DeleteMedicine")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedicineType(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = await medicineService.GetMedecineByIdAsync(id);
                    await medicineService.RemoveMedecineAsync(model);
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
        [HttpGet("GetMedicineById")]
        [ProducesResponseType(typeof(MedecinesResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var model = mapper.Map<MedecinesResponseModel>(await medicineService.GetMedecineByIdAsync(id));
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
        [HttpGet("GetAllMedicines")]
        [ProducesResponseType(typeof(List<MedecinesResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTypes()
        {
            try
            {
                var models = await medicineService.GetAllMedecinesFromDataBaseAsync();
                return Ok(models.Select(x => mapper.Map<MedecinesResponseModel>(x)));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Get medicines by portions 
        /// </summary>
        /// <returns>OK(models)</returns>
        [HttpGet("GetMedecinesByPortions")]
        [ProducesResponseType(typeof(List<MedecinesResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedecinesByPortions(int page, int count)
        {
            try
            {
                var dtos = await medicineService.GetAllMedecinesFromDataBaseAsync();
                var models = dtos.Skip(Convert.ToInt32(page-1) * count).Take(count).ToList();
                return Ok(models.Select(x => mapper.Map<MedecinesResponseModel>(x)));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

    }
}
 