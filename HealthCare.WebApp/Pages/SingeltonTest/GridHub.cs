using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using HealthCare.Core;

namespace HealthCare.WebApp.Pages.SingeltonTest
{
    public class GridHub : Hub
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HubConnection _hubConnection;
        private readonly AppointmentService _appointmentService;

        public GridHub(IJSRuntime jsRuntime, HubConnection hubConnection, AppointmentService appointmentService)
        {
            _jsRuntime = jsRuntime;
            _hubConnection = hubConnection;
            _appointmentService = appointmentService;

            // Register the event handler for "NotifyUpdate"
            _hubConnection.On<int>("NotifyUpdate", async (appointmentId) =>
            {
                // Handle the update event
                // Fetch updated appointments from the service
                var updatedAppointments = await _appointmentService.GetAvailableAppointments();

                // Notify FullCalendar to refresh events
                await _hubConnection.SendAsync("RefreshEvents", updatedAppointments);
            });
        }


        public async Task ConnectAsync()
        {
            await _hubConnection.StartAsync();
        }

        public async Task SendAsync(string method)
        {
            await _hubConnection.SendAsync(method);
        }
    }
}
