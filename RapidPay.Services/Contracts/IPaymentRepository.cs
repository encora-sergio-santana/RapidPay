namespace RapidPay.Services.Contracts;

public interface IPaymentRepository
{
    Task<string> PayAsync(string username, string cardNumber, decimal amount, string cvv, sbyte expirationMonth, short expirationYear);

    Task<decimal> GetBalanceAsync(string userName);
}
