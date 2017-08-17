using System.Threading.Tasks;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для отправки сообщений на email
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Отправляет сообщение на email
        /// </summary>
        /// <param name="email">Еmail получателя</param>
        /// <param name="name">Имя получателя</param>
        /// <param name="text">Сообщение для отправки</param>
        /// <returns></returns>
        Task SendEmail(string email, string name, string text);
    }
}
