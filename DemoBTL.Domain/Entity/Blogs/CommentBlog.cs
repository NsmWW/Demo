namespace DemoBTL.Domain.Entity.Blogs
{
    public class CommentBlog : BaseEntity
    {
        public int Content { get; set; }
        public int Edited { get; set; }
        public virtual ICollection<User>? UserId { get; set; }
        public virtual ICollection<Blog>? BlogId { get; set; }
    }
}
