using Cwiczenia8.DTO;
using Cwiczenia8.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia8.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _service;
        public DoctorsController(IDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        { 
            var doctors = await _service.GetDoctors();
            return Ok(doctors);
        }

        [HttpDelete("{idDoctor}")]
        public async Task<IActionResult> RemoveDoctor(int idDoctor)
        {
            bool response = await _service.RemoveDoctor(idDoctor);
            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("{idDoctor}")]
        public async Task<IActionResult> UpdateDoctor(int idDoctor, DoctorDTO doctor)
        {
            bool response = await _service.UpdateDoctor(idDoctor, doctor);
            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task AddDoctor(DoctorDTO doctor)
        { 
            await _service.AddDoctor(doctor);
        }
        [HttpGet("/api/prescription/{idPrescription}")]
        public async Task<IActionResult> getPrescription(int idPrescription)
        {
            var prescription =  await _service.GetPrescription(idPrescription);
            if (prescription != null)
            {
                return Ok(prescription);
            }
            return BadRequest("Prescription not Found");
        }

    }
}
