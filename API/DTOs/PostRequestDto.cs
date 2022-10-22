namespace API.DTOs;

public class PostRequestDto
{
    public int? Id { get; set; }
    public string Title { get; set; }= null! ;
    public string Description { get; set; }= null! ;
    public IFormFile Image { get; set; }= null! ;
    public DateTime PublishDate { get; set; }
    public int Type { get; set; }
}