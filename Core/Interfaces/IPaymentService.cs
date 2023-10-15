using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(CustomerBasket basket);
        Task<Order?> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order?> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}