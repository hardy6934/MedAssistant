using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Helpers.HelpersModels;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedAssistant.Controllers
{
    public class MedicalInstitutionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMedicalInstitutionService medicalInstitutionService;

        public MedicalInstitutionController(IMedicalInstitutionService medicalInstitutionService, IMapper mapper)
        {
            this.mapper = mapper;
            this.medicalInstitutionService = medicalInstitutionService;
        }

        [Authorize(Roles = "Moderator,User,Admin")]
        public async Task<IActionResult> MedicalInstitutionViewAsync(int id)
        {
            //try
            //{  
            var Dtos = await medicalInstitutionService.GetAllMedicalInstitutionsFromDataBaseAsync();

            if (Dtos != null)
            {

                var pageSize = 100;

                var models = Dtos.Select(x => mapper.Map<MedicalInstitutionModel>(x)).ToList();


                PagingInfo pagingInfo = new PagingInfo();
                pagingInfo.CurrentPage = id == 0 ? 1 : id;
                pagingInfo.TotalItems = models.Count();
                pagingInfo.ItemsPerPage = pageSize;

                var skip = pageSize * (Convert.ToInt32(id) - 1);
                MedicalInstitutuinModelForPagination model = new();
                model.PagingInfo = pagingInfo;
                model.MedicalInstitutionModels = models.Skip(skip).Take(pageSize).ToList();



                return View("MedicalInstitutionVew", model);
            }
            else
                return NotFound();
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return NotFound();
            //}
        }


        [Authorize(Roles = "Moderator,User,Admin")]
        [HttpGet]
        public async Task<IActionResult> AddMedicalInstitutionAsync()
        {
            //try
            //{

            return View("AddMedicalInstitution");

            //}
            //catch (Exception ex) {

            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return BadRequest();
            //}

        }


        [HttpPost]
        [Authorize(Roles = "Moderator,User,Admin")]
        public async Task<IActionResult> AddMedicalInstitutionAsync(MedicalInstitutionModel medicalInstitutionModel)
        {
            //try { 
            if (ModelState.IsValid)
            {
                var entity = await medicalInstitutionService.AddMedicalInstitutionAsync(mapper.Map<MedicalInstitutionDTO>(medicalInstitutionModel));
                if (entity > 0)
                {
                    return RedirectToAction("MedicalInstitutionView", "MedicalInstitution");
                }

                return BadRequest();
            }
            return BadRequest();

            //}
            //catch (Exception ex) {

            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return BadRequest();
            //}

        }


        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateMedicalInstitutionAsync(int id)
        {
            //try
            //{

            var entity = mapper.Map<MedicalInstitutionModel>(await medicalInstitutionService.GetMedicalInstitutionByIdAsync(id));
            if (entity != null)
            {
                return View("UpdateMedicalInstitution", entity);
            }
            else
            {
                return NotFound();
            }

            //}
            //catch (Exception ex)
            //{

            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return NotFound();
            //}

        }


        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateMedicalInstitutionAsync(MedicalInstitutionModel medicalInstitutionModel)
        {
            //try
            //{ 
            if (ModelState.IsValid)
            {
                var entity = await medicalInstitutionService.UpdateMedicalInstitutionAsync(mapper.Map<MedicalInstitutionDTO>(medicalInstitutionModel));
                if (entity > 0)
                {
                    return RedirectToAction("MedicalInstitutionView", "MedicalInstitution");
                }

                return BadRequest();
            }

            return BadRequest();

            //}
            //catch (Exception ex)
            //{

            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return BadRequest();
            //}


        }


        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public async Task<IActionResult> RemoveMedicalInstitutionAsync(int id)
        {
            //try {

            var entity = mapper.Map<MedicalInstitutionModel>(await medicalInstitutionService.GetMedicalInstitutionByIdAsync(id));
            if (entity != null)
            {
                return View("RemoveMedicalInstitution", entity);
            }
            else
            {
                return NotFound();
            }

            //}
            //catch (Exception ex)
            //{

            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return NotFound();
            //}

        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveMedicalInstitutionAsync(MedicalInstitutionModel medicalInstitutionModel)
        {
            //try
            //{
            if (medicalInstitutionModel.Id != 0)
            {

                if (await medicalInstitutionService.RemoveMedicalInstitutionAsync(mapper.Map<MedicalInstitutionDTO>(medicalInstitutionModel)) > 0)
                {
                    return RedirectToAction("MedicalInstitutionView", "MedicalInstitution");
                }
                else
                    return NotFound();
            }
            return NotFound();
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return NotFound();
            //}

        }
    }
}

