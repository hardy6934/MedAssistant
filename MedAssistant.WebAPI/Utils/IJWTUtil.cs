using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.WebAPI.Utils
{
    public interface IJWTUtil
    {
        Task<TokenResponse> GenerateTokenAsync(AccountDTO dto);

    }
}
