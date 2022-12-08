using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.Controllers
{  
    
    [Authorize(Roles = "Moderator,Admin")]
        public class DoctorTypeController : Controller
        {
            private readonly IDoctorTypeService doctorTypeService;
            private readonly IMapper mapper;

            public DoctorTypeController(IDoctorTypeService doctorTypeService, IMapper mapper)
            {

                this.doctorTypeService = doctorTypeService;
                this.mapper = mapper;

            }


            [HttpGet]
            [Authorize(Roles = "Moderator,Admin")]
            public async Task<IActionResult> DoctorTypeViewAsync()
            {
                try
                {
                    var dTOs = await doctorTypeService.GetAllDoctorTypes();
                    if (dTOs != null)
                    {
                        var models = dTOs.Select(x => mapper.Map<DoctorTypeModel>(x)).ToList();
                        return View("DoctorTypeView", models);
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
            public IActionResult AddDoctorType()
            {
                try
                {
                    return View("AddDoctorType");
                }
                catch (Exception ex)
                {
                    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                    return NotFound();
                }
            }


            [HttpPost]
            [Authorize(Roles = "Moderator,Admin")]
            public async Task<IActionResult> AddDoctorTypeAsync(DoctorTypeModel doctorTypeModel)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var entity = await doctorTypeService.AddDoctorTypeAsync(mapper.Map<DoctorTypeDTO>(doctorTypeModel));
                        if (entity > 0)
                        {
                            return RedirectToAction("DoctorTypeView", "DoctorType");
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
            public async Task<IActionResult> UpdateDoctorTypeAsync(int id)
            {
                try
                {
                    var entity = mapper.Map<DoctorTypeModel>(await doctorTypeService.GetDoctorTypeByIdAsync(id));
                    if (entity != null)
                    {
                        return View("UpdateDoctorType", entity);
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
            public async Task<IActionResult> UpdateDoctorTypeAsync(DoctorTypeModel doctorTypeModel)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var entity = await doctorTypeService.UpdateDoctorTypeAsync(mapper.Map<DoctorTypeDTO>(doctorTypeModel));
                        if (entity > 0)
                        {
                            return RedirectToAction("DoctorTypeView", "DoctorType");
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
            public async Task<IActionResult> RemoveDoctorTypeAsync(int id)
            {
                try
                {
                    var entity = mapper.Map<DoctorTypeModel>(await doctorTypeService.GetDoctorTypeByIdAsync(id));
                    if (entity != null)
                    {
                        return View("RemoveDoctorType", entity);
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
            public async Task<IActionResult> RemoveDoctorTypeAsync(DoctorTypeModel doctorTypeModel)
            {
                try
                {
                    if (doctorTypeModel.Id != 0)
                    {

                        if (await doctorTypeService.RemoveDoctorTypeAsync(mapper.Map<DoctorTypeDTO>(doctorTypeModel)) > 0)
                        {
                            return RedirectToAction("DoctorTypeView", "DoctorType");
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
