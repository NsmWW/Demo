namespace DemoBTL.Domain.Entity.Blogs
{
    public class Blog : BaseEntity
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int? NumberifLikes { get; set; }
        public int? NumberofComment { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int UserId { get; set; }      // tìm hiểm cách đổi tên
        public User? User { get; set; }

    }
}
