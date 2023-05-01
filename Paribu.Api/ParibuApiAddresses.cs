namespace Paribu.Api;

public class ParibuApiAddresses
{
    public string ApiAddress { get; set; }
    public string WebsocketAddress { get; set; }

    public static ParibuApiAddresses Default = new()
    {
        ApiAddress = "https://app.paribu.com/",
        WebsocketAddress = "wss://ws-eu.pusher.com/app/9583280bf9e54779ac66?protocol=7&client=js&version=7.6.0&flash=false",
    };
}