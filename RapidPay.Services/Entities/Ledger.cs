using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Services.Entities;

public class Ledger
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LedgerId { get; set; }

    [Required]
    public int Year { get; set; }

    [Required]
    public int Period { get; set; }

    public decimal? Balance { get; set; }

    public ICollection<SubLedger> SubLedgers { get; set; }

    public Ledger()
    {

    }

    public Ledger(int year, int period, decimal? balance, int? ledgerId = null)
    {
        if (ledgerId != null)
            LedgerId = (int)ledgerId;
        Year = year;
        Period = period;
        Balance = balance;
    }
}
