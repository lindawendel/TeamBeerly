using Xunit;
using HealthCare.Core;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using HealthCare.Core.Data;

namespace HealthCareTests
{
    public class BookingServiceTests : IDisposable
    {
        private readonly HealthCareContext _context;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            // Use a unique database name for each test
            var options = new DbContextOptionsBuilder<HealthCareContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new HealthCareContext(options);
            _bookingService = new BookingService(_context);
        }

        [Fact]
        public void Should_Add_A_Booking_To_DB_Using_BookingService()
        {
            // Arrange
            var patient = new Patient { Id = Guid.NewGuid(), Name = "Muppet Muppeteo", Email = "jon.jon@example.com" };
            var booking = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };

            // Act
            _bookingService.AddBooking(booking);

            // Assert
            var addedBooking = _context.Bookings.FirstOrDefault(b => b.Service == "General Checkup");
            Assert.NotNull(addedBooking);
            Assert.Equal(patient.Name, addedBooking.Patient.Name);
        }

        public void Dispose()
        {
            // Dispose of the context after each test
            _context.Dispose();
        }
    }
}
