namespace DemoBTL.Domain.Entity.Blogs
{
    public class LikeBlog : BaseEntity
    {
        public virtual ICollection<User>? UserId { get; set; }
        public virtual ICollection<Blog>? BlogId { get; set; }
        public int Unlike { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
