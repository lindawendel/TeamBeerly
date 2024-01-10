using System;
namespace HealthCare.Core.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Guid PatientId { get; set; }
        public string Service { get; set; }
    }

}

