using RapidPay.Services.Contexts;
using RapidPay.Services.Contracts;
using RapidPay.Services.Entities;
using RapidPay.Services.Models;

namespace RapidPay.Services.Repositories;

public class CardRepository : ICardRepository
{
    private readonly RapidPayContext context;

    public CardRepository(RapidPayContext context)
    {
        this.context = context;
    }

    public async Task<CardDTO> CreateCardAsync(CardDTO cardDto)
    {
        var card = new Card(
            cardId: cardDto.CardId,
            userId: cardDto.UserId,
            cardNumber: cardDto.CardNumber,
            cvv: cardDto.CVV,
            expirationMonth: cardDto.ExpirationMonth,
            expirationYear: cardDto.ExpirationYear
        );

        await context.Cards.AddAsync(card);
        await context.SaveChangesAsync();

        return new CardDTO(
            cardId: card.CardId,
            userId: card.UserId,
            cardNumber: card.CardNumber,
            cvv: card.Cvv,
            expirationMonth: card.ExpirationMonth,
            expirationYear: card.ExpirationYear
        );
    }
}
