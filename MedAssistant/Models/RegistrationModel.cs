using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }


    }
}
