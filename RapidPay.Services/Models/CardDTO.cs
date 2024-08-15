using System.ComponentModel.DataAnnotations;

namespace RapidPay.Services.Models;

public class CardDTO
{
    public int? CardId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(15)]
    public string CardNumber { get; set; }

    [Required]
    [MaxLength(3)]
    public string CVV { get; set; }

    [Required]
    [Range(1,12)]
    public sbyte ExpirationMonth { get; set; }

    [Required]
    [Range(2024, 2100)]
    public short ExpirationYear { get; set; }

    public CardDTO(int userId, string cardNumber, string cvv, sbyte expirationMonth, short expirationYear, int? cardId = null)
    {
        if(cardId != null)
            CardId = cardId;
        UserId = userId;
        CardNumber = cardNumber;
        CVV = cvv;
        ExpirationMonth = expirationMonth;
        ExpirationYear = expirationYear;
    }
}
