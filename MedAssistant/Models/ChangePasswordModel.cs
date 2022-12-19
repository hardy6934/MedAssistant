using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Oldpassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Newpassword { get; set; }

        [Compare(nameof(Newpassword))]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}
