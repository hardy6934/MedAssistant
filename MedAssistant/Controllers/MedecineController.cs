using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedAssistant.Helpers.HelpersModels;
using Serilog;

namespace MedAssistant.Controllers
{
    public class MedecineController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMedicineService medecineService;

        public MedecineController(IMedicineService medecineService, IMapper mapper)
        {
            this.mapper = mapper;
            this.medecineService = medecineService;
        }

        [Authorize(Roles = "Moderator,User,Admin")]
        public async Task<IActionResult> MedecinesViewAsync(int id)
        {
            try
            {
                var Dtos = await medecineService.GetAllMedecinesFromDataBaseAsync();

                if (Dtos != null)
                { 
                    var pageSize = 100;

                    var models = Dtos.Select(x => mapper.Map<MedicineModel>(x)).ToList();
                     
                    PagingInfo pagingInfo = new();
                    pagingInfo.CurrentPage = id == 0 ? 1 : id;
                    pagingInfo.TotalItems = models.Count();
                    pagingInfo.ItemsPerPage = pageSize;

                    var skip = pageSize * (Convert.ToInt32(id) - 1);
                    MedecineListForPagination model = new();
                    model.PagingInfo = pagingInfo;
                    model.MedicineModel = models.Skip(skip).Take(pageSize).ToList();
                     
                    return View("MedecineView", model);
                }
                else
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
        public IActionResult AddMedecine()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            } 
        }


        [HttpPost]
        [Authorize(Roles = "Moderator,User,Admin")]
        public async Task<IActionResult> AddMedecineAsync(MedicineModel medicineModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await medecineService.AddMedecineAsync(mapper.Map<MedicineDTO>(medicineModel));
                    if (entity > 0)
                    {
                        return RedirectToAction("MedecinesView", "Medecine");
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
        public async Task<IActionResult> UpdateMedecineAsync(int id)
        {
            try
            {
                var entity = mapper.Map<MedicineModel>(await medecineService.GetMedecineByIdAsync(id));
                if (entity != null)
                {
                    return View("EditMedecine", entity);
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
        public async Task<IActionResult> UpdateMedecineAsync(MedicineModel medicineModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await medecineService.UpdateMedecineAsync(mapper.Map<MedicineDTO>(medicineModel));
                    if (entity > 0)
                    {
                        return RedirectToAction("MedecinesView", "Medecine");
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
        public async Task<IActionResult> RemoveMedecineAsync(int id)
        {
            try
            {
                var entity = mapper.Map<MedicineModel>(await medecineService.GetMedecineByIdAsync(id));
                if (entity != null)
                {
                    return View("RemoveMedecine", entity);
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
        public async Task<IActionResult> RemoveMedecineAsync(MedicineModel medicineModel)
        {
            try
            {
                if (medicineModel.Id != 0)
                { 
                    if (await medecineService.RemoveMedecineAsync(mapper.Map<MedicineDTO>(medicineModel)) > 0)
                    {
                        return RedirectToAction("MedecinesView", "Medecine");
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
