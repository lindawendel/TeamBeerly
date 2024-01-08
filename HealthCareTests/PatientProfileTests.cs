using HealthCare.Core;
using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HealthCareTests
{
    public class PatientProfileTests : IDisposable
    {
        private readonly HealthCareContext _context;
        private readonly PatientService _patientService;

        public PatientProfileTests()
        {
            var options = new DbContextOptionsBuilder<HealthCareContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new HealthCareContext(options);
            _context.Patients.AddRange(new Patient { Id = Guid.NewGuid(), Name = "Hans Hansson", Email = "hans.h@example.com" },
            new Patient { Id = Guid.NewGuid(), Name = "Axel Fingersson", Email = "axel.finger@example.com" },
           new Patient { Id = Guid.NewGuid(), Name = "Astrid Lindgren", Email = "a.lindgren@example.com" },
            new Patient { Id = Guid.NewGuid(), Name = "Eva Svensson", Email = "eva.svensson@example.com" });
            _context.SaveChanges();

            _patientService = new PatientService(_context);
        }

        [Fact]
        public void Should_Update_Patient_Information_In_DB_()
        {
            //ARRANGE
            
            var patient = _context.Patients.FirstOrDefault();

            //ACT

            if (patient != null)
            {
                patient.DateOfBirth = DateTime.Now;
                patient.Name = "MIKAEL";
                patient.Email = "MIKAEL@test.com";

                _context.SaveChanges();
            }
            //ASSERT

            var testPatient = _context.Patients.FirstOrDefault();
            string testPatientDateOfBirth = testPatient.DateOfBirth.ToString("yyyy-MM-dd");
           


            Assert.Equal(testPatientDateOfBirth, DateTime.Now.ToString("yyyy-MM-dd"));
            Assert.Equal("MIKAEL", testPatient.Name);
            Assert.Equal("MIKAEL@test.com", testPatient.Email);

        }

        public void Dispose()
        {
            // Dispose of the context after each test
            _context.Dispose();
        }




    }
}
