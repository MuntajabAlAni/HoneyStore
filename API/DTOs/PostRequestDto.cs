namespace API.DTOs;

public class PostRequestDto
{
    public string Title { get; set; }= null! ;
    public string Description { get; set; }= null! ;
    public IFormFile Image { get; set; }= null! ;
    public DateTime PublishDate { get; set; }
}