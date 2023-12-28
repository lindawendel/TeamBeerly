using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCare.WebApp.Data
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
    }
}
