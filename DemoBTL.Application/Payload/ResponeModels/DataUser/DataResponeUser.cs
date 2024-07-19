namespace DemoBTL.Application.Payload.ResponeModels.DataUser
{
    public class DataResponeUser : DataResponeBase
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string UserStatus { get; set; }
    }
}
