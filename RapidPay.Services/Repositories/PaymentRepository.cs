using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RapidPay.Services.Contexts;
using RapidPay.Services.Contracts;
using RapidPay.Services.Entities;

namespace RapidPay.Services.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly RapidPayContext context;
    private readonly IMemoryCache memoryCache;
    private readonly string PAYMENT_FEE = "payment_fee";
    private decimal lastFee = 0;

    public PaymentRepository(RapidPayContext context, IMemoryCache memoryCache)
    {
        this.context = context;
        this.memoryCache = memoryCache;
    }

    public async Task<string> PayAsync(string username, string cardNumber, decimal amount, string cvv, sbyte expirationMonth, short expirationYear)
    {
        decimal balance = await GetBalanceAsync(username);

        var cardInfo = await context.Cards.FirstOrDefaultAsync(c => c.Cvv == cvv &&
            c.CardNumber == cardNumber &&
            c.ExpirationMonth == expirationMonth &&
            c.ExpirationYear == expirationYear);

        if (cardInfo == null)
            return "Invalid card infomation";

        if (cardInfo.Credit < balance + amount)
            return "Insufficient funds";

        decimal feePercentage = GetFeePercentage();
        decimal feeAmount = feePercentage == 0 ? 0 : amount * (feePercentage / 100);

        var subledger = new SubLedger(ledgerId: 2024, cardId: cardInfo.CardId, when: DateTime.UtcNow, amount: amount, fee: feePercentage, feeAmount: feeAmount);

        context.SubLedgers.Add(subledger);
        context.SaveChanges();

        return null;
    }

    public async Task<decimal> GetBalanceAsync(string username)
    {
        decimal balance = await context.SubLedgers
            .AsNoTracking()
            .Include(s => s.Ledger)
            .Include(s => s.Card).ThenInclude(c => c.User)
            .Where(r => r.Ledger.Year == 2024 && r.Ledger.Period == 8 && r.Card.User.UserName == username)
            .SumAsync(s => s.Amount + s.FeeAmount);

        return balance;
    }

    private decimal GetFeePercentage()
    {
        this.memoryCache.TryGetValue(PAYMENT_FEE, out decimal? fee);
        if (fee == null)
        {
            fee = (decimal)(new Random().Next(0, 200) / 100d);
            if (lastFee == 0)
                lastFee = (decimal)fee;
            else
                lastFee *= (decimal)fee;

            this.memoryCache.Set(PAYMENT_FEE, lastFee, TimeSpan.FromHours(1));
        }
            
        return (decimal)fee;
    }
}
