using HealthCare.Core.Data;

using System;
using Microsoft.Extensions.DependencyInjection;
using HealthCare.Core.Models;

public static class InMemoryDbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HealthCareContext>();

            dbContext.Database.EnsureCreated();

            if (!dbContext.Patients.Any() || !dbContext.Caregivers.Any() || !dbContext.Bookings.Any() || !dbContext.Appointments.Any())
            {
                var patient1 = new Patient { Id = Guid.NewGuid(), Name = "Hans Hansson", Email = "hans.h@example.com" };
                var patient2 = new Patient { Id = Guid.NewGuid(), Name = "Axel Fingersson", Email = "axel.finger@example.com" };
                var patient3 = new Patient { Id = Guid.NewGuid(), Name = "Astrid Lindgren", Email = "a.lindgren@example.com" };

                dbContext.Patients.Add(patient1);
                dbContext.Patients.Add(patient2);
                dbContext.Patients.Add(patient3);

                var caregiver1 = new Caregiver { Id = Guid.NewGuid(), Name = "Dr Emma Engberg", Email = "emma.e@example.com", };
                var caregiver2 = new Caregiver { Id = Guid.NewGuid(), Name = "Dr Marcus Nilsson", Email = "marcus.n@example.com" };
                var caregiver3 = new Caregiver { Id = Guid.NewGuid(), Name = "Dr Sandra Ask", Email = "sandra.a@example.com" };

                dbContext.Caregivers.Add(caregiver1);
                dbContext.Caregivers.Add(caregiver2);
                dbContext.Caregivers.Add(caregiver3);

                var booking1 = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient1, Service = "General Checkup" };
                var booking2 = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(4), Patient = patient2, Service = "Vaccination" };
                var booking3 = new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(6), Patient = patient3, Service = "Amputation" };

                dbContext.Bookings.Add(booking1);
                dbContext.Bookings.Add(booking2);
                dbContext.Bookings.Add(booking3);

                var appointment1 = new Appointment { Id = Guid.NewGuid(), Patient = patient1, Caregiver = caregiver1, Time = DateTime.Now.AddHours(2), Service = booking1.Service, Description = "Ahoy ahoy" };
                var appointment2 = new Appointment { Id = Guid.NewGuid(), Patient = patient2, Caregiver = caregiver2, Time = DateTime.Now.AddHours(4), Service = booking2.Service, Description = "Ahoy ahoy2" };
                var appointment3 = new Appointment { Id = Guid.NewGuid(), Patient = patient3, Caregiver = caregiver3, Time = DateTime.Now.AddHours(6), Service = booking3.Service, Description = "Ahoy ahoy3" };

                dbContext.Appointments.Add(appointment1);
                dbContext.Appointments.Add(appointment2);
                dbContext.Appointments.Add(appointment3);

                dbContext.SaveChanges();

                foreach (var p in dbContext.Patients)
                {
                    Console.WriteLine(p.Id + " " + p.Name + " " + p.Email);
                }

                foreach (var c in dbContext.Caregivers)
                {
                    Console.WriteLine(c.Id + " " + c.Name + " " + c.Email);
                }

                foreach (var a in dbContext.Appointments)
                {
                    Console.WriteLine(a.Id + " " +  a.Patient.Name + " "+ a.Caregiver.Name);
                }
            }

        }

        
    }
}

