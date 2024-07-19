namespace DemoBTL.Domain.Entity.StutyStudent.Course.Detail
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool IsActivce { get; set; } = false;

        public virtual ICollection<CourseSubject>? CourseSubjects { get; set; }
        public virtual ICollection<SubjectDetail>? SubjectDetails { get; set; }
        public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; }
        public virtual ICollection<LearningProgres>? LearningProgres { get; set;}
    }
}
