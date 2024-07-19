namespace DemoBTL.Domain.Entity.Address.W.D.P
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<District>? Districts { get; set; }
    }
}
