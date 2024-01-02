using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public int RatingValue { get; set; }
        public DateTime Time { get; set; }
    }
}
