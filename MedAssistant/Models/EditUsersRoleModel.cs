using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class EditUsersRoleModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Location { get; set; }
        public int RoleId { get; set; }
        public List<SelectListItem> Roles { get; set; }

        public int AccountId { get; set; }
        public string AccountLogin { get; set; }

    }
}
