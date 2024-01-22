using HealthCare.Core.Data;
using HealthCare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HealthCareTests
{
    public class AppointmentDBTests
    {
        private readonly HealthCareContext _context;

        public AppointmentDBTests()
        {
            // For simplicity, we are not initializing a real database context in this example.
            // In a real-world scenario, you would use dependency injection and a real database context.
            // For testing purposes, we are using a simple list instead.
        }

        [Fact]
        public void Should_Add_Appointment_To_List()
        {
            var patient = new Patient { Id = Guid.NewGuid(), Name = "Test Testson", Email = "epost@example.com" };
            var appointments = new List<Appointment>();

            var appointment = new Appointment { Id = Guid.NewGuid(), StartTime = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };

            appointments.Add(appointment);

            Assert.NotNull(appointments.FirstOrDefault(b => b.Service == "General Checkup"));
        }

        [Fact]
        public void Should_Delete_Appointment_From_List()
        {
            var patient = new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" };
            var appointments = new List<Appointment>();

            var appointment = new Appointment { Id = Guid.NewGuid(), StartTime = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };
            appointments.Add(appointment);

            appointments.Remove(appointment);

            Assert.Null(appointments.FirstOrDefault(b => b.Service == "General Checkup"));
        }

        [Fact]
        public void Should_Edit_Name_In_An_Appointment_In_List()
        {
            var patient = new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" };
            var appointments = new List<Appointment>();

            var appointment = new Appointment { Id = Guid.NewGuid(), StartTime = DateTime.Now.AddHours(2), Patient = patient, Service = "General Checkup" };
            appointments.Add(appointment);

            var appointmentToUpdate = appointments.FirstOrDefault(b => b.Service == "General Checkup");
            Assert.NotNull(appointmentToUpdate);

            appointmentToUpdate.Patient.Name = "Test Testson";

            var updatedAppointment = appointments.FirstOrDefault(b => b.Service == "General Checkup");
            Assert.NotNull(updatedAppointment);
            Assert.Equal("Test Testson", updatedAppointment.Patient.Name);
        }
    }
}
