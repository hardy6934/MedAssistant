using MedAssistant.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAssistant.Controllers
{
    public class MedicalInstitutionController : Controller
    {
         
        public async Task<IActionResult> AddMedicalInstitutionAsync()
        {
            return View();
        }

        public async Task<IActionResult> AddMedicalInstitutionAsync(MedicalInstitutionModel medicalInstitutionModel)
        {
            return View();
        }

        public async Task<IActionResult> UpdateMedicalInstitutionAsync(int id)
        {
            return View();
        }

        public async Task<IActionResult> UpdateMedicalInstitutionAsync(MedicalInstitutionModel medicalInstitutionModel)
        {
            return View();
        }

        public async Task<IActionResult> RemoveMedicalInstitutionAsync(int id)
        {
            return View();
        }

        public async Task<IActionResult> RemoveMedicalInstitutionAsync(MedicalInstitutionModel medicalInstitutionModel)
        {
            return View();
        }
    }
}
