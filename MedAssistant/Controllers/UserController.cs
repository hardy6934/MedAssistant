using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedAssistant.Controllers
{
    [Authorize(Roles = "Moderator,User,Admin")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAccountService accountService;

        public UserController(IMapper mapper,
            IUserService userService, 
            IAccountService accountService)
        {
            _mapper = mapper;
            _userService = userService;
            this.accountService = accountService;
        }

        public async Task<IActionResult> UserView()
        {
            var emailAddress =   HttpContext.User.Identity.Name.ToString();
              
            var idaccount = await accountService.GetIdAccountByEmailAsync(emailAddress);
            
            var model = await _userService.GetUsersByAccountId(idaccount);
          
            if (model != null)
            {
                

                return View(_mapper.Map<UserModel>(model));
            }
            return NotFound();
            
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersAsync(int id)
        {
             
            var model = await _userService.GetUserByIdAsync(id);
            return View(_mapper.Map<UserModel>(model));
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersAsync(UserModel model)
        {
            
            var entity = await _userService.UpdateUserAsync(_mapper.Map<UserDTO>(model));

            if (entity > 0)
            { 
             return RedirectToAction("UserView", "User");
            }
            return BadRequest();

        }
    }
}
