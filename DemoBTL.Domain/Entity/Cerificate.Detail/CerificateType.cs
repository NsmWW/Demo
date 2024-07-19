namespace DemoBTL.Domain.Entity.Cerificate.Detail
{
    public class CerificateType : BaseEntity
    {
        public string Name { get; set; }
        public int CertificateId { get; set; }
        public virtual Certificate? Certificate { get; set; }
    }
}
