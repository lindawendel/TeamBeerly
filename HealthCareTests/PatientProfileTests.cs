using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareTests
{
    public class PatientProfileTests : IDisposable
    {
        private readonly HealthCareContext _context;

        public PatientProfileTests()
        {
            var options = new DbContextOptionsBuilder<HealthCareContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new HealthCareContext(options);
        }

        [Fact]
        public void Should_Update_Personal_Profile()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }


        public void Dispose()
        {
            // Dispose of the context after each test
            _context.Dispose();
        }




    }
}
