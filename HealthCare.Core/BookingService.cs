using System;
using System.Collections.Generic;
using System.Linq;
using HealthCare.Core.Models;
using HealthCare.WebApp.Data;

namespace HealthCare.Core
{
    public class BookingService 
    {
        private readonly HealthCareContext _dbContext;

        public BookingService(HealthCareContext dbContext)
        {
            _dbContext = dbContext;

            // Ensure the database is created
            _dbContext.Database.EnsureCreated();
        }

        public IEnumerable<Booking> GetBookings()
        {
            return _dbContext.Bookings.ToList();
        }

        public void AddBooking(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }
    }
}



