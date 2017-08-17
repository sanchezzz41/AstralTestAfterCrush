using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Tests.Domain.Entities.Factory
{
    /// <summary>
    /// Класс для заполнения задач в контейнеры
    /// </summary>
    public class UserTaskDataFactory
    {
        private readonly DatabaseContext _context;

        public UserTaskDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
        }

        //Заполняет бд контейнерами задач
        public async Task CreateTasks()
        {
            var tasksContainer = await _context.TasksContainers.FirstAsync();
            var tasks = new List<UserTask>
            {
                new UserTask(tasksContainer.ListId,"Задача1",DateTime.Now),
                new UserTask(tasksContainer.ListId,"Задача2",DateTime.Now)
            };
            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var tasks = await _context.Tasks.ToListAsync();
            _context.Tasks.RemoveRange(tasks);
            await _context.SaveChangesAsync();
        }
    }
}
