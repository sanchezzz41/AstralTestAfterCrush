using System;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Extensions;
using AstralTest.FileStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с пользователем,
    /// Доступен только для админов
    /// </summary>
    [Route("Admin")]
    [Authorize(Roles = nameof(RolesOption.Admin))]
    public class UsersController : Controller
    {
        private readonly IUserService _context;

        public UsersController(IUserService context)
        {
            _context = context;
        }

        //Возвращает всех пользователей
        [HttpGet("Users")]
        public async Task<object> GetUsers()
        {
            var result = await _context.GetAsync();
            return result.UsersForAdminView(HttpContext);
        }

        //Удаляет пользователя по Id
        [HttpDelete("Users/{id}")]
        public async Task DeleteUser(Guid id)
        {
            await _context.DeleteAsync(id);

        }

        //Добавляет пользователя, скорей всего не нужно(как админ может кого то добавить, не спрашиваю его)
        [HttpPost("Users")]
        public async Task<object> AddUser([FromBody] UserRegisterModel us)
        {
            var result = await _context.AddAsync(us);
            return result;
        }

        //Изменяет пользователя
        [HttpPut("Users/{id}")]
        public async Task EditUser([FromBody] EditUserModel us, Guid id)
        {
            await _context.EditAsync(us, id);
        }
    }
}
