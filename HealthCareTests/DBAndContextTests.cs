using Xunit;
using HealthCare.Core;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using HealthCare.Core.Data;
using Microsoft.Extensions.Configuration;

namespace HealthCareTests
{
    //funkar på local om man har en DB

   /* public class AppointmentServiceTests
    {
        private readonly HealthCareContext _context;
        private readonly AppointmentService _appointmentService;

        public AppointmentServiceTests()
        {
            // Use the configuration system to get the connection string based on the environment
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Update the file name if necessary
                .Build();

            // Output the value of the connection string for debugging purposes
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");

            var options = new DbContextOptionsBuilder<HealthCareContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new HealthCareContext(options);
            _appointmentService = new AppointmentService(_context);
        }

        [Fact]
        public void Should_Add_A_Appointment_To_DB_Using_AppointmentService()
        {
            // Arrange
            var patient = _context.Patients.FirstOrDefault();
            var appointment = new Appointment { Id = Guid.NewGuid(), StartTime = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };

            // Act
            _appointmentService.AddAppointment(appointment);

            // Assert
            var addedAppointment = _context.Appointments.FirstOrDefault(b => b.Patient == patient);
            Assert.NotNull(appointment);
            Assert.Equal(patient.Name, appointment.Patient.Name);
        }
    }*/
}
