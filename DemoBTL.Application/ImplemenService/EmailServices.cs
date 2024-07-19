using DemoBTL.Application.Handle.HandleEmail;
using DemoBTL.Application.InterfaceService;
using DemoBTL.Application.Payload.Respones;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoBTL.Application.ImplemenService
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailConfiguration _configuration;
        public EmailServices() { }
        public EmailServices(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string SendEmail(EmailMessage emailMessage)
        {
            var messgae = CreateEmailMessage(emailMessage);
            Send(messgae);
            var recipents = string.Join(", ", messgae.To);
            return Responmessage.GetuinSuccesMessage(recipents);
        }
        #region
        private MimeMessage CreateEmailMessage(EmailMessage Message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _configuration.From));
            emailMessage.To.AddRange(Message.To);
            emailMessage.Subject = Message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = Message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage message)
        {
            using var cilent = new SmtpClient();
            try
            {
                cilent.Connect(_configuration.SmptServer, _configuration.Port, true);
                cilent.AuthenticationMechanisms.Remove("XoaAuth2");
                cilent.Authenticate(_configuration.UserName, _configuration.Password);
                cilent.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cilent.Disconnect(true);
                cilent.Dispose();
            }
        }
        #endregion
    }
}
