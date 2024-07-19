using DemoBTL.Application.Handle.HandleEmail;

namespace DemoBTL.Application.InterfaceService
{
    public interface IEmailServices
    {
        string SendEmail(EmailMessage emailMessage);
    }
}
