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
    [Authorize(Roles = "User,Moderator,Admin")]
    public class NoteController : Controller
    {
        private readonly IMapper mapper;
        private readonly INoteService noteService;

        public NoteController(IMapper mapper, INoteService noteService) { 
            this.mapper = mapper;
            this.noteService = noteService; 
        }

        public async Task<IActionResult> NoteViewAsync()
        {
            try
            {
                var emailAddress = HttpContext.User.Identity.Name.ToString();

                var Dtos = await noteService.GetNotesbyUserEmailAsync(emailAddress);

                if (Dtos != null)
                {
                    var models = Dtos.Select(x => mapper.Map<NoteModel>(x)).ToList();

                    return View("NoteView", models);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }


        }


        [HttpGet]
        public async Task<IActionResult> AddNoteAsync()
        {
            try
            {
                var model = new CreateNoteModel();

                var emailAddress = HttpContext.User.Identity.Name.ToString();

                var doctors = await noteService.GetAllDoctorsForUserByEmailAsync(emailAddress);
                var notetypes = await noteService.GetAllNoteTypesAsync();

                model.ListOfDoctors = doctors.Select(dto => new SelectListItem(dto.FullName, dto.Id.ToString())).ToList();
                model.ListOfNoteTypes = notetypes.Select(dto => new SelectListItem(dto.Type, dto.Id.ToString())).ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddNoteAsync(NoteModel noteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emailAddress = HttpContext.User.Identity.Name.ToString();

                    var userid = await noteService.GetUserIdByEmailAdressAsync(emailAddress);

                    noteModel.UserId = userid;

                    var entity = await noteService.CreateNoteAsync(mapper.Map<NoteDTO>(noteModel));

                    if (entity > 0)
                    {
                        return RedirectToAction("NoteView", "Note");
                    }

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
        public async Task<IActionResult> UpdateNoteAsync(int id)
        {
            try
            {
                var model = mapper.Map<CreateNoteModel>(await noteService.GetNoteByIdAsync(id));

                var emailAddress = HttpContext.User.Identity.Name.ToString();

                var doctors = await noteService.GetAllDoctorsForUserByEmailAsync(emailAddress);
                var notetypes = await noteService.GetAllNoteTypesAsync();

                model.ListOfDoctors = doctors.Select(dto => new SelectListItem(dto.FullName, dto.Id.ToString())).ToList();
                model.ListOfNoteTypes = notetypes.Select(dto => new SelectListItem(dto.Type, dto.Id.ToString())).ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            } 
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNoteAsync(NoteModel noteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emailAddress = HttpContext.User.Identity.Name.ToString();

                    var userid = await noteService.GetUserIdByEmailAdressAsync(emailAddress);

                    noteModel.UserId = userid;

                    var entity = await noteService.UpdateNoteAsync(mapper.Map<NoteDTO>(noteModel));

                    if (entity > 0)
                    {
                        return RedirectToAction("NoteView", "Note");
                    }

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
        public async Task<IActionResult> RemoveNoteAsync(int id)
        {
            try
            {
                var dto = await noteService.GetNoteByIdAsync(id);
                var model = mapper.Map<NoteModel>(dto);

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveNoteAsync(NoteModel noteModel)
        {
            try
            {
                if (noteModel.Id != 0)
                {
                    var entity = await noteService.RemoveNoteAsync(noteModel.Id);

                    if (entity > 0)
                    {
                        return RedirectToAction("NoteView", "Note");
                    }
                    return BadRequest();
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
