namespace DemoBTL.Domain.Entity.Address.W.D.P
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province? Province { get; set; }
    }
}
