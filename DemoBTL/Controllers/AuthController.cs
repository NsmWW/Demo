using DemoBTL.Application.ContansConnection;
using DemoBTL.Application.InterfaceService;
using DemoBTL.Application.Payload.RequestModels.UserResquest;
using DemoBTL.Application.Payload.ResponeModels.DataUser;
using DemoBTL.Application.Payload.Respones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.VisualBasic;

namespace DemoBTL.Controllers
{
    [Route(contans.defaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuService _uService;
        public AuthController(IAuService uService)
        {
            _uService = uService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Request_Register request_Register)
        {
            return Ok(await _uService.Register(request_Register));
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmRegister([FromBody] string confirmcod)
        {
            return Ok(await _uService.ConfirmRegister(confirmcod));
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Resquest_Login request)
        {
            return Ok(await _uService.Login(request));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] Request_ChangePassword request_ChangePassword)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _uService.ChangePassword(id, request_ChangePassword));

        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            return Ok(await _uService.ForgotPassword(email));
        }
        [HttpPut]
        public async Task<IActionResult> ConfirmCreatePassword([FromBody] Request_CreateNewPassword request_CreateNewPassword)
        {
            return Ok(await _uService.ConfirmCreatePassword(request_CreateNewPassword));
        }
        [HttpPost("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Addrole([FromRoute] int userId, [FromBody] List<string> roles)
        {
            return Ok(await _uService.Addrole(userId, roles));
        }
        [HttpPost("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Deleterole([FromRoute]int userId,[FromBody] List<string> roles)
        {
            return Ok(await _uService.Deleterole(userId, roles));   
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> updateuser([FromForm]ResquestUpdateUser updateUser)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _uService.updateuser(updateUser, Id));
        }
        [HttpGet]
        public async Task<IActionResult> DowloadFilee(string filenamee)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload//files", filenamee);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filenamee, out var contentType))
            {
                contentType = "Application-stream";
            }
            var byteS = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(byteS, contentType, Path.GetFileName(filenamee));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddCertificate([FromForm]Request_Certificate request_Certificate)
        {
            var Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _uService.AddCertificate(Id, request_Certificate));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Addcourse([FromBody] Request_Couse request_Couse)
        {
            var Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _uService.Addcourse(Id, request_Couse));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddSubject(Request_Subject request_Subject)
        {
            var Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _uService.AddSubject(Id, request_Subject));
        }
    }
}
