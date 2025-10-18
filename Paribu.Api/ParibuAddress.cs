namespace Paribu.Api;

public class ParibuAddress
{
    public string ApiAddress { get; set; } = "";
    public string AppAddress { get; set; } = ""; // Legacy
    public string WebAddress { get; set; } = ""; // Legacy
    public string ParibuStreamAddress { get; set; } = "";
    public string PusherStreamAddress { get; set; } = ""; // Legacy

    public static ParibuAddress Default = new()
    {
        ApiAddress = "https://api.paribu.com",
        AppAddress = "https://app.paribu.com",
        WebAddress = "https://web.paribu.com",
        ParibuStreamAddress = "wss://stream.paribu.com",
        PusherStreamAddress = "wss://ws-eu.pusher.com/app/9583280bf9e54779ac66?protocol=7&client=js&version=8.4.0&flash=false",
    };
}

// pusher-dotnet-client