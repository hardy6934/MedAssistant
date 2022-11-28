using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using MedAssistant.WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IJWTUtil jwtutil;
        private readonly IMapper mapper;

        public AccountController(IAccountService accountService,
            IMapper mapper,
            IUserService userService,
            IRoleService roleService,
            IJWTUtil jwtutil)
        {
            this.accountService = accountService;
            this.mapper = mapper;
            this.userService = userService;
            this.roleService = roleService;
            this.jwtutil = jwtutil;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> Create([FromBody] RegAccountRequestModel request)
        {
            try
            {
                if (accountService.IsEmailExist(request.Email) == false && request.Password.Equals(request.PasswordConfirmation))
                {
                    var entity = await accountService.CreateAccountAsync(mapper.Map<AccountDTO>(request));

                    if (entity > 0)
                    {
                        var accountId = await accountService.GetIdAccountByEmailAsync(request.Email);
                        var IdRole = await roleService.FindRoleIdByRoleName("User");
                        var defaultuser = userService.CreateDefaultUserUserAsync(accountId, IdRole);
                        var Userentity = await userService.CreateUserAsync(defaultuser);

                        if (Userentity > 0)
                        {
                            var accountid = await accountService.GetIdAccountByEmailAsync(request.Email);
                            var account = await accountService.GetAccountByIdAsync(accountId);

                            var response = await jwtutil.GenerateTokenAsync(account);

                            return Ok(response);
                        }

                    }
                     
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }
              
        }


        [HttpGet("GetUser")]
        [Authorize(Roles = "Moderator,User,Admin")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
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


        //[HttpPut("EditUsers")]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> EditUsersAsync([FromBody]UserRequestModel userRequestModel)
        //{
        //    var model = await userService.GetUserByIdAsync(userRequestModel.Id);

        //    var entity = await userService.UpdateUserAsync(mapper.Map<UserDTO>(model));

        //    if (entity > 0)
        //    {
        //        return Ok();
        //    }

        //    return BadRequest();

        //}

    }
}
 