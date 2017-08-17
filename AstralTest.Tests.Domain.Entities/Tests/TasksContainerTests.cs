using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Services;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для проверки серсиса работающего с контейнерами задач(TasksContainer)
    /// </summary>
    [TestFixture]
    public class TasksContainerTests
    {
        private DatabaseContext _context;

        //Тестируемый сервис
        private ITasksContainerService _service;

        //Для проверки значений
        private List<TasksContainer> _tasksContainersList;

        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<TasksContainerDataFactory>().CreateTasksContainer();
            _tasksContainersList = await _context.TasksContainers.ToListAsync();

            //Services
            _service = new TasksContainerService(_context);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<TasksContainerDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();

        }

        /// <summary>
        /// Тест для добавления контейнера(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Create_TasksContainer_Success()
        {
            var userId = _context.Users.First().UserId;
            var tasksContainer = new TasksContainerModel
            {
                Name = "testContainer"
            };

            //act
            var containerId = await _service.AddAsync(userId, tasksContainer);
            var resultContainer = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == containerId);

            //assert
            Assert.AreEqual(tasksContainer.Name, resultContainer.Name);
        }



        /// <summary>
        /// Тест для обновления контейнера(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Update_TasksContainer_Success()
        {
            var containerId = _context.TasksContainers.First().ListId;
            var tasksContainer = new TasksContainerModel
            {
                Name = "testContainer"
            };

            //act
            await _service.EditAsyc(containerId, tasksContainer);
            var resultContainer = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == containerId);

            //assert
            Assert.AreEqual(tasksContainer.Name, resultContainer.Name);
        }

        /// <summary>
        /// Тест для удаления контейнера(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Delete_TasksContainer_Success()
        {
            var containerId = _context.TasksContainers.First().ListId;

            //act
            await _service.DeleteAsync(containerId);
            var resultContainer = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == containerId);

            //assert
            Assert.IsNull(resultContainer);
        }

        /// <summary>
        /// Тест для получения контейнеров(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_TasksContainer_Success()
        {

            //act
            var resultContainers = await _service.GetAsync();

            //assert
            for (int i = 0; i < _tasksContainersList.Count; i++)
            {
                Assert.AreEqual(_tasksContainersList[i].Name, resultContainers[i].Name);
            }
        }
    }
}
