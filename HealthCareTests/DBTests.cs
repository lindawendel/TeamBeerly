using HealthCare.Core.Models;
using HealthCare.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace HealthCareTests
{
    public class BookingDBTests : IDisposable
    {
        private readonly HealthCareContext _context;

        public BookingDBTests()
        {
            var options = new DbContextOptionsBuilder<HealthCareContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new HealthCareContext(options);
        }

        [Fact]
        public void Should_Add_Booking_To_DB()
        {
            // Arrange
            var patient = new Patient { Id = Guid.NewGuid(), Name = "Test Testson", Email = "epost@example.com" };
            var booking = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };

            // Act
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Assert
            Assert.NotNull(_context.Bookings.FirstOrDefault(b => b.Service == "General Checkup"));
        }

        [Fact]
        public void Should_Delete_Booking_From_DB()
        {
            // Arrange
            var patient = new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" };
            var booking = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Act
            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            // Assert
            Assert.Null(_context.Bookings.FirstOrDefault(b => b.Service == "General Checkup"));
        }

        [Fact]
        public void Should_Edit_Name_In_A_Booking_In_DB()
        {
            // Arrange
            var patient = new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" };
            var booking = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Act
            var bookingToUpdate = _context.Bookings.FirstOrDefault(b => b.Service == "General Checkup");
            Assert.NotNull(bookingToUpdate); // Ensure that the booking exists in the database

            // Update the patient's name
            bookingToUpdate.Patient.Name = "Test Testson";
            _context.SaveChanges();

            // Assert
            var updatedBooking = _context.Bookings.FirstOrDefault(b => b.Service == "General Checkup");
            Assert.NotNull(updatedBooking);
            Assert.Equal("Test Testson", updatedBooking.Patient.Name);
        }


        public void Dispose()
        {
            // Dispose of the context after each test
            _context.Dispose();
        }




    }
}
