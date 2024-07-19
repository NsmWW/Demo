namespace DemoBTL.Application.Handle.HandleEmail
{
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmptServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
