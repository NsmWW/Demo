namespace DemoBTL.Domain.Entity.StutyStudent.Course.Detail
{
    public class LearningProgres : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? RegisterStudyId { get; set; }
        public virtual RegisterStudy? RegisterStudy { get; set; }

        public int? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
