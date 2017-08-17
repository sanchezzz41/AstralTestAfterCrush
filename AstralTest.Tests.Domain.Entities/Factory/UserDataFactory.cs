using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Utilits;
using AstralTest.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Tests.Domain.Entities.Factory
{
    /// <summary>
    /// Заполняет бд пользователями
    /// </summary>
    public class UserDataFactory
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
            _passwordHasher = TestInitializer.Provider.GetService<IPasswordHasher<User>>();
        }

        public async Task CreateUsers()
        {
            //При создание пароля, сначала пароль, потом соль
            var userList = new List<User>
            {
                new User("testUser1", "testEmail1","88005553555", "qwert", _passwordHasher.HashPassword(null, "admin" + "qwert"),
                    RolesOption.Admin),
                new User("testUser2", "testEmail2","88005553555", "asd", _passwordHasher.HashPassword(null, "user" + "asd"),
                    RolesOption.User),
            };
            await _context.Users.AddRangeAsync(userList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var users = await _context.Users.ToListAsync();
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
        }
    }
}
