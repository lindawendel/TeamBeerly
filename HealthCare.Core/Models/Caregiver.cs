using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class Caregiver
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

       // public string? Role {  get; set; } 
        public string Email { get; set; }

        [JsonIgnore]
        public List<Appointment> Appointments { get; set; }
    }
}
