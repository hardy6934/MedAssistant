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
        /// Add new medecine
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost("Medecines")]
        [Authorize]
        [ProducesResponseType(typeof(MedicinesRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMedicine([FromBody] MedicinesRequestModel medicinesRequestModel)
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
        /// Update medecine 
        /// </summary>
        /// <returns>204</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMedicine([FromQuery] int id, [FromBody] MedicinesRequestModel medicinesRequestModel)
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
        /// Delete medecine 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedicine(int id)
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
        /// Get medecine by id
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpGet("{id}")]
        [Authorize]
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
        /// Get all medecines 
        /// </summary>
        /// <returns>OK(models)</returns>
        [HttpGet]
        [Authorize]
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
        [HttpGet("{page}/{count}")]
        [Authorize]
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
 