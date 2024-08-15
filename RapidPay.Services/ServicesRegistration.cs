using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RapidPay.Services.Contexts;
using RapidPay.Services.Contracts;
using RapidPay.Services.Repositories;

namespace RapidPay.Services;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<RapidPayContext>(options => options.UseInMemoryDatabase("RapidPayDB"));
        services.BuildServiceProvider().GetService<RapidPayContext>().Database.EnsureCreated();
        services.AddMemoryCache();
        services.AddScoped<ISecurityRepository, SecurityRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        return services;
    }
}
