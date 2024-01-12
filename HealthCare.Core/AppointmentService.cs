using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Core
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HealthCareContext database;

        public AppointmentService(HealthCareContext database)
        {
            this.database = database;
        }

        public async Task<List<Appointment>> GetCaregiverAppointments(Guid caregiverId)
        {
            var caregiverAppointments = await database.Appointments
                .Where(appointment => appointment.Caregiver.Id == caregiverId)
                .ToListAsync();

            return caregiverAppointments;
        }


        public async Task AddAppointment(Appointment newAppointment)
        {
            try
            {
                // Add the new appointment to the database
                database.Appointments.Add(newAppointment);
                await database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (log, show message, etc.)
            }
        }
        
    }

}




