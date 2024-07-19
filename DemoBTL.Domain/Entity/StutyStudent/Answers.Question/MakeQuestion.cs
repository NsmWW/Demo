namespace DemoBTL.Domain.Entity.StutyStudent.Answers.Question
{
    public class MakeQuestion : BaseEntity
    {
        public string Question { get; set; }
        public int NumberOfAnswers { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual ICollection<Answers>? Answers { get; set; }

    }
}
