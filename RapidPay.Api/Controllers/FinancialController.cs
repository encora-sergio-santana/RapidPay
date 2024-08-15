using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.Filters;
using RapidPay.Api.Models;
using RapidPay.Services.Contracts;
using System.Web.Http.ModelBinding;

namespace RapidPay.Api.Controllers
{
    [Route("Financial")]
    public class FinancialController : Controller
    {
        private IPaymentRepository paymentRepository { get; }

        public FinancialController(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ServiceFilter(typeof(TokenAuthFilter))]
        public async Task<IActionResult> PaymentView()
        {
            return View();
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ServiceFilter(typeof(TokenAuthFilter))]
        public async Task<IActionResult> PaymentPostView(PaymentViewModel vmPayment)
        {
            try
            {
                string paymentResponse = await Pay(vmPayment);

                ViewBag.PaymentMessage = paymentResponse;

                if (string.IsNullOrEmpty(paymentResponse))
                {
                    return RedirectToAction("BalanceView", "Financial");
                }
            }
            catch(Exception ex)
            {
                ViewBag.PaymentMessage = ex.Message;
            }
            return View(vmPayment);
        }

        private async Task<string> Pay(PaymentViewModel vmPayment)
        {
            string paymentResponse;
            if (ModelState.IsValid)
            {
                HttpContext.Request.Cookies.TryGetValue("username", out string username);
                    paymentResponse = await paymentRepository.PayAsync(
                        username: username,
                        cardNumber: vmPayment.CardNumber,
                        amount: vmPayment.Amount,
                        cvv: vmPayment.Cvv,
                        expirationMonth: vmPayment.ExpirationMonth,
                        expirationYear: vmPayment.ExpirationYear
                    );
                    return paymentResponse;
            }
            else
            {
                paymentResponse = string.Join("\n", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }
            return paymentResponse;
        }

        [HttpPost]
        [Route(nameof(PaymentPost))]
        [ServiceFilter(typeof(TokenAuthFilter))]
        public async Task<IActionResult> PaymentPost(PaymentViewModel vmPayment)
        {
            try
            {
                string paymentResponse = await Pay(vmPayment);

                ViewBag.PaymentMessage = paymentResponse;

                decimal balance = await GetBalanceAsync();

                if (string.IsNullOrEmpty(paymentResponse))
                {
                    return Ok(balance);
                }

                return BadRequest(paymentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(Balance))]
        [ServiceFilter(typeof(TokenAuthFilter))]
        public async Task<IActionResult> Balance()
        {
            try
            {
                decimal balance = await GetBalanceAsync();

                return Ok(balance);
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(BalanceView))]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ServiceFilter(typeof(TokenAuthFilter))]
        public async Task<IActionResult> BalanceView()
        {
            decimal balance = await GetBalanceAsync();

            return View(new BalanceViewModel() { Balance = balance });
        }

        private async Task<decimal> GetBalanceAsync()
        {
            HttpContext.Request.Cookies.TryGetValue("username", out string username);

            return await paymentRepository.GetBalanceAsync(username);
        }
    }
}
