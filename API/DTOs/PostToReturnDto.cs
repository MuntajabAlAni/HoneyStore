using Core.Entities;

namespace API.DTOs
{
    public class PostToReturnDto: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
