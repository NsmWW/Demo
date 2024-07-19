using DemoBTL.Domain.Entity.StutyStudent.Course.Detail;
using DemoBTL.Domain.Entity.StutyStudent.DoHomework;

namespace DemoBTL.Domain.Entity.StutyStudent
{
    public class RegisterStudy : BaseEntity
    {

        public int CouserId { get; set; }
        public bool IsFinished { get; set; } = false;
        public DateTime? Registertime { get; set; }
        public int? PercentComplete { get; set; }
        public DateTime? DoneTime { get; set; }
        public bool IsActive { get; set; } = false;


        public int SubjectId { get; set; }     // cần đổi tên sử dung fluae API 
        public virtual Subject? Subject { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Dohomework>? DoHomeworks { get; set; }
        public virtual ICollection<LearningProgres>? LearningProgres { get; set; }
    }
}
