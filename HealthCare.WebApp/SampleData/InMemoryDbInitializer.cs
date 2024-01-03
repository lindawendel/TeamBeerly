using HealthCare.WebApp.Data;

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

            if (!dbContext.Patients.Any())
            {
                if (!dbContext.Patients.Any())
                {
                    var patient1 = new Patient { Id = Guid.NewGuid(), Name = "Hans Hansson", Email = "hans.h@example.com" };
                    var patient2 = new Patient { Id = Guid.NewGuid(), Name = "Axel Fingersson", Email = "axel.finger@example.com" };
                    var patient3 = new Patient { Id = Guid.NewGuid(), Name = "Astrid Lindgren", Email = "a.lindgren@example.com" };


                    dbContext.Patients.Add(patient1);
                    dbContext.Patients.Add(patient2);
                    dbContext.Patients.Add(patient3);

                    dbContext.SaveChanges();

                if (!dbContext.Caregivers.Any())
                {
                    dbContext.Caregivers.Add(new Caregiver { Id = Guid.NewGuid(), Name = "Emma Engberg", Email = "emma.e@example.com" });
                    dbContext.Caregivers.Add(new Caregiver { Id = Guid.NewGuid(), Name = "Marcus Nilsson", Email = "marcus.n@example.com" });
                    dbContext.Caregivers.Add(new Caregiver { Id = Guid.NewGuid(), Name = "Sandra Ask", Email = "sandra.a@example.com" });

                    dbContext.SaveChanges();
                }

                if (!dbContext.Bookings.Any())
                {

                    dbContext.Add(new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(2), Patient = patient1, Service = "General Checkup" });
                    dbContext.Add(new Booking { Id = Guid.NewGuid(), Time = DateTime.Now.AddHours(4), Patient = patient2, Service = "Vaccination" });
                }
                dbContext.SaveChanges();
                }




            }
        }
    }
}