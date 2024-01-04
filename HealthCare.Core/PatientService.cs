using HealthCare.Core.Models;
using HealthCare.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthCare.Core
{
    public class PatientService
    {

        private readonly HealthCareContext _dbContext;

        public PatientService(HealthCareContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Patient> GetAllPatients()
        {
            return _dbContext.Patients.ToList();
        }

        public Patient GetPatient(Guid id) 
        {
            Patient result = _dbContext.Patients.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Patient DuringDevFindOnePatient()
        {
            if (_dbContext.Patients != null)
            {
                return _dbContext.Patients.FirstOrDefault();
            }
            else
            {
                throw new Exception();
            }
        }

    }
}
