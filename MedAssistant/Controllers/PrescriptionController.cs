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

        public PrescriptionController(IPrescriptionService prescriptionService, IMapper mapper)
        {
            this.mapper = mapper;
            this.prescriptionService = prescriptionService;
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
                prescriptionModel.UserId =   userid; 
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
                var Medicines = await prescriptionService.GetAllMedicinesAsync(); 
                model.Names = Medicines.Select(dto => new SelectListItem(dto.Name, dto.Id.ToString())).ToList(); 
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
                    var userid = prescriptionService.GetUserIdByEmailAdressAsync(emailAddress); 
                    prescriptionModel.UserId = await userid; 
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
