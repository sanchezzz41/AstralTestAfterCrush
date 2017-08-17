using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для добавления информации о том, к чему обращался пользователь
    /// </summary>                                                              
    public interface IInfoActionService
    {
        /// <summary>
        /// Содержит информацию о том, к чему обращались пользователи
        /// </summary>
        List<InfoAboutAction> InfoUsers { get; }

        /// <summary>
        /// Добавляет информацию о том, к чему обращается пользователь
        /// </summary>
        /// <param name="paramets">Параметры метода</param>
        /// <param name="idAction">Id обращаегося пользователя</param>
        /// <returns></returns>
        Task<Guid> AddAsync(string paramets, Guid idAction);

        /// <summary>
        /// Возвращает всю информацию о том, кто и когда обращался к приложению
        /// </summary>
        /// <returns></returns>
        Task<List<InfoAboutAction>> GetAsync();
    }
}
