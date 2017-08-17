using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{
    public class InfoActionService : IInfoActionService
    {
        /// <summary>
        /// Содержит информацию о том, к чему обращались пользователи
        /// </summary>
        public List<InfoAboutAction> InfoUsers
        {
            get
            {
                return _context.InfoAboutActions
                    .Include(x => x.Action)
                    .ToList();
            }
        }

        private readonly DatabaseContext _context;
        private readonly IActionService _enteredService;

        public InfoActionService(DatabaseContext context, IActionService enteredService)
        {
            _context = context;
            _enteredService = enteredService;
        }

        /// <summary>
        /// Добавляет информацию о том, к чему обращается пользователь
        /// </summary>
        /// <param name="paramets">Параметры метода</param>
        /// <param name="idAction">Id обращаегося пользователя</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(string paramets, Guid idAction)
        {
            var resultEntUser = await _context.Actions.SingleOrDefaultAsync(x => x.Id == idAction);

            if (resultEntUser == null)
            {
                throw new NullReferenceException(
                    $"Пользователя, который обращается к приложению с таким Id{idAction} не существует.");
            }
            var result = new InfoAboutAction(idAction,paramets);
            await _context.InfoAboutActions.AddAsync(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }


        /// <summary>
        /// Возвращает всю информацию о том, кто и когда обращался к приложению
        /// </summary>
        /// <returns></returns>
        public async Task<List<InfoAboutAction>> GetAsync()
        {
            return await _context.InfoAboutActions
                .Include(x => x.Action)
                .ToListAsync();
        }
    }
}

