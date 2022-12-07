using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedAssistant.Controllers
{
    [Authorize(Roles = "Moderator,Admin")]
    public class NoteTypeController : Controller
    {

        private readonly INoteTypeService noteTypeService;
        private readonly IMapper mapper;

        public NoteTypeController(INoteTypeService noteTypeService, IMapper mapper)
        {

            this.noteTypeService = noteTypeService;
            this.mapper = mapper;

        }


        [HttpGet]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> NoteTypeViewAsync()
        {
            //try { 

            var dTOs = await noteTypeService.GetAllNoteTypes();
            if (dTOs != null)
            {
                var models = dTOs.Select(x => mapper.Map<NoteTypeModel>(x)).ToList();
                return View("NoteTypeView", models);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public IActionResult AddNoteType()
        {
            //try
            //{

                return View("AddNoteType");

            //}
            //catch (Exception ex) {

            //    Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
            //    return BadRequest();
            //}

        }


        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> AddNoteTypeAsync(NoteTypeModel noteTypeModel)
        {
            //try { 
                if (ModelState.IsValid)
                {
                    var entity = await noteTypeService.AddNoteTypeAsync(mapper.Map<NoteTypeDTO>(noteTypeModel));
                    if (entity > 0)
                    {
                        return RedirectToAction("NoteTypeView", "NoteType");
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
        public async Task<IActionResult> UpdateNoteTypeAsync(int id)
        {
            //try
            //{

                var entity = mapper.Map<NoteTypeModel>(await noteTypeService.GetNoteTypeByIdAsync(id));
                if (entity != null)
                {
                    return View("UpdateNoteType", entity);
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
        public async Task<IActionResult> UpdateNoteTypeAsync(NoteTypeModel noteTypeModel)
        {
            //try
            //{ 
                if (ModelState.IsValid)
                {
                    var entity = await noteTypeService.UpdateNoteTypeAsync(mapper.Map<NoteTypeDTO>(noteTypeModel));
                    if (entity > 0)
                    {
                        return RedirectToAction("NoteTypeView", "NoteType");
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
        public async Task<IActionResult> RemoveNoteTypeAsync(int id)
        {
            //try {

                var entity = mapper.Map<NoteTypeModel>(await noteTypeService.GetNoteTypeByIdAsync(id));
                if (entity != null)
                {
                    return View("RemoveNoteType", entity);
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
        public async Task<IActionResult> RemoveNoteTypeAsync(NoteTypeModel noteTypeModel)
        {
            //try
            //{
                if (noteTypeModel.Id != 0)
                {

                    if (await noteTypeService.RemoveNoteTypeAsync(mapper.Map<NoteTypeDTO>(noteTypeModel)) > 0)
                    {
                        return RedirectToAction("NoteTypeView", "NoteType");
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
