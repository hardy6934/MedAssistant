using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.Controllers
{
    [Authorize(Roles = "Moderator,User,Admin")]
    public class VaccinationTypeController : Controller
    {
        private readonly IVaccinationTypeService vaccinationTypeService;
        private readonly IMapper mapper;

        public VaccinationTypeController( IVaccinationTypeService vaccinationTypeService, IMapper mapper) { 
        
            this.vaccinationTypeService = vaccinationTypeService;
            this.mapper = mapper;

        }


        [HttpGet]
        [Authorize(Roles = "Moderator,User,Admin")]
        public async Task<IActionResult> VaccinationTypeViewAsync()
        {
            try
            {
                var dTOs = await vaccinationTypeService.GetAllVaccinationTypes();
                if (dTOs != null)
                {
                    var models = dTOs.Select(x => mapper.Map<VaccinationTypeModel>(x)).ToList();
                    return View("VaccinationTypeView", models);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            } 
        }

        [Authorize(Roles = "Moderator,User,Admin")]
        [HttpGet]
        public IActionResult AddVaccinationType()
        {
            try
            { 
                return View("AddVaccinationType"); 
            }
            catch (Exception ex)
            { 
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            }

        }


        [HttpPost]
        [Authorize(Roles = "Moderator,User,Admin")]
        public async Task<IActionResult> AddVaccinationTypeAsync(VaccinationTypeModel vaccinationTypeModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await vaccinationTypeService.AddVaccinationTypeAsync(mapper.Map<VaccinationTypeDTO>(vaccinationTypeModel));
                    if (entity > 0)
                    {
                        return RedirectToAction("VaccinationTypeView", "VaccinationType");
                    }

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


        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateVaccinationTypeAsync(int id)
        {
            try
            { 
                var entity = mapper.Map<VaccinationTypeModel>(await vaccinationTypeService.GetVaccinationTypeByIdAsync(id));
                if (entity != null)
                {
                    return View("UpdateVaccinationType", entity);
                }
                else
                {
                    return NotFound();
                } 
            }
            catch (Exception ex)
            { 
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }

        }


        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateVaccinationTypeAsync(VaccinationTypeModel vaccinationTypeModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await vaccinationTypeService.UpdateVaccinationTypeAsync(mapper.Map<VaccinationTypeDTO>(vaccinationTypeModel));
                    if (entity > 0)
                    {
                        return RedirectToAction("VaccinationTypeView", "VaccinationType");
                    } 
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


        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public async Task<IActionResult> RemoveVaccinationTypeAsync(int id)
        {
            try
            { 
                var entity = mapper.Map<VaccinationTypeModel>(await vaccinationTypeService.GetVaccinationTypeByIdAsync(id));
                if (entity != null)
                {
                    return View("RemoveVaccinationType", entity);
                }
                else
                {
                    return NotFound();
                } 
            }
            catch (Exception ex)
            { 
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }

        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveVaccinationTypeAsync(VaccinationTypeModel vaccinationTypeModel)
        {
            try
            {
                if (vaccinationTypeModel.Id != 0)
                {

                    if (await vaccinationTypeService.RemoveVaccinationTypeAsync(mapper.Map<VaccinationTypeDTO>(vaccinationTypeModel)) > 0)
                    {
                        return RedirectToAction("VaccinationTypeView", "VaccinationType");
                    }
                    else
                        return NotFound();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }

}

    }
}
