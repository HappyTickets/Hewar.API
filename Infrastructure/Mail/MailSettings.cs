namespace Infrastructure.Mail
{
    internal class MailSettings
    {
        public const string SectionName = "Mail";

        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
