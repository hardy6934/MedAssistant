using MedAssistant.Helpers.HelpersModels;

namespace MedAssistant.Models
{
    public class MedicalInstitutuinModelForPagination
    {
        public IEnumerable<MedicalInstitutionModel> MedicalInstitutionModels { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}
