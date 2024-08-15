using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.Models;
using RapidPay.Services.Contracts;

namespace RapidPay.Api.Controllers;

[ApiController]
[Route("Home")]
public class HomeController : Controller
{
    private readonly ISecurityRepository _userRepository;

    public HomeController(ISecurityRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(nameof(Index))]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [Route($"{nameof(Login)}")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginViewModel vmLogin)
    {
        try
        {
            var token = await SetCookiesAndGetToken(vmLogin);
            var loginResponse = new LoginResponseModel() { Token = token };
            return Ok(loginResponse);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }

    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [Route($"{nameof(LoginView)}")]
    [HttpPost]
    public async Task<IActionResult> LoginView([FromForm] LoginViewModel vmLogin)
    {
        try
        {
            await SetCookiesAndGetToken(vmLogin);
            return RedirectToAction("BalanceView", "Financial");
        }
        catch (Exception ex)
        {
            return View("Index", vmLogin);
        }

    }

    private async Task<string> SetCookiesAndGetToken(LoginViewModel vmLogin)
    {
        if (!ModelState.IsValid)
            return string.Empty;

        var token = await _userRepository.GenerateTokenAsync(vmLogin.Username, vmLogin.Password);
        HttpContext.Response.Cookies.Append("Authorization", token, new CookieOptions
        { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });
        HttpContext.Response.Cookies.Append("username", vmLogin.Username, new CookieOptions
        { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });
        return token;
    }
}
