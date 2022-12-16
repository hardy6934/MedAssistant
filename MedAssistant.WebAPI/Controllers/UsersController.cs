using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.WebAPI.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;


        public UsersController(IUserService userService, IAccountService accountService, IMapper mapper)
        {
            this.userService = userService;
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpGet("GetUser")]
        [Authorize]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(string email)
        {
            var account = await accountService.GetIdAccountByEmailAsync(email);
            var user = await userService.GetUsersByAccountId(account);

            if (user != null)
            {
                return Ok(mapper.Map<UserResponseModel>(user));
            }

            return NotFound(); 
        }


        [HttpGet("GetAllUsers")]
        [Authorize]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers()
        {
            var claims = User.Identity as ClaimsIdentity;
            var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
            if (role == "Admin")
            {
                var users = await userService.GetAllUsers();

                if (users != null)
                {
                    return Ok(users.Select(x=>mapper.Map<UserResponseModel>(x)));
                }

                return NotFound();
            }
            else
                return StatusCode(403);
        }

        [HttpGet("ChangeUserRoleByEmail")]
        [Authorize]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeUserRoleByEmail([FromQuery] string email, string newRole)
        {
            var claims = User.Identity as ClaimsIdentity;
            var role = claims.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).FirstOrDefault();
            if (role == "Admin")
            {
                var result = await userService.ChangeUserRoleByEmail(email, newRole);

                if (result > 0)
                {
                    return StatusCode(204);
                }

                return BadRequest();
            }
            else
                return StatusCode(403);
        }
    }
}
