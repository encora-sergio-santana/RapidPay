using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RapidPay.Services.Contexts;
using RapidPay.Services.Contracts;
using RapidPay.Services.Repositories;

namespace RapidPay.Services.Tests.Repositories;

[TestClass]
public class PaymentRepositoryTests
{
    private RapidPayContext context;
    private IMemoryCache memoryCache;

    public PaymentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RapidPayContext>()
            .UseInMemoryDatabase("RapidPayDBUAT")
            .Options; ;
        this.context = new RapidPayContext(options);
        this.context.Database.EnsureCreated();

        this.memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    [TestMethod]
    public async Task PayAsync_Test()
    {
        // Arrange
        IPaymentRepository paymentRepository = new PaymentRepository(this.context, this.memoryCache);
        string username = "sergio",
               cardNumber = "411111111111111",
               cvv = "123";
        decimal amount = 1000;
        sbyte expirationMonth = 12;
        short expirationYear = 2030;

        // Act
        string paymentResponse = await paymentRepository.PayAsync(
            username: username,
            cardNumber: cardNumber,
            amount: amount,
            cvv: cvv,
            expirationMonth: expirationMonth,
            expirationYear: expirationYear
        );

        // Assert
        Assert.IsTrue(string.IsNullOrEmpty(paymentResponse), "Must be success");
    }

    [TestMethod]
    public async Task GetBalanceAsync_Test()
    {
        // Arrange
        IPaymentRepository paymentRepository = new PaymentRepository(this.context, this.memoryCache);
        string username = "sergio";
        string usernameEmpty = "zero";

        // Act
        decimal balance = await paymentRepository.GetBalanceAsync( username );
        decimal balanceEmpty = await paymentRepository.GetBalanceAsync(usernameEmpty);

        // Assert
        Assert.IsTrue(balance > 0, "Balance must be greater than 0");
        Assert.IsTrue(balanceEmpty == 0, "Balance must be greater than 0");
    }
}