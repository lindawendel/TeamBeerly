﻿using HealthCare.Core.Models;
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

        //private readonly HealthCareContext _dbContext;

        //public PatientService(HealthCareContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}


        //public List<Patient> GetAllPatients()
        //{
        //    return _dbContext.Patients.ToList();
        //}

        private List<Patient> patients = new List<Patient>();
        public PatientService()
        {
            patients.Add(new Patient { Id = Guid.NewGuid(), Name = "Hans Hansson", Email = "hans.h@example.com" });
            patients.Add(new Patient { Id = Guid.NewGuid(), Name = "Axel Fingersson", Email = "axel.finger@example.com" });
            patients.Add(new Patient { Id = Guid.NewGuid(), Name = "Astrid Lindgren", Email = "a.lindgren@example.com" });
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return patients;
        }

        public Patient GetPatient(Guid id)
        {
            Patient result = patients.FirstOrDefault(x => x.Id == id);
            return result;
        }

    }
}
