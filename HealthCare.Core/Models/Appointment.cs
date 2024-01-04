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
        public Patient Patient { get; set; }
        public Caregiver Caregiver { get; set; }
        public DateTime Time { get; set; }
        public string Service {  get; set; }
        public string Description { get; set; }
    }

    //possible changes to entity
    
    //public class Appointment
    //{
    //    public Guid Id { get; set; }
    //    public Booking Booking { get; set; }
    //    public Caregiver Caregiver { get; set; }
    //    public string Description { get; set; }
    //}
}
