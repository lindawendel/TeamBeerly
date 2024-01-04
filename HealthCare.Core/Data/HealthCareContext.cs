using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCare.Core.Data
{
    public class HealthCareContext : DbContext
    {
        public HealthCareContext()
        { }

        public HealthCareContext(DbContextOptions<HealthCareContext> options)
                   : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
