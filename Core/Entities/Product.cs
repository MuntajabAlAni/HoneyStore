namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }  = null!;
    public string Description { get; set; }  = null!;
    public string Link { get; set; }  = null!;
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }  = null!;
    public ProductType ProductType {get; set;}  = null!;
    public int ProductTypeId { get; set; }
    public ProductCollection ProductCollection { get; set; }  = null!;
    public int ProductCollectionId { get; set; }
    public List<ProductImages> ProductImages { get; set; }  = null!;
    public bool IsDeleted { get; set; } = false;
}