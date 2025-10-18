namespace Paribu.Api;

public class ParibuRestClientOptions : RestApiClientOptions
{
    public ParibuRestClientOptions() : this("", "")
    {
    }

    public ParibuRestClientOptions(string apikey, string secret) : this(new ApiCredentials(apikey, secret))
    {
    }

    public ParibuRestClientOptions(ApiCredentials credentials)
    {
        ApiCredentials = credentials;
        FailOnEmptyResponse = false;
    }
}