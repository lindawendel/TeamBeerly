using System;
using System.Collections.Generic;
using System.Linq;
using HealthCare.Core.Data;
using HealthCare.Core.Models;

namespace HealthCare.Core
{
    public class CaregiverService
    {
        private readonly HealthCareContext _dbContext;

        public CaregiverService(HealthCareContext dbContext)
        {
            _dbContext = dbContext;

            // Ensure the database is created
            _dbContext.Database.EnsureCreated();
        }

        public IEnumerable<Caregiver> GetCaregivers()
        {
            return _dbContext.Caregivers.ToList();
        }


    }
}



