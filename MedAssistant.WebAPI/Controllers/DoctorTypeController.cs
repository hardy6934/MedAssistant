using AutoMapper;
using Hangfire;
using MedAssistant.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
    }
}
