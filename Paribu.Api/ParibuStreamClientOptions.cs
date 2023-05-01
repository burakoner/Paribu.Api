namespace Paribu.Api;

public class ParibuStreamClientOptions : StreamApiClientOptions
{
    public static ParibuStreamClientOptions Default { get; set; } = new();

    public ParibuStreamClientOptions() : base()
    {
        this.BaseAddress = ParibuApiAddresses.App.WebsocketAddress;
    }
}