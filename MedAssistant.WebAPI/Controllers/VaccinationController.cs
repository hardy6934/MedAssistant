using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Models;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IVaccinationService vaccinationService;

        public VaccinationController(IMapper mapper, IUserService userService, IVaccinationService vaccinationService)
        {

            this.mapper = mapper;
            this.userService = userService;
            this.vaccinationService = vaccinationService;

        }

        ///<summary>
        /// Get all vaccinations for concret User by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns></returns>
        [HttpGet("GetVaccinations")]
        [ProducesResponseType(typeof(List<VaccinationsResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVaccinationsByEmail(string email)
        { 
            var Dtos = await vaccinationService.GetVaccinationsbyUserEmailAsync(email);

            if (Dtos != null)
            {
                var answer = Dtos.Select(x => mapper.Map<VaccinationsResponseModel>(x)).ToList();
                return Ok(answer);
            }
            return NotFound();
              
        }

        ///<summary>
        /// Get concret vaccination with Id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<VaccinationsResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVaccinationById(int id)
        {
            var Dto = await vaccinationService.GetVaccinationByIdAsync(id);

            if (Dto != null)
            {
                return Ok(mapper.Map<VaccinationsResponseModel>(Dto));
            }
            return NotFound();

        }
    }
} 