using Microsoft.EntityFrameworkCore;
using RapidPay.Services.Contexts;
using RapidPay.Services.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace RapidPay.Services.Repositories;

public class SecurityRepository : ISecurityRepository
{
    private readonly RapidPayContext context;
    private readonly string SECRET_KEY = Guid.NewGuid().ToString();

    public SecurityRepository(RapidPayContext context)
    {
        this.context = context;
    }

    public async Task<string> GenerateTokenAsync(string username, string password)
    {
        if (!await VerifyCredentialsAsync(username, password))
        {
            throw new UnauthorizedAccessException("Not Authorized");
        }

        string token = GenerateToken(username);

        return token;
    }

    private async Task<bool> VerifyCredentialsAsync(string username, string password)
    {
        string hashPassword = GetHashPassword(password);
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.HashPassword == hashPassword);
        return user != null;
    }

    private string GetHashPassword(string password)
    {
        string hashPassword = string.Empty;

        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            hashPassword = BitConverter.ToString(bytes);
        }

        return hashPassword;
    }

    private string GenerateToken(string username)
    {
        return $"{username.GetHashCode()}.{DateTime.UtcNow.AddHours(8)}";
    }

    public bool IsValidToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return false;
        var sections = token.Split('.');

        if (string.IsNullOrEmpty(sections[0]))
            return false;

        DateTime.TryParse(sections[1], out DateTime expirationDate);
        if (DateTime.UtcNow > expirationDate)
            return false;

        return true;
    }
}
