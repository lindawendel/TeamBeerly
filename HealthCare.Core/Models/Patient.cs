﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Auth0Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        [RegularExpression(@"^\d{8,}$", ErrorMessage = "Phone Number must have at least 8 digits")]
        public string PhoneNumber { get; set; } = "00000000";
       
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [JsonIgnore]
        public List<Appointment> Appointments { get; set; }
    }
}
