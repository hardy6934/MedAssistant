using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using Serilog.Events;
using System.Threading;

namespace MedAssistant.Controllers
{
    [Authorize(Roles = "Moderator,User,Admin")]
    public class PrescriptionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPrescriptionService prescriptionService;
        private readonly IMedicineService medicineService;
         
        public PrescriptionController(IPrescriptionService prescriptionService, IMapper mapper, IMedicineService medicineService)
        {
            this.mapper = mapper;
            this.prescriptionService = prescriptionService;
             this.medicineService = medicineService;
        }


        public async Task<IActionResult> PrescriptionViewAsync()
        {
            try
            {
                var emailAddress = HttpContext.User.Identity.Name.ToString();

                var Dtos = await prescriptionService.GetPrescriptionsbyUserEmailAsync(emailAddress);

                if (Dtos != null)
                {


                    return View("PrescriptionView", Dtos.Select(x => mapper.Map<PrescriptionModel>(x)).ToList());
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult> AutocompleteSearchMedecinesAsync(string term)
        {
            try
            {
                var medecines = await prescriptionService.GetAllMedicinesAsync();

                var models = medecines.Where(med => med.Name.Contains(term))
                                .Select(med => new { value = med.Name })
                                .Distinct();

                return Ok(models);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddPrescriptionAsync()
        {
            try
            { 
                var model = new CreatePrescriptionModel();

                var vaccinationTypes = await prescriptionService.GetAllMedicinesAsync();
                 

                model.Medicine = vaccinationTypes.Select(dto => new SelectListItem(dto.Name, dto.Id.ToString())).AsParallel().ToList();

                return View(model);
            }
            catch (Exception ex) {

                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            } 
        }


        [HttpPost]
        public async Task<IActionResult> AddPrescriptionAsync(PrescriptionModel prescriptionModel)
        {
            try 
            {  
                var emailAddress = HttpContext.User.Identity.Name.ToString(); 
                var userid = await prescriptionService.GetUserIdByEmailAdressAsync(emailAddress);
                var MedecineId = prescriptionService.FindMedecineIdByName(prescriptionModel.MedicineName);
                prescriptionModel.UserId =   userid;
                prescriptionModel.MedicineId = MedecineId;
                var entity = await prescriptionService.CreatePrescriptionAsync(mapper.Map<PrescriptionDTO>(prescriptionModel)); 
                if (entity > 0)
                {
                    return RedirectToAction("PrescriptionView", "Prescription");
                }  
                return BadRequest(); 
            }
            catch (Exception ex)
            { 
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            }

        }



        [HttpGet]
        public async Task<IActionResult> EditPrescriptionAsync(int id)
        {
            try
            { 
                var model = await prescriptionService.GetPrescriptionByIdAsync(id); 
                 
                var ent = mapper.Map<CreatePrescriptionModel>(model); 
                return View(ent); 
            }
            catch (Exception ex)
            { 
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            } 
        }        



        [HttpPost]
        public async Task<IActionResult> EditPrescriptionAsync(PrescriptionModel prescriptionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emailAddress = HttpContext.User.Identity.Name.ToString(); 
                    var userid = await prescriptionService.GetUserIdByEmailAdressAsync(emailAddress); 
                    prescriptionModel.UserId = userid;
                    prescriptionModel.MedicineId = prescriptionService.FindMedecineIdByName(prescriptionModel.MedicineName);
                    var entity = await prescriptionService.UpdatePrescriptionAsync(mapper.Map<PrescriptionDTO>(prescriptionModel)); 
                    if (entity > 0)
                    {
                        return RedirectToAction("PrescriptionView", "Prescription");
                    } 
                }
                return BadRequest(); 
            }
            catch (Exception ex)
            { 
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            } 
        }

        


        [HttpGet]
        public async Task<IActionResult> RemovePrescriptionAsync(int id)
        {
            try
            { 
                var Dto = await prescriptionService.GetPrescriptionByIdAsync(id); 
                if (Dto != null)
                {
                    return View("RemovePrescription", mapper.Map<PrescriptionModel>(Dto));
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
        public async Task<IActionResult> RemovePrescriptionAsync(PrescriptionModel prescriptionModel)
        {
            try
            {
                if (prescriptionModel.Id != 0)
                { 
                    if (await prescriptionService.RemovePrescriptionAsync(prescriptionModel.Id) > 0)
                    {
                        return RedirectToAction("PrescriptionView", "Prescription");
                    }
                    else
                        return BadRequest();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            } 
        }



    }
}
