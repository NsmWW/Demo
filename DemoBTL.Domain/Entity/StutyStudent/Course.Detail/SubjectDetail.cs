using DemoBTL.Domain.Entity.StutyStudent.Answers.Question;
using DemoBTL.Domain.Entity.StutyStudent.DoHomework;

namespace DemoBTL.Domain.Entity.StutyStudent.Course.Detail
{
    public class SubjectDetail : BaseEntity
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; } = false;
        public string Linkvideo { get; set; }
        public bool IsActive { get; set; } = false;
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<Practice>? Practices { get; set; }
        public virtual ICollection<MakeQuestion>? MakeQuestions { get; set; }
    }
}
