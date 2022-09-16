namespace Core.Entities;

public class ProductImages : BaseEntity
{
    public string pictureUrl { get; set; }
    public int productId { get; set; }
    public Product Product { get; set; }
}