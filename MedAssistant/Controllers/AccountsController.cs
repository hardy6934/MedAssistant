using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedAssistant.Controllers
{
 
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public AccountsController(IAccountService accountService,
            IMapper mapper,
            IUserService userService,
            IRoleService roleService)
        {
            this.accountService = accountService;
            this.mapper = mapper;
            this.userService = userService;
            this.roleService = roleService;
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationModel model)
        {

            if (ModelState.IsValid)
            { 
                var email = model.Login;
                 
                if (accountService.IsEmailExist(email) == false)
                {  
                    var entity =  await accountService.CreateAccountAsync(mapper.Map<AccountDTO>(model));

                    if (entity > 0)
                    {
                        var accountId = await accountService.GetIdAccountByEmailAsync(email);
                        var IdRole = await roleService.FindRoleIdByRoleName("User");
                        var defaultuser = userService.CreateDefaultUserUserAsync(accountId, IdRole);
                        var Userentity = await userService.CreateUserAsync(defaultuser);

                        if (Userentity > 0)
                        {
                            await Authenticate(email);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View(model);

        }


        [HttpGet]
        public IActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticationAsync(AuthenticationModel model)
        {
            var isPasswordCorrect = await accountService.CheckUserPassword(mapper.Map<AccountDTO>(model));
            if (isPasswordCorrect)
            {
                await Authenticate(model.Login);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Logout( )
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



        private async Task Authenticate(string email)
        {
            var accountId = await accountService.GetIdAccountByEmailAsync(email);

            var UserValuesWithIncludes = await userService.GetUsersByAccountId(accountId);

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, UserValuesWithIncludes.AccountLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserValuesWithIncludes.RoleName)
            };

            if (claims != null)
            { 
                var identity = new ClaimsIdentity(claims,
                    "ApplicationCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType
                );
          
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public  ActionResult  IsLoggedIn()
        {

            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return Ok(true);    
            }
            else return Ok(false);
        
        }

        [HttpGet]
        public async Task<IActionResult> UserLoginPreview()
        {
            if (User.Identities.Any(identity => identity.IsAuthenticated))
            {  

                var accountEmail = User.Identity?.Name;

                if (string.IsNullOrEmpty(accountEmail))
                {
                    return BadRequest();
                }

                var accountId = await accountService.GetIdAccountByEmailAsync(accountEmail);

                var user = mapper.Map<UserShortDataModel>(await userService.GetUsersByAccountId(accountId));

                return View(user);
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserDataAsync()
        {
            var accountEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(accountEmail))
            {
                return BadRequest();
            }

            var accountId = await accountService.GetIdAccountByEmailAsync(accountEmail);
             
            var user = mapper.Map<UserShortDataModel>( await userService.GetUsersByAccountId(accountId));

             
                return Ok(user);
            
        }


    }
}
