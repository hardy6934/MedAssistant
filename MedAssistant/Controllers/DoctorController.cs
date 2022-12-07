using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;

namespace MedAssistant.Controllers
{

    [Authorize(Roles = "Moderator,User,Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper )
        {
            this.doctorService = doctorService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DoctorViewAsync()
        {
            try {
                var email = HttpContext.User.Identity.Name;
                var doctors = await doctorService.GetAllDoctorsByEmailAsync(email);
                return View("DoctorView", doctors.Select(doctor => mapper.Map<DoctorModel>(doctor)).ToList());
            }
            catch (Exception ex) {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddDoctorAsync()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var types = await doctorService.GetAllDoctorTypesAsync();
                var medicalInstitutions = await doctorService.GetAllMedicalInstitutionsAsync();

                CreateDoctorModel model = new();
                model.ListOfMedicalInstitutionName = medicalInstitutions.Select(dto => new SelectListItem(dto.Name, dto.Id.ToString())).ToList();
                model.ListOfDoctorTypes = types.Select(dto => new SelectListItem(dto.Type, dto.Id.ToString())).ToList();

                return View("AddDoctor", model);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(DoctorModel doctorModel)
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var userId = await doctorService.GetUserIdByEmailAdressAsync(email);
                doctorModel.UserId = userId;


                var entity = await doctorService.AddDoctorAsync(mapper.Map<DoctorDTO>(doctorModel));

                if (entity > 0)
                {
                    return RedirectToAction("DoctorView", "Doctor");
                }
                return BadRequest();
                
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDoctorAsync(int id)
        {
            try
            {

                var email = HttpContext.User.Identity.Name;
                var types = await doctorService.GetAllDoctorTypesAsync();
                var medicalInstitutions = await doctorService.GetAllMedicalInstitutionsAsync();

                var model = mapper.Map<CreateDoctorModel>(await doctorService.GetDoctorByIdAsync(id));
                if (model !=null)
                {
                    model.ListOfMedicalInstitutionName = medicalInstitutions.Select(dto => new SelectListItem(dto.Name, dto.Id.ToString())).ToList();
                    model.ListOfDoctorTypes = types.Select(dto => new SelectListItem(dto.Type, dto.Id.ToString())).ToList();

                    return View("UpdateDoctor", model);
                } 
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoctorAsync(DoctorModel doctorModel)
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var userId = await doctorService.GetUserIdByEmailAdressAsync(email);
                doctorModel.UserId = userId;


                var entity = await doctorService.UpdateDoctorAsync(mapper.Map<DoctorDTO>(doctorModel));
                if (entity > 0)
                {
                    return RedirectToAction("DoctorView", "Doctor");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveDoctorAsync(int id)
        {
            try
            {
                var model = mapper.Map<DoctorModel>(await doctorService.GetDoctorByIdAsync(id));
                if (model != null)
                {
                    return View("RemoveDoctor", model);
                }
                return NotFound();
                
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDoctorAsync(DoctorModel doctorModel)
        {
            try
            { 
                var entity = await doctorService.RemoveDoctorAsync(mapper.Map<DoctorDTO>(doctorModel));
                if(entity>0)
                {
                    return RedirectToAction("DoctorView", "Doctor");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }
    }
}
