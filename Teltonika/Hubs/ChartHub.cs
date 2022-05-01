using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Teltonika.Hubs
{
    public class ChartHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.AllExcept("").SendAsync("statistics-changed", "anonymous", message);
        }
    }
}
