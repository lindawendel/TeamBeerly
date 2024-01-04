using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Appointment> Appointments { get; set; } // remove?

         // public List<Booking> Bookings { get; set } // possible change
         // public List<Feedback> Feedbacks {get; set } // eventual

    }
}
