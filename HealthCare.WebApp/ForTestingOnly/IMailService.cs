using static HealthCare.WebApp.ForTestingOnly.InMemoryMailService;

namespace HealthCare.WebApp.ForTestingOnly
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        List<Email> GetSentEmails();
    }

    public class InMemoryMailService : IMailService
    {
        private List<Email> sentEmails = new List<Email>();

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            sentEmails.Add(new Email { To = to, Subject = subject, Body = body });
        }

        public List<Email> GetSentEmails()
        {
            // Return a copy of the list to avoid exposing the internal state
            return new List<Email>(sentEmails);
        }

        public class Email
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
    }
}
