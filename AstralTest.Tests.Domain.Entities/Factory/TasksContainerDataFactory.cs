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
    //Класс для заполнения бд контейнерами задач
    public class TasksContainerDataFactory
    {
        private readonly DatabaseContext _context;

        public TasksContainerDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
        }

        //Заполняет бд контейнерами задач
        public async Task CreateTasksContainer()
        {
            var user = await _context.Users.FirstAsync();
            var tasksContainers = new List<TasksContainer>
            {
                new TasksContainer(user.UserId,"Контейнер1"),
                new TasksContainer(user.UserId,"Контейнер2")
            };
            await _context.TasksContainers.AddRangeAsync(tasksContainers);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var tasksContainers = await _context.TasksContainers.ToListAsync();
            _context.TasksContainers.RemoveRange(tasksContainers);
            await _context.SaveChangesAsync();
        }
    }
}
