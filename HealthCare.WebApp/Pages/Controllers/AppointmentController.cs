using HealthCare.Core;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;

    public AppointmentController(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("availableAppointments")]
    public IActionResult GetAvailableAppointments()
    {
        // Fetch available appointments from your database
        var availableAppointments = _appointmentService.GetAvailableAppointments();
        return Ok(availableAppointments);
    }
}



