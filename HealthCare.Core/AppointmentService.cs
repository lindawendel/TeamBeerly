using HealthCare.Core.Models;
using System;
namespace HealthCare.Core
{
    public class AppointmentService
    {

        public List<Appointment> Appointments { get; private set; }
        public Appointment GetAppointments(Guid appointmentId)
        {
            return Appointments.FirstOrDefault(a => a.Id == appointmentId);
        }
        public AppointmentService()
        {
            InitializeAppointments();
        }

        private void InitializeAppointments()
        {
            Appointments = new List<Appointment>
        {
            new Appointment
            {
                Id = Guid.NewGuid(),
                Patient = new Patient { Id = Guid.Parse("100"), Name = "Patient 100" },
                Caregiver = new Caregiver { Id = Guid.NewGuid(), Name = "Dr. Smith" },
                Time = DateTime.Now.AddHours(1),
                Service = "General Checkup",
                Description = "Appointment with Dr. Smith"
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                Patient = new Patient { Id = Guid.Parse("101"), Name = "Patient 101" },
                Caregiver = new Caregiver { Id = Guid.NewGuid(), Name = "Dr. Johnson" },
                Time = DateTime.Now.AddHours(2),
                Service = "Dental Checkup",
                Description = "Appointment with Dr. Johnson"
            }


        };
        }

        /*        private List<AppointmentDetails> appointments;

                public AppointmentService()
                {
                    // mock data
                    appointments = new List<AppointmentDetails>
                    {
                        new AppointmentDetails { Id = "1", PatientId = "100", Details = "Appointment with Dr. Smith" },
                        new AppointmentDetails { Id = "2", PatientId = "101", Details = "Appointment with Dr. Johnson" }
                    };
                }

                public AppointmentDetails GetAppointmentDetails(string appointmentId)
                {
                    return appointments.FirstOrDefault(a => a.Id == appointmentId);
                }


                public class AppointmentDetails
                {
                    public string Id { get; set; }
                    public string PatientId { get; set; }
                    public string Details { get; set; }
                }*/
    }
}

