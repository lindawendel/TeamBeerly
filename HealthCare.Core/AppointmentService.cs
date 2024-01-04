using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;

namespace HealthCare.Core
{
    public class AppointmentService
    {
       private readonly HealthCareContext _dbContext;
       
       public AppointmentService(HealthCareContext dbContext)
       {
                _dbContext = dbContext;
       }
            public Appointment GetAppointment(Guid appointmentId)
            {
            return _dbContext.Appointments.FirstOrDefault(a => a.Id == appointmentId);
            }

        public List<Appointment> GetAppointmentsByPatientId(Guid patientId) 
        {
            return _dbContext.Appointments.Include(a => a.Caregiver).Where(a => a.Patient.Id == patientId).ToList();
          
        }
    }
}



