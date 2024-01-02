using HealthCare.Core.Models;
using Xunit;

namespace HealthCareTests
{
    public class ModelTests
    {
        [Fact]
        public void Patient_Attributes_set_Test()
        {
            // Arrange
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = "Muppet Muppetsson",
                Email = "denna@example.com"
            };

            // Act

            // Assert
            Assert.Equal(patient.Id, patient.Id);
            Assert.Equal("Muppet Muppetsson", patient.Name);
            Assert.Equal("denna@example.com", patient.Email);
        }

        [Fact]
        public void Caregiver_Attributes_set_Test()
        {
            // Arrange
            var caregiver = new Caregiver
            {
                Id = Guid.NewGuid(),
                Name = "Muppet Muppetsson",
                Email = "denna@example.com"
            };

            // Act

            // Assert
            Assert.Equal(caregiver.Id, caregiver.Id);
            Assert.Equal("Muppet Muppetsson", caregiver.Name);
            Assert.Equal("denna@example.com", caregiver.Email);
        }

        [Fact]
        public void Admin_Attributes_set_Test()
        {
            // Arrange
            var admin = new Admin
            {
                Id = Guid.NewGuid()
            };

            // Act

            // Assert
            Assert.Equal(admin.Id, admin.Id);
        }

        [Fact]
        public void Appointment_Attributes_set_Test()
        {
            // Arrange
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = "Patient 1",
                Email = "patient1@example.com"
            };

            var caregiver = new Caregiver
            {
                Id = Guid.NewGuid(),
                Name = "Dr. Smith",
                Email = "drsmith@example.com"
            };

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Patient = patient,
                Caregiver = caregiver,
                Time = DateTime.Now,
                Service = "General Checkup",
                Description = "Appointment with Dr. Smith"
            };

            // Act

            // Assert
            Assert.Equal(appointment.Id, appointment.Id);
            Assert.Equal(patient, appointment.Patient);
            Assert.Equal(caregiver, appointment.Caregiver);
            Assert.Equal(appointment.Time, appointment.Time);
            Assert.Equal("General Checkup", appointment.Service);
            Assert.Equal("Appointment with Dr. Smith", appointment.Description);
        }

        [Fact]
        public void Booking_Attributes_set_Test()
        {
            // Arrange
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = "Patient 2",
                Email = "patient2@example.com"
            };

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Time = DateTime.Now,
                Patient = patient,
                Service = "Dental Checkup"
            };

            // Act

            // Assert
            Assert.Equal(booking.Id, booking.Id);
            Assert.Equal(patient, booking.Patient);
            Assert.Equal(booking.Time, booking.Time);
            Assert.Equal("Dental Checkup", booking.Service);
        }

        [Fact]
        public void Feedback_Attributes_set_Test()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                Title = "Great Service",
                Comment = "Great service, thank you!",
                Time = DateTime.Now
            };

            // Act

            // Assert
            Assert.Equal(feedback.Id, feedback.Id);
            Assert.Equal("Great Service", feedback.Title);
            Assert.Equal("Great service, thank you!", feedback.Comment);
            Assert.Equal(feedback.Time, feedback.Time);
        }

        [Fact]
        public void Rating_Attributes_set_Test()
        {
            // Arrange
            var rating = new Rating
            {
                Id = Guid.NewGuid(),
                RatingValue = 5,
                Time = DateTime.Now
            };

            // Act

            // Assert
            Assert.Equal(rating.Id, rating.Id);
            Assert.Equal(5, rating.RatingValue);
            Assert.Equal(rating.Time, rating.Time);
        }
    }
}
