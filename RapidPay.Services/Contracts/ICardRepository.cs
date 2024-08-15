using RapidPay.Services.Models;

namespace RapidPay.Services.Contracts;

public interface ICardRepository
{
    Task<CardDTO> CreateCardAsync(CardDTO card);
}
