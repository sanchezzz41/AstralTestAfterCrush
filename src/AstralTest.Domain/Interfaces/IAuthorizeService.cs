using System;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для авторизации
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Возвращает пользователя, если он авторизовался, иначе null
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        User Authorization(string userName, string password);

        /// <summary>
        /// Отправляет и добавляет в memoryCashe код для смены пароля
        /// </summary>
        /// <param name="userName">Имя пользователя, который меняет пароль</param>
        /// <returns>Возвращает Id операции, которая занимается востановлением пароля</returns>
        Task<Guid> SendSmsToResetPassword(string userName);

        /// <summary>
        /// Проверяет код который отправляется по смс
        /// </summary>
        /// <param name="idAction">Id операции, которая отправляла код</param>
        /// <param name="code">Код для подтверждения</param>
        /// <returns>Возвращает id для подтверждение смены пароля</returns>
        Task<Guid> ConfirmCodeFromSms(Guid idAction, string code);

        /// <summary>
        /// Меняет пароль пользователя на новый
        /// </summary>
        /// <param name="idAction">Id операции, которая подтверждает смену пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        Task ResetPassword(Guid idAction, string newPassword);
    }
}
