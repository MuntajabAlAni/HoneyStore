namespace API.DTOs;

public class ProductImagesDto
{
    public IFormFile Image { get; set; }
    public bool IsMain { get; set; }
}