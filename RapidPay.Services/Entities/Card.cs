using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Services.Entities;

public class Card
{
    [Key]
    public int CardId { get; set; }

    [Required]
    public decimal Credit { get; set; }

    [Required]
    [MaxLength(15)]
    public string CardNumber { get; set; }

    [Required]
    [MaxLength(3)]
    public string Cvv { get; set; }

    [Required]
    public sbyte ExpirationMonth { get; set; }

    [Required]
    public short ExpirationYear { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public Card()
    {

    }

    public Card(string cardNumber, string cvv, sbyte expirationMonth, short expirationYear, int userId, decimal credit = 10000, int? cardId = null)
    {
        if (cardId != null)
            CardId = (int)cardId;

        UserId = userId;
        Credit = credit;
        CardNumber = cardNumber;
        Cvv = cvv;
        ExpirationMonth = expirationMonth;
        ExpirationYear = expirationYear;
    }
}