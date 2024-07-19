namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Request_CreateNewPassword
    {
        public string ConfirmCode { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
