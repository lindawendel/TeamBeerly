using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCare.Core.Data
{
    public class HealthCareContext : DbContext
    {
        public HealthCareContext(DbContextOptions<HealthCareContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "ConnectionStrings",
                    b => b.MigrationsAssembly("HealthCare.Core"));
            }
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
