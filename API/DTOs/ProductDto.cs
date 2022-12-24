namespace API.DTOs;

public class ProductDto
{
    public int? Id { get; set; }
    public string Name { get; set; } = null! ;
    public string Description { get; set; } = null! ;
    public string Link { get; set; } = null! ;
    public decimal Price { get; set; }
    public List<ProductImagesDto> Images { get; set; } = new List<ProductImagesDto>();
    public int ProductTypeId { get; set; }
    public int ProductCollectionId { get; set; }
}