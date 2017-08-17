using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс дял работы с задачами пользователя
    /// </summary>
    public interface IUserTaskService
    {
        /// <summary>
        /// Список задач
        /// </summary>
        IEnumerable<UserTask> Tasks { get; }

        /// <summary>
        /// Добавляет задачу в список
        /// </summary>
        /// <param name="idContainer">Id списка, в который будут добавлять задачу</param>
        /// <param name="task">Модель задачи</param>
        /// <returns></returns>
        Task<Guid> AddAsync(Guid idContainer, UserTaskModel task);

        /// <summary>
        /// Изменяет задачу
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        Task EditAsync(Guid idTask, UserTaskModel task);

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <param name="idTask"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid idTask);

        /// <summary>
        /// Возвращает все задачи
        /// </summary>
        /// <returns></returns>
        Task<List<UserTask>> GetAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<byte[]> TasksConvertToXssfAsync(IEnumerable<UserTask> list);
    }
}
