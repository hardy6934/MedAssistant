namespace MedAssistant.WebAPI.Models.Requests
{
    public class RegAccountRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

    }
}
