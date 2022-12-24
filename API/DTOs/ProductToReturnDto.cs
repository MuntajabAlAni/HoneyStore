using Core.Entities;

namespace API.DTOs;

public class ProductToReturnDto : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
    public string? ProductType {get; set;}
    public string? ProductCollection { get; set; }

}