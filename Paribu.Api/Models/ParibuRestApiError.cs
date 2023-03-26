namespace Paribu.Api.Models;

public class ParibuRestApiError : Error
{
    public ParibuRestApiError(int? code, string message, object data) : base(code, message, data)
    {
    }
}
