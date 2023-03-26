namespace Paribu.Api;

public class ParibuApiAddresses
{
    public string ApiAddress { get; set; }
    public string WebsocketAddress { get; set; }

    public static ParibuApiAddresses Default = new ParibuApiAddresses
    {
        ApiAddress = "https://v3.paribu.com/app",
        WebsocketAddress = "wss://ws-eu.pusher.com/app/a68d528f48f652c94c88?protocol=7&client=js&version=5.1.1&flash=false"
    };
}