namespace Paribu.Api.Authentication;

public class ParibuAuthenticationProvider : AuthenticationProvider
{
    public ParibuAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
    }

    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        // Check Point
        if (!signed) return;

        // Authorization
        headers.Add("Authorization", Credentials.Key.GetString());
    }

    public override void AuthenticateSocketApi()
    {
        throw new NotImplementedException();
    }

    public override void AuthenticateStreamApi()
    {
        throw new NotImplementedException();
    }
}