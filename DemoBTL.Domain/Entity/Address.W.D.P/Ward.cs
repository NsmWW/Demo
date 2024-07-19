namespace DemoBTL.Domain.Entity.Address.W.D.P
{
    public class Ward : BaseEntity
    {
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public virtual District? District { get; set; }
    }
}
