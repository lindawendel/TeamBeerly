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
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.WebApp
{
    public class PatientService
    {

        private readonly HealthCareContext _dbContext;
        private AuthenticationStateProvider _authenticationStateProvider;

        public PatientService(HealthCareContext dbContext, AuthenticationStateProvider authenticationStateProvider)
        {
            _dbContext = dbContext;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public List<Patient> GetAllPatients()
        {
            return _dbContext.Patients.ToList();
        }

        public async Task<string> GetAuthId()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Claims
                .Where(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                .Select(c => c.Value)
                .FirstOrDefault() ?? string.Empty;
        }

        public async Task<string?> GetProfilePicture()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? string.Empty;
        }

        public async Task<Patient> GetCurrentPatient()
        {
            var authId = await GetAuthId();

            return _dbContext.Patients.FirstOrDefault(p => p.Auth0Id == authId);
        }

        public async Task SavePatientInformation(string authId, string name, DateTime dob, string phoneNumber, string email)
        {
            var existingPatient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Auth0Id == authId);

            if (existingPatient != null)
            {
                existingPatient.Name = name;
                existingPatient.DateOfBirth = dob;
                existingPatient.PhoneNumber = phoneNumber;
                existingPatient.Email = email;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
