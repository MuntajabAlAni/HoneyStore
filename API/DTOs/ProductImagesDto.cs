namespace API.DTOs;

public class ProductImagesDto
{
    public IFormFile Image { get; set; } = null!;
    public bool IsMain { get; set; } = false;
}