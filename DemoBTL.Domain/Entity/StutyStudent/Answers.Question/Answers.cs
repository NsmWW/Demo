namespace DemoBTL.Domain.Entity.StutyStudent.Answers.Question
{
    public class Answers : BaseEntity
    {
        public string Answer { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int QuestionId { get; set; }
        public MakeQuestion? Question { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
