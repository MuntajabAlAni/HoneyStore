using Core.Entities.Identity;

namespace Core.Entities;

public class Post 
{
    public int Id { get; set; }
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;
    public string PictureUrl { get; set; }  = null!;
    public DateTime PublishDate { get; set; }
    public int Type { get; set; }
    public int UserId { get; set; }
    public int ReceiptField { get; set; }
    public bool IsDeleted { get; set; } = false;
}