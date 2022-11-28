using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedAssistant.Controllers
{
    [Authorize(Roles = "Moderator,User,Admin")]
    public class VaccinationController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVaccinationService vaccinationService;

        public VaccinationController(IVaccinationService vaccinationService, IMapper mapper)
        {
            this.mapper = mapper;
            this.vaccinationService = vaccinationService;           
        }


        public async Task<IActionResult> VaccinationViewAsync()
        {
            List<VaccinationModel> vaccinationModels = new();
            var emailAddress =   HttpContext.User.Identity.Name.ToString();

            var Dtos = await vaccinationService.GetVaccinationsbyUserEmailAsync(emailAddress);

            if (Dtos != null)
            {

                foreach (var n in Dtos)
                {
                    vaccinationModels.Add(mapper.Map<VaccinationModel>(n));
                }
                 
                return View("VaccinationView", vaccinationModels);
            }
            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult> AddVaccinationAsync()
        {

            var model = new CreateVaccinationModel();

            var vaccinationTypes = await vaccinationService.AddvaccinationTypesAsync();

            model.Types = vaccinationTypes.Select(dto => new SelectListItem(dto.Type,dto.Id.ToString())).ToList();

            return View(model);
             
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccinationAsync(VaccinationModel vaccinationModel)
        {
            if (ModelState.IsValid)
            {
                var emailAddress = HttpContext.User.Identity.Name.ToString();

                var userid = vaccinationService.GetUserIdByEmailAdressAsync(emailAddress);

                vaccinationModel.UserId = await userid;

                var entity = await vaccinationService.CreateVaccinationAsync(mapper.Map<VaccinationDTO>(vaccinationModel));
                  
                if (entity > 0)
                {
                    return RedirectToAction("VaccinationView", "Vaccination");
                }

            }
            return BadRequest();

        }



        [HttpGet]
        public async Task<IActionResult> EditVaccinationAsync(int id)
        {

            var model = await vaccinationService.GetVaccinationByIdAsync(id);

            var vaccinationTypes = await vaccinationService.AddvaccinationTypesAsync();

            model.Types = vaccinationTypes.Select(dto => new SelectListItem(dto.Type, dto.Id.ToString())).ToList();

            return View(mapper.Map<CreateVaccinationModel>(model));
             
        }

        [HttpPost]
        public async Task<IActionResult> EditVaccinationAsync(VaccinationModel vaccinationModel)
        {
            if (ModelState.IsValid)
            {
                var emailAddress = HttpContext.User.Identity.Name.ToString();

                var userid = vaccinationService.GetUserIdByEmailAdressAsync(emailAddress);

                vaccinationModel.UserId = await userid;

                var entity = await vaccinationService.UpdateVaccinationAsync(mapper.Map<VaccinationDTO>(vaccinationModel));
                 
                if (entity > 0)
                {
                    return RedirectToAction("VaccinationView", "Vaccination");
                }

            }
            return BadRequest();

        }


        [HttpGet]
        public async Task<IActionResult> RemoveVaccinationAsync(int id)
        { 
             
            var Dto = await vaccinationService.GetVaccinationByIdAsync(id);

            if (Dto != null)
            {  
                return View("RemoveVaccination", mapper.Map<VaccinationModel>(Dto));
            }
            return NotFound();
             
        }


        [HttpPost]
        public async Task<IActionResult> RemoveVaccinationAsync(VaccinationModel vaccinationModel)
        {
             
            if (vaccinationModel.Id != 0)
            {

                if (await vaccinationService.RemoveVaccinationAsync(vaccinationModel.Id) > 0)
                {  
                     return RedirectToAction("VaccinationView", "Vaccination");
                }
                else
                    return NotFound();
            }
            return NotFound();
             
        }
         
    }
}
