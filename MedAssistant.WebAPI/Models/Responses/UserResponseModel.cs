namespace MedAssistant.WebAPI.Models.Requests
{
    public class UserResponseModel
    { 
        public int Id { get; set; } 
        public string FullName { get; set; } 
        public DateTime Birthday { get; set; } 
        public string Location { get; set; }
        public int RoleId { get; set; }
        public int AccountId { get; set; }
        public string AccountLogin { get; set; }

    }
}
