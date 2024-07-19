using DemoBTL.Application.Payload.RequestModels.UserResquest;
using DemoBTL.Application.Payload.ResponeModels.DataUser;
using DemoBTL.Application.Payload.Respones;
using DemoBTL.Domain.Entity;
using System.Reflection.Metadata;

namespace DemoBTL.Application.InterfaceService
{
    public interface IAuService
    {
        Task<ResponseOject<DataResponeUser>> Register(Request_Register request_Register);
        Task<string> ConfirmRegister(string confirmcod);
        Task<ResponseOject<DataResponeLogin>> GetJwtTokenAsync(User user);
        Task<ResponseOject<DataResponeLogin>> Login(Resquest_Login request);
        Task<ResponseOject<DataResponeUser>> ChangePassword(int userId, Request_ChangePassword request_ChangePassword);
        Task<string> ForgotPassword(string email);
        Task<string> ConfirmCreatePassword(Request_CreateNewPassword request_CreateNewPassword);
        Task<string> Addrole(int userId, List<string> roles);
        Task<string> Deleterole(int userId, List<string> roles);
        Task<ResponseOject<DataResponeUser>> updateuser(ResquestUpdateUser updateUser, int userid);
        Task<string> AddCertificate(int userId, Request_Certificate request_Certificate);
        Task<string> AddCertificateOfType(int Userid, Resquest_CertificateOfType resquest_CertificateOfType);
        Task<string> Addcourse(int userId, Request_Couse request_Couse);
        Task<string> AddSubject(int userId, Request_Subject request_Subject);
    }
}
