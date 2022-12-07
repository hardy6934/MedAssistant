using MedAssistant.Helpers.HelpersModels;

namespace MedAssistant.Models
{
    public class MedecineListForPagination
    { 
            public IEnumerable<MedicineModel> MedicineModel { get; set; }
            public PagingInfo PagingInfo { get; set; }
       
    }
}
