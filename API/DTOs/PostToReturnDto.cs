using Core.Entities;

namespace API.DTOs
{
    public class PostToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }  = null!;
        public string Description { get; set; }  = null!;
        public string PictureUrl { get; set; }  = null!;
        public DateTime PublishDate { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int ReceiptField { get; set; }
    }
}
