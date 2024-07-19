using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Matching;
using MimeKit;
using MimeKit.Text;

namespace DemoBTL.Application.Handle.HandleEmail
{
    public class EmailMessage
    {
        //public List<MailboxAddress> To { get; set; }
        //public string Subject { get; set; }
        //public String Conten { get; set; }
        //public EmailMessage() { }
        //public EmailMessage(IEnumerable<string> to, string subject, String conten)
        //{
        //    To = new List<MailboxAddress>();
        //    To.AddRange(to.Select(x => new MailboxAddress("Email", x)));
        //    Subject = subject;
        //    Conten = conten;
        //}
        private List<MailboxAddress> _to;
        public IEnumerable<MailboxAddress> To
        {
            get { return _to; }
            set { _to = value.ToList(); }
        }
        public string Subject { get; set; }
        public string Content {  get; set; }
        public EmailMessage() 
        {
            _to = new List<MailboxAddress>();
        }
        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            _to = to.Select(x=> new MailboxAddress("Email", x)).ToList();
            Subject = subject;
            Content = content;
        }
    }
}
