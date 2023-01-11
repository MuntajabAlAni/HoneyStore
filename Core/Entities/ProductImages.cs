namespace Core.Entities;

public class ProductImages 
{
    public int Id { get; set; }
    public string PictureUrl { get; set; }  = null!;
    public int ProductId { get; set; }
}