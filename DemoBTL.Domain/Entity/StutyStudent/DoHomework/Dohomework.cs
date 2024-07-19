namespace DemoBTL.Domain.Entity.StutyStudent.DoHomework
{
    public class Dohomework : BaseEntity
    {
        public int RegisterStudyId { get; set; }
        public virtual RegisterStudy? RegisterStudy { get; set; }

    }
}
