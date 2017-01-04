namespace Bicimad.Services.Query.Interfaces
{
    public interface ISecurityQueryService
    {
        bool IsTokenValid(string token);
    }
}