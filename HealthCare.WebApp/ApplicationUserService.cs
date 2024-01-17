using HealthCare.Core.Data;
using HealthCare.Core.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Security.Claims;

namespace HealthCare.WebApp
{
    public class ApplicationUserService
    {
        private HealthCareContext _dbContext;
        private AuthenticationStateProvider _authState;

        public ApplicationUserService(HealthCareContext dbContext, AuthenticationStateProvider authenticationStateProvider)
        {
            _dbContext = dbContext;
            _authState = authenticationStateProvider;
        }

        List<string> auth0Ids = new List<string>
        {
            "auth0|659fce7eb2b6d54c87f82290",
            "auth0|659fd055a4ffcf97947ac05b",
            "auth0|659fd0d0b2b6d54c87f823d0",
            "auth0|659fd1182c6a20cc35b2bc25",
            "auth0|659fd140a4ffcf97947ac0de",
            "auth0|659fd16f86e1aaad2fb02430"
        };

        public async Task<string> GetAuthId()
        {
            var state = await _authState.GetAuthenticationStateAsync();
            return state.User.Claims
                .Where(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                .Select(c => c.Value)
                .FirstOrDefault() ?? string.Empty;
        }

        public async Task<bool> IsCaregiver()
        {
            string authId = await GetAuthId();

            if (auth0Ids.Contains(authId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Patient?> GetCurrentPatient()
        {
            var authId = await GetAuthId();

            return _dbContext.Patients.FirstOrDefault(p => p.Auth0Id == authId);
        }

        public async Task<Caregiver?> GetCurrentCaregiver()
        {
            var authId = await GetAuthId();
            return _dbContext.Caregivers.FirstOrDefault(p => p.Auth0Id == authId);
        }

        public async Task<string> GetGivenName()
        {
            var givenName = await _authState?.GetAuthenticationStateAsync();
            return givenName.User.Claims
                .Where(c => c.Type.Equals(ClaimTypes.GivenName))
                .Select(c => c.Value)
                .FirstOrDefault() ?? string.Empty;
        }

        public async Task<string> GetUserName(Patient patient, Caregiver caregiver)
        {
            string googleName = await GetGivenName();
            var authId = await GetAuthId();

            if (googleName != null)
            {
                return "Hej " + googleName;
            }
            else if (auth0Ids.Contains(authId))
            {
                caregiver = _dbContext.Caregivers.FirstOrDefault(p => p.Auth0Id == authId);

                if (caregiver != null)
                {
                    if (caregiver.Name == "Not provided Not provided")
                    {
                        return "";
                    }
                    else
                    {
                        return "Hej " + caregiver.Name;
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                patient = await GetCurrentPatient();

                if (patient != null)
                {
                    if (patient.Name == "Not provided Not provided")
                    {
                        return "";
                    }
                    else
                    {
                        return "Hej " + patient.Name;
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<string?> GetProfilePicture()
        {
            var authState = await _authState.GetAuthenticationStateAsync();
            return authState.User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? string.Empty;
        }
    }
}
