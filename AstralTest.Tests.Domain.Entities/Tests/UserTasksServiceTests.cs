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
using NUnit.Framework.Internal;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для проверки сервсиса для работы с задачами 
    /// </summary>
    [TestFixture]
    public class UserTasksServiceTests
    {
        private DatabaseContext _context;

        //Тестируемый сервис
        private IUserTaskService _service;

        //Для проверки значений
        private List<UserTask> _userTasks;

        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<TasksContainerDataFactory>().CreateTasksContainer();
            await TestInitializer.Provider.GetService<UserTaskDataFactory>().CreateTasks();
            _userTasks = await _context.Tasks.ToListAsync();

            //Services
            _service = new UserTaskService(_context);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<TasksContainerDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserTaskDataFactory>().Dispose();
        }

        /// <summary>
        /// Тест на создание задача(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Create_UserTask_Success()
        {
            var containerId = _context.TasksContainers.First().ListId;
            var userTask = new UserTaskModel()
            {
                TextTask = "Какой то текст",
                EndTime = DateTime.Now
            };

            //act
            var resultId = await _service.AddAsync(containerId, userTask);
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == resultId);

            //assert
            Assert.AreEqual(userTask.TextTask, resultTask.TextTask);
            Assert.AreEqual(userTask.EndTime, resultTask.EndTime);
        }

        /// <summary>
        /// Тест на обновление задачи(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Update_Note_Success()
        {
            var taskId = _context.Tasks.First().TaskId;
            var userTask = new UserTaskModel()
            {
                TextTask = "Какой то текст",
                EndTime = DateTime.Now
            };

            //act
            await _service.EditAsync(taskId, userTask);
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == taskId);

            //assert
            Assert.AreEqual(userTask.TextTask, resultTask.TextTask);
            Assert.AreEqual(userTask.EndTime, resultTask.EndTime);
        }

        /// <summary>
        /// Тест на удаление задачи(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Delete_Note_Success()
        {
            var taskId = _context.Tasks.First().TaskId;

            //act
            await _service.DeleteAsync(taskId);
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == taskId);

            //assert
            Assert.IsNull(resultTask);
        }


        /// <summary>
        /// Тест на получение всех задач(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_Users_Sueccess()
        {
            //Act
            var taskList = await _service.GetAsync();


            //Assrt
            for (int i = 0; i < _userTasks.Count; i++)
            {
                Assert.AreEqual(_userTasks[i].TextTask, taskList[i].TextTask);
                Assert.AreEqual(_userTasks[i].EndTime, taskList[i].EndTime);
            }
        }
    }
}
