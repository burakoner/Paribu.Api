namespace Paribu.Api;

public class ParibuStreamClientOptions : WebSocketApiClientOptions
{
    public static ParibuStreamClientOptions Default { get; set; } = new();

    public ParibuStreamClientOptions() : base()
    {
        this.BaseAddress = ParibuApiAddresses.App.WebsocketAddress;
    }
}