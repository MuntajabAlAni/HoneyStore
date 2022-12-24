namespace Core.Entities;

public class ProductImages : BaseEntity
{
    public string PictureUrl { get; set; }  = null!;
    public int ProductId { get; set; }
}