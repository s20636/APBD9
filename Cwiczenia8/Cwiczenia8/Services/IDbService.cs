using Cwiczenia8.DTO;

namespace Cwiczenia8.Services
{
    public interface IDbService
    {
        Task<IEnumerable<DoctorDTO>> GetDoctors();
        Task AddDoctor(DoctorDTO doctor);
        Task<bool> UpdateDoctor(int id, DoctorDTO doctor);
        Task<bool> RemoveDoctor(int id);
        Task<PrescriptionDTO> GetPrescription(int id);
    }
}
