using HealthCare.Core.Models;
using HealthCare.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.WebApp.SampleData
{
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
                    dbContext.Patients.Add(new Patient { Id = Guid.NewGuid(), Name = "Hans Hansson", Email = "hans.h@example.com" });
                    dbContext.Patients.Add(new Patient { Id = Guid.NewGuid(), Name = "Axel Fingersson", Email = "axel.finger@example.com" });
                    dbContext.Patients.Add(new Patient { Id = Guid.NewGuid(), Name = "Astrid Lindgren", Email = "a.lindgren@example.com" });

                    dbContext.SaveChanges();
                }

                if (!dbContext.Caregivers.Any())
                {
                    dbContext.Caregivers.Add(new Caregiver { Id = Guid.NewGuid(), Name = "Emma Engberg", Email = "emma.e@example.com" });
                    dbContext.Caregivers.Add(new Caregiver { Id = Guid.NewGuid(), Name = "Marcus Nilsson", Email = "marcus.n@example.com" });
                    dbContext.Caregivers.Add(new Caregiver { Id = Guid.NewGuid(), Name = "Sandra Ask", Email = "sandra.a@example.com" });

                    dbContext.SaveChanges();
                }

                foreach (var p  in dbContext.Patients) 
                {
                    Console.WriteLine(p.Id + " " + p.Name + " " + p.Email);
                }

                foreach (var c in dbContext.Caregivers)
                {
                    Console.WriteLine(c.Id + " " + c.Name + " " + c.Email);
                }
            }

            
        }

    }
}
