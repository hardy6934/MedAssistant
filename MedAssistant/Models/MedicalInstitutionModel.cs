using MedAssistant.Core.DataTransferObject;

namespace MedAssistant.Models
{
    public class MedicalInstitutionModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<DoctorDTO> DoctorsDTO { get; set; }

    }
}
