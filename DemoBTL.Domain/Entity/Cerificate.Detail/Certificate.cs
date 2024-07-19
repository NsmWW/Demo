namespace DemoBTL.Domain.Entity.Cerificate.Detail
{
    public class Certificate : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<CerificateType> CerificateTypes { get; set; }
        public int userId { get; set; }
        public virtual User User { get; set; }

    }
}
