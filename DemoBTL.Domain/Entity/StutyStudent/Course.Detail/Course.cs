using DemoBTL.Domain.Entity.StutyStudent.Pay;

namespace DemoBTL.Domain.Entity.StutyStudent.Course.Detail
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Introduce { get; set; }
        public string? ImageCouse { get; set; }
        public string? Creatorld { get; set; }  //không hiểu
        public string? Code { get; set; }
        public decimal? Price { get; set; }
        public int? TotalCourseDuration { get; set; }
        public int? NumberOfStudent { get; set; }
        public int? NumberOfPurchases { get; set; }

        public virtual ICollection<CourseSubject>? CourseSubjects { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
