namespace Core.Entities;

public class PostReceiptFields : BaseEntity
{
    public Post Post { get; set; } = null!;
    public int PostId { get; set; }
    public int ReceiptFieldId { get; set; }
}