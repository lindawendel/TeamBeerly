using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace HealthCare.WebApp.Pages.SingeltonTest
{
    public class GridHub : Hub
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HubConnection _hubConnection;

        public GridHub(IJSRuntime jsRuntime, HubConnection hubConnection)
        {
            _jsRuntime = jsRuntime;
            _hubConnection = hubConnection;
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
