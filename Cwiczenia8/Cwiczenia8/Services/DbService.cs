using Cwiczenia8.DTO;
using Cwiczenia8.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia8.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _context;
        public DbService(MainDbContext context)
        {
            _context = context;
        }
        public async Task AddDoctor(DoctorDTO doctor)
        {
            Doctor d = new Doctor {FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email};
            await _context.Doctors.AddAsync(d);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorDTO>> GetDoctors()
        {
            return await _context.Doctors
                .Select(e => new DoctorDTO
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                })
                .ToListAsync();
        }

        public async Task<PrescriptionDTO> GetPrescription(int id)
        {
            return await _context.Prescriptions
                .Where(e => e.IdPrescription == id)
                .Include(e => e.Prescription_Medicaments)
                .ThenInclude(e => e.Medicament)
                .Select(e => new PrescriptionDTO
                {
                    Date = e.Date,
                    DueDate = e.DueDate,
                    Doctor = new DoctorDTO { FirstName = e.Doctor.FirstName, LastName = e.Doctor.LastName, Email = e.Doctor.Email },
                    Patient = new PatientDTO { FirstName = e.Patient.FirstName, LastName = e.Patient.LastName },
                    Medicaments = e.Prescription_Medicaments
                        .Select(e => new MedicamentDTO { Name = e.Medicament.Name, Dose = e.Dose })
                }).SingleAsync();
        }

        public async Task<bool> RemoveDoctor(int id)
        {
            Doctor doctor = await _context.Doctors.Where(d => d.IdDoctor == id).FirstOrDefaultAsync();
            if (doctor == null)
            {
                return false;
            }
            _context.Attach(doctor);
            _context.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDoctor(int id, DoctorDTO doctor)
        {
            Doctor dbDoctor = await _context.Doctors.Where(d => d.IdDoctor == id).FirstOrDefaultAsync();
            if (dbDoctor == null)
            {
                return false;
            }
            dbDoctor.FirstName = doctor.FirstName;
            dbDoctor.LastName = doctor.LastName;
            dbDoctor.Email = doctor.Email;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
