using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class CustomerBasketDto
{
    [Required] public string Id { get; set; } = null!;
    public List<BasketItemDto> Items { get; set; } = null!;
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public decimal ShippingPrice { get; set; }
}