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
        /// Add new medical institutions from https://clinics.medsovet.info/belarus/bolnicy?cat=0&page=1 Monthly
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
    }
}
