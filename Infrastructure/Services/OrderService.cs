using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class OrderService: IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
        Address shippingAddress)
    {
        // get basket from repo 
        // get items from product repo
        var items = new List<OrderItem>();
       
        // get dm from repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
        // calc subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        // create order
        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod!, subtotal);
        _unitOfWork.Repository<Order>().Add(order);
        //save to db
        var result = await _unitOfWork.Complete();
        // return order
        return result <= 0 ? null : order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
        return await _unitOfWork.Repository<Order>().ListAsyncWithSpec(spec);
    }

    public async Task<Order?> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
        return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }
}