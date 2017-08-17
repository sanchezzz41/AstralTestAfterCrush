using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Extensions;

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для редактирования пользователя, самим собой
    /// </summary>
    [Route("Users")]
    [Authorize(Roles=nameof(RolesOption.User))]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Возвращает список пользователей, в специальной обертке
        // GET: /<controller>/
        [HttpGet]
        public async Task<object> GetUsers()
        {
            var result = await _userService.GetAsync();
            return result.Select(x=>x.UserView()); 
        }
    }
}
