using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Patient? Patient { get; set; }
        public Caregiver Caregiver { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Service {  get; set; }
        public string? Description { get; set; }
        public bool IsBooked { get; set; } = false;
    }
}
