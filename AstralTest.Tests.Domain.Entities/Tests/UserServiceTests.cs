using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Services;
using AstralTest.Identity;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс, который проверяет сервис для работы с пользователями
    /// </summary>
    [TestFixture]
    public class UserServiceTests
    {
        //Список для сравнения
        private List<User> _existstingUsers;

        //Тестируемый сервис
        private IUserService _userService;

        private DatabaseContext _context;

        private IPasswordHasher<User> _passwordHasher;

        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            _existstingUsers = await _context.Users.ToListAsync();

            //Services
            _passwordHasher = TestInitializer.Provider.GetService<IPasswordHasher<User>>();
            _userService = new UserService(_context, _passwordHasher);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }


        ///Тест создание пользователя
        [Test]
        public async Task Crete_User_Success()
        {
            var user = new UserRegisterModel
            {
                Email = "Email",
                Password = "qwert",
                RoleId = RolesOption.User,
                UserName = "UserTest"
            };

            //Act
            var userGuud = await _userService.AddAsync(user);
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userGuud);

            //Assrt
            Assert.AreEqual(user.Email, resultUser.Email);
            Assert.AreEqual(user.UserName, resultUser.UserName);
            Assert.AreEqual(user.RoleId, resultUser.RoleId);
        }

        /// <summary>
        /// Тест по создание пользоваетля(ожидается исплюкчение, указания на Null)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Crete_User_NullExp()
        {

            //Act
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () => await _userService.AddAsync(null));

            //Assrt
            Assert.AreEqual(exception.Message, "Ссылка на пользователя указывате на Null.");
        }

        /// <summary>
        /// Тест на обновление пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Update_User_Success()
        {
            var user = new EditUserModel
            {
                Email = "Email",
                RoleId = RolesOption.User,
                UserName = "UserTest"
            };

            //Act
            await _userService.EditAsync(user, _existstingUsers[0].UserId);
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserId == _existstingUsers[0].UserId);

            //Assrt
            Assert.AreEqual(user.Email, resultUser.Email);
            Assert.AreEqual(user.UserName, resultUser.UserName);
            Assert.AreEqual(user.RoleId, resultUser.RoleId);
        }

        /// <summary>
        /// Тест на удаление пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Delete_User_Success()
        {

            //Act
            await _userService.DeleteAsync(_existstingUsers[0].UserId);
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserId == _existstingUsers[0].UserId);

            //Assrt
            Assert.IsNull(resultUser);
        }

        /// <summary>
        /// Тест на получение всех пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_Users_Sueccess()
        {
            //Act
            var userList = await _userService.GetAsync();

            //Assrt
            for (int i = 0; i < _existstingUsers.Count; i++)
            {
                Assert.AreEqual(_existstingUsers[i].Email, userList[i].Email);
                Assert.AreEqual(_existstingUsers[i].UserName, userList[i].UserName);
                Assert.AreEqual(_existstingUsers[i].RoleId, userList[i].RoleId);
            }
        }
    }
}
