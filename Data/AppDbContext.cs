using Microsoft.EntityFrameworkCore;
using Telintec_RH.Models;

namespace Telintec_RH.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Operation> Operations { get; set; }

    }
}
