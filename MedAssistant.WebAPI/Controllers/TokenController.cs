using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;
using MedAssistant.WebAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly IJWTUtil jwtutil;

        public TokenController(IAccountService accountService,
            IJWTUtil jwtutil,
            IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
            this.jwtutil = jwtutil;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJWTToken([FromBody] LoginAccountRequestModel request)
        {
            try
            {
                var accountid = await accountService.GetIdAccountByEmailAsync(request.Email);
                var account = await accountService.GetAccountByIdAsync(accountid);
                if (account == null)
                {
                    return NotFound(new ErrorModel { Message = "Login is not exist" });
                }
                var isPassCorrect = await accountService.CheckUserPassword(mapper.Map<AccountDTO>(request));

                if(!isPassCorrect)
                {
                    return BadRequest(new ErrorModel { Message = "Password is incorrect"});
                }

                var response = await jwtutil.GenerateTokenAsync(mapper.Map<AccountDTO>(account));
                 

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return StatusCode(500);
            }

        }
    }
}
