namespace Paribu.Api;

public class ParibuRestClientOptions : RestApiClientOptions
{
    public static ParibuRestClientOptions Default { get; set; } = new();

    public ParibuRestClientOptions() : base()
    {
        this.BaseAddress = ParibuApiAddresses.Default.ApiAddress;
    }
}