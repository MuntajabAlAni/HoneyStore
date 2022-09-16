namespace API.DTOs;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<ProductImagesDto> Images { get; set; }
    public int ProductTypeId { get; set; }
    public int ProductCollectionId { get; set; }
}