using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interfaces
{

    /// <summary>
    /// Интерфейс для работы с пользователем, включает стандартные операции CRUD
    /// </summary>
    public interface IUserService
    {
        IEnumerable<User> Users { get; }

        /// <summary>
        /// Добавления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id пользователя</returns>
        Task<Guid> AddAsync(UserRegisterModel user);

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="idUser">id пользователя</param>
        /// <returns></returns>
        Task DeleteAsync(Guid idUser);

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        /// <param name="user">Модель для изменения пользователя</param>
        /// <param name="id">Id пользователя, которого изменяют</param>
        /// <returns></returns>
        Task EditAsync(EditUserModel user, Guid id);

        /// <summary>
        /// Получает пользователей из БД 
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAsync();

        /// <summary>
        /// Меняет пароль пользователя на новый
        /// </summary>
        /// <param name="idUser">ID пользователя, которому надо сменить пароль</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        Task ResetPassword(Guid idUser, string newPassword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<byte[]> UsersConvertToXssfAsync(IEnumerable<User> list);
    }
}
