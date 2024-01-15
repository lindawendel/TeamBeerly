using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
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
                // Throw a custom exception to signal the UI about the error
                throw new AppointmentServiceException("Error adding appointment to the database.", ex);
            }
        }

        public Appointment GetSpecificAppointment(Guid caregiverId, DateTime startTime)
        {

            var appointment = database.Appointments.Where(a => a.Caregiver.Id == caregiverId && a.StartTime == startTime).FirstOrDefault(); 

            if (appointment == null) 
            {
                return null; 
            }
            else
            {
                return appointment;
            }
        }

        public void DeleteAppointment(Appointment appointment)
        {
            database.Appointments.Remove(appointment);
            database.SaveChanges();
        }
        
    }
    public class AppointmentServiceException : Exception
    {
        public AppointmentServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}




