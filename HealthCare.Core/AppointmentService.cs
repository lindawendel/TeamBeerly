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
                // Set caregiver for the new appointment
                newAppointment.Caregiver = database.Caregivers.FirstOrDefault();

                // Add the new appointment to the database
                database.Appointments.Add(newAppointment);
                await database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (log, show message, etc.)
            }
        }


        //AS OF RIGHT NOW
        //public async task addappointment(appointment newappointment)
        //{
        //    // set caregiver id for the new appointment
        //    newappointment.caregiver.id = database.caregivers.firstordefault().id;

        //    // add the new appointment to the database
        //    database.appointments.add(newappointment);
        //    await database.savechangesasync();
        //}

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




