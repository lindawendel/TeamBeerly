using HealthCare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetCaregiverAppointments(Guid caregiverId);
        Task AddAppointment(Appointment newAppointment);
    }
}
