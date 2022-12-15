using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Responses;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MedAssistant.WebAPI.Utils
{
    public class JWTUtilSha256 : IJWTUtil
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public JWTUtilSha256(IConfiguration configuration, IUserService userService)
        { 
            this.configuration = configuration;
            this.userService = userService;
        }
         
        public async Task<TokenResponse> GenerateTokenAsync(AccountDTO dto)
        {
            var UserValuesWithIncludes = await userService.GetUsersByAccountId(dto.Id);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:JWTSecret"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var nowUtc = DateTime.UtcNow;
            var expire = nowUtc.AddMinutes(double.Parse(configuration["Token:ExpiryMinutes"])).ToUniversalTime();

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, UserValuesWithIncludes.AccountLogin),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")),
                new Claim(ClaimTypes.NameIdentifier, UserValuesWithIncludes.AccountId.ToString("D")),
                new Claim(ClaimTypes.Role, UserValuesWithIncludes.RoleName),
            };

            var JWTToken = new JwtSecurityToken(configuration["Token:Issuer"],
                configuration["Token:Issuer"],
                claims,
                expires: expire,
                signingCredentials: credentials);

            var accestoken = new JwtSecurityTokenHandler().WriteToken(JWTToken);

            return new TokenResponse()
            {
                AccesToken = accestoken,
                Role = UserValuesWithIncludes.RoleName,
                TokenExp = JWTToken.ValidTo,
                AccountId = UserValuesWithIncludes.AccountId
            };
        }
    }
     

    
}
