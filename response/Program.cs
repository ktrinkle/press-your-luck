using PressYourLuck;

// .net 6 minimal program model
// not sure if this is the best way yet.


var lockedStatus = true;
HubConnection eventHub;
string? accessToken = String.Empty;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get values from the config, and assign to the big variable.
var settings = config.GetRequiredSection("AppSettings").Get<Config>();
   
// inits
// 3 arrays are set in appsettings, and all 3 must be in the correct order for returns to work.

using var controller = new GpioController();
foreach ( int pin in settings.Pins )
{
    controller.OpenPin(pin, PinMode.InputPullDown);
    controller.RegisterCallbackForPinValueChangedEvent(pin, PinEventTypes.Rising, OnPinEvent);
}

foreach ( int pin in settings.Lights )
{
    controller.SetPinMode(pin, PinMode.Output);
}

// Check if we are online. If so, await result of API call.
// if (settings.OnlineInd)
// {
//     eventHub = new HubConnectionBuilder()
//         .WithUrl(settings.SignalRHub, options =>
//         { 
//             options.AccessTokenProvider = () => Task.FromResult(accessToken);
//         })
//         .Build();

//     eventHub.Closed += async (error) =>
//     {
//         await Task.Delay(new Random().Next(0,5) * 1000);
//         await eventHub.StartAsync();
//     };
// }

if (!settings.OnlineInd)
{
    lockedStatus = false;
}

// once we're initialized let the users know
foreach(int pin in settings.Pins)
{
    controller.Write(pin, PinValue.High);
}

Thread.Sleep(1000);

foreach(int pin in settings.Pins)
{
    controller.Write(pin, PinValue.Low);
}

// now we just sit and wait forever instead of doing a while loop

await Task.Delay(Timeout.Infinite);

void OnPinEvent(object sender, PinValueChangedEventArgs args)
{     
    var pinIndex = Array.FindIndex(settings.Pins, p => p == args.PinNumber);

    if (!lockedStatus)
    {
        // only allow this to run if this isn't locked.
        controller.Write(settings.Lights[pinIndex], PinValue.High);
        lockedStatus = true;
        // if (settings.OnlineInd)
        // {
        //     eventHub.InvokeAsync("buzz", settings.Colors[pinIndex]);
        // }
        Thread.Sleep(5000);
        controller.Write(settings.Lights[pinIndex], PinValue.Low);
    }
}