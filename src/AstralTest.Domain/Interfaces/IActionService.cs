using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интферфейс для работы с пользователями, которые обращаются к приложение
    /// </summary>
    public interface IActionService
    {
        /// <summary>
        /// Содержит входивших пользователей
        /// </summary>
        List<ActionLog> EnteredUsers { get; }

        /// <summary>
        /// Добавляет действие, которое сделал пользователь в приложение
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="controllerName">Название контроллера</param>
        /// <param name="actionName">Название действия</param>
        /// <returns></returns>
        Task<Guid> AddAsync(string userName,string controllerName,string actionName);

        /// <summary>
        /// Возвращает список входивших пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<ActionLog>> GetAsync();
    }
}
