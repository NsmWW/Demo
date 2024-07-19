using System.ComponentModel.DataAnnotations;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Resquest_Login
    {
        [Required(ErrorMessage = "username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "password")]
        public string Password { get; set; }
    }
}
