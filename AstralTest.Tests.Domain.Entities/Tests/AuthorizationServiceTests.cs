using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Services;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для тестирования авторизации
    /// </summary>
    [TestFixture]
    public class AuthorizationServiceTests
    {

        //Тестируемый сервис
        private IAuthorizationService _service;

        //Контекст бд
        private DatabaseContext _context;



        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            var passwordHasher = TestInitializer.Provider.GetService<IPasswordHasher<User>>();
            var userService = new UserService(_context, passwordHasher);

            //Services
            //_service = new AuthorizationService(userService, passwordHasher);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }

        /// <summary>
        /// Тест на авторизацию пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Success()
        {
            //Пароль "admin"
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.UserName == "testUser1");

            //act
            var resultUser = _service.Authorization(userForIdentity.UserName, "admin");

            //assert
            Assert.IsNotNull(resultUser);
        }

        /// <summary>
        /// Тест на авторизацию пользователя(ожидается неудача, так как входим за несуществующего пользователя)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Login_Fail()
        {
            //act
            var resultUser = _service.Authorization("Noname", "admin");

            //assert
            Assert.IsNull(resultUser);
        }

        /// <summary>
        /// Тест на авторизацию пользователя(ожидается неудача, вводим неправильный пароль)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Password_Fail()
        {
            //Пароль "admin"
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.UserName == "testUser1");

            //act
            var resultUser = _service.Authorization(userForIdentity.UserName, "PasswordWTF");

            //assert
            Assert.IsNull(resultUser);
        }
    }
}
