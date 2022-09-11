namespace API.DTOs;

public class OrderDto
{
    public string BasketId { get; set; } = null!;
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; } = null!;
}