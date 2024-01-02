using HealthCare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Core
{
    public class AppointmentService
    {
        public List<Appointment> Appointments { get; private set; }

        public Appointment GetAppointment(Guid appointmentId)
        {
            return Appointments.FirstOrDefault(a => a.Id == appointmentId);
        }

       
    }
}



