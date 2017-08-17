using System.Threading.Tasks;
using AstralTest.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Заглушка для отправки сообщений
    /// </summary>
    public class EmailSenderService : IEmailSender
    {
        private readonly ILogger _logs;
        public EmailSenderService(ILogger<EmailSenderService> log)
        {
            _logs = log;
        }
        public async Task SendEmail(string email, string name, string text)
        {
            await Task.Run(() => _logs.LogInformation(2,
                $"Сообщение на адрес {email}(получатель:{name}) с текстом {text} отправленно!"));
        }
    }
}
