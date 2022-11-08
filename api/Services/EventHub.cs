namespace PressYourLuckApi.Services;

public class EventHub : Hub
{
    public async Task SendMessageAsync(string message)
    {
        Console.WriteLine("Sendmessage called");
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}

