namespace Paribu.Api.Authentication;

public class ParibuAuthenticationProvider : AuthenticationProvider
{
    public ParibuAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
    }

    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        // headers.Add("user-agent", "ParibuApp/345 (Android 12)");
        // headers.Add("x-app-version", "345");
        // headers.Add("x-device", "Paribu.Net");
        // headers.Add("pragma-cache-local", ((ParibuClientSingleApi)apiClient)._baseClient.DeviceId);

        // Check Point
        if (!signed) return;

        // Authorization
        headers.Add("Authorization", $"Bearer {Credentials.Key.GetString()}");
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