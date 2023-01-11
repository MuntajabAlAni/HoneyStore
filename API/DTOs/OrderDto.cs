namespace API.DTOs;

public class OrderDto
{
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; } = null!;
}