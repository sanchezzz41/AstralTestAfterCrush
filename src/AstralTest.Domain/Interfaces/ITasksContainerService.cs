using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с контейнерами для задач
    /// </summary>
    public interface ITasksContainerService
    {
        /// <summary>
        /// Контейнеры для задач
        /// </summary>
        IEnumerable<TasksContainer> TasksContainers { get; }

        /// <summary>
        /// Добавления контейнер для задач
        /// </summary>
        /// <param name="idMaster">Id пользователя, которому будет добавляться контейнер</param>
        /// <param name="containerModel">Модель для добавления контейнера</param>
        /// <returns></returns>
        Task<Guid> AddAsync(Guid idMaster, TasksContainerModel containerModel);

        /// <summary>
        /// Изменяет контейнер для задач
        /// </summary>
        /// <param name="idContainer">Id контейнера, который надо изменить</param>
        /// <param name="containerModel">Модель для изменения контейнера</param>
        /// <returns></returns>
        Task EditAsyc(Guid idContainer, TasksContainerModel containerModel);

        /// <summary>
        /// Удаляет контейнер
        /// </summary>
        /// <param name="idContainer">Id контейнера, который надо удалить</param>
        /// <returns></returns>
        Task DeleteAsync(Guid idContainer);

        /// <summary>
        /// Возвращает список контейнеров
        /// </summary>
        /// <returns></returns>
        Task<List<TasksContainer>> GetAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        Task<byte[]> TaskContainersConvertToXssfAsync(IEnumerable<TasksContainer> list);
    }
}
