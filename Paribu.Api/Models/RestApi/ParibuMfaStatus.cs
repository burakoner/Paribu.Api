namespace Paribu.Api.Models.RestApi;

public class ParibuMfaStatus
{
    public int TTL { get; set; }
    public string Provider { get; set; }

    [JsonProperty("verify_token")]
    public string VerificationToken { get; set; }

    public ParibuMfaSteps Steps { get; set; }
}

public class ParibuMfaSteps
{
    public int Total { get; set; }
    public int Current { get; set; }
}