using Microsoft.EntityFrameworkCore;
using RapidPay.Services.Contexts;
using RapidPay.Services.Contracts;
using RapidPay.Services.Entities;
using RapidPay.Services.Models;
using RapidPay.Services.Repositories;

namespace RapidPay.Services.Tests.Repositories;

[TestClass]
public class CardRepositoryTests
{
    private RapidPayContext context;

    public CardRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RapidPayContext>()
            .UseInMemoryDatabase("RapidPayDBUAT")
            .Options; ;
        this.context = new RapidPayContext(options);
        this.context.Database.EnsureCreated();
    }

    [TestMethod]
    public async Task CreateCardAsync_Test()
    {
        // Arrange
        var user = new User()
        {
            UserName = "Test",
            HashPassword = "1234567",
        };
        this.context.Users.Add(user);
        this.context.SaveChanges();
        ICardRepository cardRepository = new CardRepository(this.context);

        string cardNumber = "522222222222222",
               cvv = "123";
        sbyte expirationMonth = 1;
        short expirationYear = 2024;

        CardDTO cardDto = new CardDTO(
            userId: user.UserId,
            cardNumber: cardNumber,
            cvv: cvv,
            expirationMonth: expirationMonth,
            expirationYear: expirationYear
        );

        // Act
        var card = await cardRepository.CreateCardAsync(cardDto);

        // Assert
        Assert.IsNotNull(card, "Card must not be null");
        Assert.AreEqual(cardDto.CardNumber, card.CardNumber, "CardNumber must be equal");
        Assert.AreEqual(cardDto.CVV, card.CVV, "CVV must be equal");
        Assert.AreEqual(cardDto.ExpirationMonth, card.ExpirationMonth, "ExpirationMonth must be equal");
        Assert.AreEqual(cardDto.ExpirationYear, card.ExpirationYear, "ExpirationMonth must be equal");
    }
}
