using System.Security.Claims;

namespace RapidPay.Services.Contracts;

public interface ISecurityRepository
{
    Task<string> GenerateTokenAsync(string username, string password);

    bool IsValidToken(string token);
}
