using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MedAssistant.Controllers
{

    [Authorize(Roles = "Moderator,User,Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper )
        {
            this.doctorService = doctorService;
            this.mapper = mapper;
        }


        public async Task<IActionResult> DoctorViewAsync()
        {
            try {
                var email = HttpContext.User.Identity.Name;
                var doctors = await doctorService.GetAllDoctorsByEmailAsync(email);
                return View("DoctorView", doctors.Select(doctor => mapper.Map<DoctorModel>(doctor)).ToList());
            }
            catch (Exception ex) {
                Log.Error($"{ex.Message}. {Environment.NewLine}  {ex.StackTrace}");
                return NotFound();
            }
        }
    }
}
