namespace MedAssistant.WebAPI.Models.Responses
{
    public class TokenResponse
    {
        public string AccesToken { get; set; }
        public string Role { get; set; }
        public int AccountId { get; set; }
        public DateTime TokenExp { get; set; }
    }
}
