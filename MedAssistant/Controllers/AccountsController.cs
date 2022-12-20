using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace MedAssistant.Controllers
{
 
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;

        public AccountsController(IAccountService accountService,
            IMapper mapper,
            IUserService userService,
            IRoleService roleService,
            IEmailService emailService)
        {
            this.emailService = emailService;
            this.accountService = accountService;
            this.mapper = mapper;
            this.userService = userService;
            this.roleService = roleService;
        }


        [HttpGet]
        public IActionResult Registration()
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
        public async Task<IActionResult> RegistrationAsync(RegistrationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var email = model.Login;

                    if (accountService.IsEmailExist(email) == false)
                    {
                        var entity = await accountService.CreateAccountAsync(mapper.Map<AccountDTO>(model));

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
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
            

        }


        [HttpGet]
        public IActionResult Authentication()
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
        public async Task<IActionResult> AuthenticationAsync(AuthenticationModel model)
        {
            try
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
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            }
           
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return BadRequest();
            }
            
        }
         
        private async Task Authenticate(string email)
        {
            try
            {
                var accountId = await accountService.GetIdAccountByEmailAsync(email);

                var UserValuesWithIncludes = await userService.GetUsersByAccountId(accountId);

                var claims = new List<Claim>()
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, UserValuesWithIncludes.AccountLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserValuesWithIncludes.RoleName) };

                if (claims != null)
                {
                    var identity = new ClaimsIdentity(claims,
                        "ApplicationCookie",
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}"); 
            }
            
        }


        [AllowAnonymous]
        [HttpGet]
        public  ActionResult  IsLoggedIn()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
                {
                    return Ok(true);
                }
                else return Ok(false);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }

            
        
        }

        [HttpGet]
        public async Task<IActionResult> UserLoginPreview()
        {
            try
            {
                if (User.Identities.Any(identity => identity.IsAuthenticated))
                {

                    var accountEmail = User.Identity?.Name;

                    if (string.IsNullOrEmpty(accountEmail))
                    {
                        return NotFound();
                    }

                    var accountId = await accountService.GetIdAccountByEmailAsync(accountEmail);

                    var user = mapper.Map<UserShortDataModel>(await userService.GetUsersByAccountId(accountId));

                    return View(user);
                }
                return View();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserDataAsync()
        {
            try
            {
                var accountEmail = User.Identity?.Name; 
                if (string.IsNullOrEmpty(accountEmail))
                {
                    return BadRequest();
                } 
                var accountId = await accountService.GetIdAccountByEmailAsync(accountEmail); 
                var user = mapper.Map<UserShortDataModel>(await userService.GetUsersByAccountId(accountId)); 
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
             
        }

        [HttpGet]
        public IActionResult ForgottenPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }

        }


        [HttpGet]
        public  IActionResult  ResetPassword(EmailRecoveryPasswordModel emailRecoveryPasswordModel)
        {
            try
            {
                var path = "https://" + HttpContext.Request.Host.Value;
                path += "/Accounts/ResetPasswordForm";

                emailService.SendEmailForRecoveryPassword(emailRecoveryPasswordModel.Login, path);
                 
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpGet]
        public  IActionResult  ResetPasswordForm()
        {
            try
            {
                return View("ResetPassword");
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(RegistrationModel registrationModel)
        {
            try
            {
                if (registrationModel != null)
                {
                    var result = await accountService.UpdateUserPasswordAsync(mapper.Map<AccountDTO>(registrationModel));
                    return RedirectToAction("Authentication", "Accounts");
                }
                else return BadRequest();


            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public  IActionResult ChangePassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (HttpContext.User.Identity.Name == changePasswordModel.Login)
                    {
                        var isPasswordCorrect = await accountService.CheckUserPassword(mapper.Map<AccountDTO>(changePasswordModel));
                        if (isPasswordCorrect == true)
                        {
                            AccountDTO accountDTO = new();
                            accountDTO.Login = changePasswordModel.Login;
                            accountDTO.Password = changePasswordModel.Newpassword; 
                            var result = await accountService.UpdateUserPasswordAsync(accountDTO);
                            return RedirectToAction("Authentication", "Accounts");
                        }
                        else return BadRequest();
                    } 
                    else return BadRequest();
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }


    }
}
