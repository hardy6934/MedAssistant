using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class AuthenticationModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
    }
}
