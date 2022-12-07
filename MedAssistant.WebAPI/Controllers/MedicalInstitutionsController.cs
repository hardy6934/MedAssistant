using AutoMapper;
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

        [HttpPost]
        public async Task<IActionResult> AddMedInstitutionAsync()
        {
            try
            {
                await medicalInstitutionService.AddMedicalInstitutionsAsync();
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
