using Hangfire;
using MedAssistant.Core.Abstractions;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedecinesController : ControllerBase
    {
        private readonly IMedicineService medicineService;

        public MedecinesController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
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
                return StatusCode(500, ex.Message);
            }
        }
    }
}
 