using System.Net;
using System.Text;

namespace Paribu.Api;

public class ParibuAuthentication(ApiCredentials credentials) : AuthenticationProvider(credentials)
{
    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        // Check Point
        if (!signed)
            return;

        // Check Point
        if (Credentials == null || Credentials.Key == null || Credentials.Secret == null)
            throw new ArgumentException("Api Key/Secret needed");

        // Api Key
        var apikey = Credentials.Key.GetString();
        if (string.IsNullOrEmpty(apikey))
            throw new ArgumentException("Api Key/Secret needed");

        // Api Secret
        var apisecret = Credentials.Secret.GetString();
        if (string.IsNullOrEmpty(apisecret))
            throw new ArgumentException("Api Key/Secret needed");

        // Set Uri Parameters
        uri = uri.SetParameters(query, serialization);

        // Signature
        var queryString = uri.Query.Replace("?", "") ?? "";
        var requestBody = bodyContent ?? "";
        var dataToSign = queryString + requestBody;
        var signature = SignHMACSHA256(dataToSign, SignatureOutputType.Base64);

        // Headers
        //headers.Add("Content-Type", "application/json");
        //headers.Add("Authorization", WebUtility.UrlEncode(apikey));
        //headers.Add("X-Signature", WebUtility.UrlEncode(signature));
        headers.Add("Authorization", apikey);
        headers.Add("X-Signature", signature);
    }
}