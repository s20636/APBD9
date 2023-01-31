namespace Cwiczenia8.DTO
{
    public class PrescriptionDTO
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DoctorDTO Doctor { get; set; }
        public PatientDTO Patient { get; set; }
        public IEnumerable<MedicamentDTO> Medicaments { get; set; }
    }
}
