using Core.Entities.Identity;

namespace Core.Entities;

public class Post :BaseEntity
{
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;
    public string PictureUrl { get; set; }  = null!;
    public DateTime PublishDate { get; set; }
    public int Type { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; } = false;
}