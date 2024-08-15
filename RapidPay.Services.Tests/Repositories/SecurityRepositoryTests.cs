using RapidPay.Services.Contexts;
using Microsoft.EntityFrameworkCore;
using RapidPay.Services.Contracts;
using RapidPay.Services.Repositories;

namespace RapidPay.Services.Tests.Repositories;

[TestClass]
public class SecurityRepositoryTests
{
    private RapidPayContext context;

    public SecurityRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RapidPayContext>()
            .UseInMemoryDatabase("RapidPayDBUAT")
            .Options; ;
        this.context = new RapidPayContext(options);
        this.context.Database.EnsureCreated();
    }

    [TestMethod]
    public async Task GenerateTokenAsync_Test()
    {
        // Arrange
        ISecurityRepository userRepository = new SecurityRepository(this.context);
        string username = "sergio";
        string password = "santana";      

        // Act
        string token = await userRepository.GenerateTokenAsync(username, password);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(token));
    }
}
