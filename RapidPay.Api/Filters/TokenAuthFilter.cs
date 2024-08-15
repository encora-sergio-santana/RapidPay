using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RapidPay.Services.Contracts;

namespace RapidPay.Api.Filters;

public class TokenAuthFilter : IAuthorizationFilter
{
    private readonly ISecurityRepository _userRepository;
    public TokenAuthFilter(ISecurityRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Cookies.TryGetValue("Authorization", out string token);
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        bool isValidToken = _userRepository.IsValidToken(token);
        if (!isValidToken)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
