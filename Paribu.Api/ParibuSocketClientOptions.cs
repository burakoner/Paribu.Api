namespace Paribu.Api;

public class ParibuSocketClientOptions : WebSocketApiClientOptions
{
    public static ParibuSocketClientOptions Default { get; set; } = new();

    public ParibuSocketClientOptions() : base()
    {
        this.BaseAddress = ParibuApiAddresses.App.WebsocketAddress;
    }
}