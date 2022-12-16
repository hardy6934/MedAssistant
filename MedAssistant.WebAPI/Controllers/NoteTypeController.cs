using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly INoteTypeService noteTypeService;

        public NoteTypeController(IMapper mapper, INoteTypeService noteTypeService)
        {
            this.noteTypeService = noteTypeService;
            this.mapper = mapper;
        }
         

        /// <summary>
        /// Add new note type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpPost("CreateNoteType")]
        [ProducesResponseType(typeof(NoteTypeRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNoteType([FromBody] NoteTypeRequestModel noteTypeRequestModel)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (noteTypeRequestModel != null)
                    {
                        int entity = await noteTypeService.AddNoteTypeAsync(mapper.Map<NoteTypeDTO>(noteTypeRequestModel));

                        if (entity > 0)
                        {
                            return Ok(noteTypeRequestModel);
                        }
                        else return BadRequest();
                    }
                    else return BadRequest();
                }
                else
                    return StatusCode(403);


            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update new note type 
        /// </summary>
        /// <returns>204</returns>
        [HttpPut("UpdateNoteType")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNoteType([FromQuery] int id, [FromBody] NoteTypeRequestModel noteTypeRequestModel)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (noteTypeRequestModel != null)
                    {
                        var model = mapper.Map<NoteTypeDTO>(noteTypeRequestModel);
                        model.Id = id;
                        await noteTypeService.UpdateNoteTypeAsync(model);
                    }
                    return StatusCode(204);
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete note type 
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpDelete("DeleteNoteType")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNoteType(int id)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (id != 0)
                    {
                        var model = await noteTypeService.GetNoteTypeByIdAsync(id);
                        await noteTypeService.RemoveNoteTypeAsync(model);
                    } 
                    return StatusCode(204);
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Get note type by id
        /// </summary>
        /// <returns>OK(model)</returns>
        [HttpGet("GetTypeById")]
        [ProducesResponseType(typeof(NoteTypeResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTypeById(int id)
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    if (id != 0)
                    {
                        var model = mapper.Map<NoteTypeResponseModel>(await noteTypeService.GetNoteTypeByIdAsync(id));
                        return Ok(model);
                    } 
                    return NotFound();
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }


        /// <summary>
        /// Get all note types 
        /// </summary>
        /// <returns>OK(models)</returns>
        [HttpGet("GetAllTypes")]
        [ProducesResponseType(typeof(List<NoteTypeResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTypes()
        {
            try
            {
                var claims = User.Identity as ClaimsIdentity;
                var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
                if (role == "Moderator" || role == "Admin")
                {
                    var models = await noteTypeService.GetAllNoteTypes();
                    return Ok(models.Select(x => mapper.Map<NoteTypeResponseModel>(x)));
                }
                else
                    return StatusCode(403);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
