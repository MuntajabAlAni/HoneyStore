using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly string _webhookSecret;
        private readonly ILogger<IPaymentService> _logger;
        public PaymentsController(IPaymentService paymentService, ILogger<IPaymentService> logger, IConfiguration configuration)
        {
            _webhookSecret = configuration.GetSection("StripeSettings:WebHookSecret").Value;
            _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost()]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(CustomerBasket basket)
        {
            var updatedBasket = await _paymentService.CreateOrUpdatePaymentIntent(basket);
            return updatedBasket == null ? BadRequest(new ApiResponse(400, "Problem with your basket")) : updatedBasket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var jason = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(jason, Request.Headers["Stripe-Signature"], _webhookSecret);
            PaymentIntent intent;
            Order? order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded: {IntentId}", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: {OrderId}", order!.Id);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed: {IntentId}", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Order updated to payment failed: {OrderId}", order!.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}