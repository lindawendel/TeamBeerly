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


        //AS OF RIGHT NOW
        public async Task AddAppointment(Appointment newAppointment)
        {
            // Set caregiver ID for the new appointment
            newAppointment.Caregiver.Id = database.Caregivers.FirstOrDefault().Id;

            // Add the new appointment to the database
            database.Appointments.Add(newAppointment);
            await database.SaveChangesAsync();
        }

    }

    //private readonly HealthCareContext _dbContext;

    //public AppointmentService(HealthCareContext dbContext)
    //{
    //         _dbContext = dbContext;
    //}

    //     public Appointment GetAppointment(Guid appointmentId)
    //     {
    //     return _dbContext.Appointments.FirstOrDefault(a => a.Id == appointmentId);
    //     }

    // public List<Appointment> GetAppointmentsByPatientId(Guid patientId) 
    // {
    //     return _dbContext.Appointments.Include(a => a.Caregiver).Where(a => a.Patient.Id == patientId).ToList();

    // }
}




