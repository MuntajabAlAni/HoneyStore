namespace API.DTOs;

public class PostRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public DateTime PublishDate { get; set; }
}