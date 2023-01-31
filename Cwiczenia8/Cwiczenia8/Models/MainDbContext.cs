using Microsoft.EntityFrameworkCore;

namespace Cwiczenia8.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }
        protected MainDbContext()
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(p => 
            {
                
            });
            modelBuilder.Entity<Patient>(p => 
            {
                p.HasData(
                        new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-01-01")},
                        new Patient { IdPatient = 2, FirstName = "Tomasz", LastName = "Tracz", BirthDate = DateTime.Parse("2001-02-03")}
                    );
            });
            modelBuilder.Entity<Doctor>(p =>
            {
                p.HasData(
                        new Doctor {IdDoctor = 1, FirstName = "Maria", LastName = "Db", Email = "maria@wp.pl"}
                    );
            });
            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasData(
                        new Prescription { IdPrescription = 1, Date = DateTime.Parse("2020-01-01"), DueDate = DateTime.Parse("2021-01-01"), IdDoctor = 1, IdPatient = 1}
                    );
            });
            modelBuilder.Entity<Prescription_Medicament>(p =>
            {
                p.HasKey(k => new { k.IdMedicament, k.IdPrescription });
                p.HasData(
                        new Prescription_Medicament {IdPrescription = 1, IdMedicament = 1, Dose = 5, Details = "xddd" }
                    );
            });
            modelBuilder.Entity<Medicament>(p =>
            {
                p.HasData(
                        new Medicament { IdMedicament = 1, Name = "Nervosol", Description = "asdas", Type = "agdf" }
                    );
            });
        }
    }
}
