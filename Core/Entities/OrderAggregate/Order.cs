namespace Core.Entities.OrderAggregate;

public class Order: BaseEntity
{
    public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress,
        DeliveryMethod deliveryMethod, decimal subtotal)
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
    }

    public Order()
    {
    }

    public string BuyerEmail { get; set; } = null!;
    public string OrderDate { get; set; } = DateTime.Now.ToString();
    public Address ShipToAddress { get; set; } = null!;
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public IReadOnlyList<OrderItem> OrderItems { get; set; } = null!;
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // for AutoMapper !!
    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }
}