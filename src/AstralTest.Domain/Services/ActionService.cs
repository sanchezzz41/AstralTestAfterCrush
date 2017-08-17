using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для работы с пользователями, которые входили в приложение
    /// </summary>
    public class ActionService : IActionService
    {
        private readonly DatabaseContext _context;

        public ActionService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Содержит входивших пользователей
        /// </summary>
        public List<ActionLog> EnteredUsers {
            get
            {
                return _context.Actions
                    .Include(x => x.User)
                    .Include(x => x.InfoAboutActions)
                    .ToList();
            } }

        /// <summary>
        /// Добавляет действие, которое сделал пользователь в приложение
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="controllerName">Название контроллера</param>
        /// <param name="actionName">Название действия</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(string userName, string controllerName, string actionName)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким именем {userName} не существует.");
            }
            var testEntuser = await _context.Actions.SingleOrDefaultAsync(x => x.User.UserName == userName);
            if (testEntuser != null)
            {
                return testEntuser.Id;
            }
            var resuleAction = new ActionLog(resultUser.UserId,controllerName,actionName);
            await _context.Actions.AddAsync(resuleAction);
            await _context.SaveChangesAsync();
            return resuleAction.Id;
        }

        /// <summary>
        /// Возвращает список входивших пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<ActionLog>> GetAsync()
        {
            return await _context.Actions
                .Include(x => x.User)
                .Include(x => x.InfoAboutActions)
                .ToListAsync(); ;
        }
    }
}
