﻿namespace Core.Entities.OrderAggregate;

public class DeliveryMethod
{
    public int Id { get; set; }
    public string ShortName { get; set; } = null!;
    public string DeliveryTime { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}