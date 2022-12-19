using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class EmailRecoveryPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

    }
}
