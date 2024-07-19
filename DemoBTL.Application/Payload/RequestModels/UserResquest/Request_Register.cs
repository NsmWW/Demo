using System.ComponentModel.DataAnnotations;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Request_Register
    {
        [Required(ErrorMessage = "Email phải điền")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username phải điền")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password phải điền")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Tên phải điền")]
        public string Fullname { get; set; }
    }
}
