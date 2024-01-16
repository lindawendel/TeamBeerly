using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace HealthCare.Core
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HealthCareContext database;

        public AppointmentService(HealthCareContext database)
        {
            this.database = database;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var allAppointments = await database.Appointments.ToListAsync();

                return allAppointments;
        }

        public async Task<List<Appointment>> GetCaregiverAppointments(Guid caregiverId)
        {
            try
            {
                var caregiverAppointments = await database.Appointments
                    .Where(appointment => appointment.Caregiver.Id == caregiverId)
                    .ToListAsync();

                return caregiverAppointments;
            }
            catch (Exception ex)
            {
                throw new AppointmentServiceException("Error retrieving caregiver appointments.", ex);
            }
        }

      
        public async Task AddAppointment(Appointment newAppointment)
        {
            try
            {
                database.Appointments.Add(newAppointment);
                await database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                throw new AppointmentServiceException("Error adding appointment to the database.", ex);
            }
        }

        public async Task<Appointment> GetSpecificAppointment(Guid caregiverId, DateTime startTime)
        {

            var appointment = database.Appointments.Where(a => a.Caregiver.Id == caregiverId && a.StartTime == startTime).FirstOrDefault();  

            return appointment;
        }

        public async Task DeleteAppointment(Appointment appointment)
        {
            database.Appointments.Remove(appointment);
            await database.SaveChangesAsync();
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




