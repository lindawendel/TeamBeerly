using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
        public Patient Patient { get; set; } //prolly b a FK?
        public Caregiver Caregiver { get; set; } //also proly a FK?
    }
}
