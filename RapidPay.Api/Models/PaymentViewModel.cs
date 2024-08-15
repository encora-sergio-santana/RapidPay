using System.ComponentModel.DataAnnotations;

namespace RapidPay.Api.Models;

public class PaymentViewModel
{
    [Required]
    [MaxLength(15)]
    public string CardNumber { get; set; }
    [Required]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(3)]
    public string Cvv { get; set; }
    [Required]
    [Range(1, 12)]
    public sbyte ExpirationMonth { get; set; }
    [Required]
    public short ExpirationYear { get; set; }
}
