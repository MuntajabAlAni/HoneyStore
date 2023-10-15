using Core.Entities;

namespace API.DTOs;

public class OrderDto
{
    public AddressDto ShipToAddress { get; set; } = null!;
    public CustomerBasket Basket { get; set; } = null!;
}