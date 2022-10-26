using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        public string Location { get; set; }
        public int RoleId { get; set; }
        public int AccountId { get; set; }
        public string AccountLogin { get; set; }

    }
}
