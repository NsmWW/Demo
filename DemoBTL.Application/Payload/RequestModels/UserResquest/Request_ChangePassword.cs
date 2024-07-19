namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Request_ChangePassword
    {
        public string OladPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
