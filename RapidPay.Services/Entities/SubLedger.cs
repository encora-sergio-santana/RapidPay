using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Services.Entities;

public class SubLedger
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubLedgerId { get; set; }

    [Required]
    public DateTime When { get; set; }

    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public decimal Fee { get; set; }
    
    [Required]
    public decimal FeeAmount { get; set; }

    [Required]
    public int LedgerId { get; set; }
    public Ledger Ledger { get; set; }

    [Required]
    public int CardId { get; set; }
    public Card Card { get; set; }

    public SubLedger()
    {

    }

    public SubLedger(int ledgerId, int cardId, DateTime when, decimal amount, decimal fee, decimal feeAmount, int? SubLedgerId = null)
    {
        if (SubLedgerId != null)
            this.SubLedgerId = (int)SubLedgerId;
        LedgerId = ledgerId;
        CardId = cardId;
        When = when;
        Amount = amount;
        Fee = fee;
        FeeAmount = feeAmount;
    }
}
