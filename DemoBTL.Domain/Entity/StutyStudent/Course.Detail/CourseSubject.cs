namespace DemoBTL.Domain.Entity.StutyStudent.Course.Detail
{
    public class CourseSubject : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
