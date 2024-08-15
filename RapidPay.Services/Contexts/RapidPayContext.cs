using Microsoft.EntityFrameworkCore;
using RapidPay.Services.Entities;

namespace RapidPay.Services.Contexts;

public class RapidPayContext : DbContext
{
    public RapidPayContext(DbContextOptions<RapidPayContext> options) : base(options)
    {

    }

    public DbSet<Card> Cards { get; set; }

    public DbSet<Ledger> Ledgers { get; set; }

    public DbSet<SubLedger> SubLedgers { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User(userId: 1, userName: "sergio", hashPassword: "48-FA-F5-89-CB-AD-0A-A4-78-02-DA-41-F1-1B-6B-A2-4E-F7-71-97-5B-4D-64-DF-D6-9C-C0-6B-BA-83-62-49") //"48FAF589CBAD0AA47802DA41F11B6BA24EF771975B4D64DFD69CC06BBA836249")
        );
        modelBuilder.Entity<Card>().HasData(
            new Card(cardId: 1, userId: 1, credit: 10000, cardNumber: "411111111111111", cvv: "123", expirationMonth: 12, expirationYear: 2030)
        );
        modelBuilder.Entity<Ledger>().HasData(
            new Ledger(ledgerId: 2024, year: 2024, period: 8, balance: null)
        );
        modelBuilder.Entity<SubLedger>().HasData(
            // 1%
            new SubLedger(SubLedgerId: 1, ledgerId: 2024, cardId: 1, when: new DateTime(2024, 8, 2, 11, 33, 0), amount: 100, fee: 1, feeAmount: 100 * (1m / 100)),
            new SubLedger(SubLedgerId: 2, ledgerId: 2024, cardId: 1, when: new DateTime(2024, 8, 5, 10, 12, 0), amount: 200, fee: 2, feeAmount: 200 * (1m / 100)),
            new SubLedger(SubLedgerId: 3, ledgerId: 2024, cardId: 1, when: new DateTime(2024, 8, 9, 11, 33, 0), amount: 1000, fee: 10, feeAmount: 1000 * (1m / 100)),
            new SubLedger(SubLedgerId: 4, ledgerId: 2024, cardId: 1, when: new DateTime(2024, 8, 12, 10, 12, 0), amount: 2000, fee: 20, feeAmount: 2000 * (1m / 100)),
            // 2%
            new SubLedger(SubLedgerId: 5, ledgerId: 2024, cardId: 1, when: new DateTime(2024, 8, 9, 11, 33, 0), amount: 1000, fee: 20, feeAmount: 1000 * (2m / 100)),
            new SubLedger(SubLedgerId: 6, ledgerId: 2024, cardId: 1, when: new DateTime(2024, 8, 12, 10, 12, 0), amount: 2000, fee: 40, feeAmount: 2000 * (2m / 100))
        );
        base.OnModelCreating(modelBuilder);
    }
}
