using medicationService.Models;
using Microsoft.EntityFrameworkCore;

namespace medicationService.Data
{
    public class MedicationServiceConnection
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Medication> Medications { get; set; }
            public DbSet<MedicationRequest> MedicationRequests { get; set; }
        }
    }
}
