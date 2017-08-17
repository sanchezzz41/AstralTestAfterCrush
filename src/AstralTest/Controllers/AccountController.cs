using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using AstralTest.Domain.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для авторизации пользователей
    [Authorize]
    [Route("Auth")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signManager;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailService;
        private readonly Domain.Interfaces.IAuthorizationService _authorizationService;

        public AccountController(SignInManager<User> sign, Domain.Interfaces.IAuthorizationService authService,
            IUserService userService, IEmailSender email)
        {
            _signManager = sign;
            _userService = userService;
            _emailService = email;
            _authorizationService = authService;
        }


        //Post:Регистрация пользователя
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<object> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.Select(x => new {Key = x.Key, Error = x.Value.Errors.Select(a => a.ErrorMessage)});
            }
            var resultId = await _userService.AddAsync(model);

            //Тут отправляем сообщение пользователю(либо для подверждения, либо ещё
            //для чего либо, но пока только в логи записываем это)
            await _emailService.SendEmail(model.Email, model.UserName, "Регистрация прошла успешна.");
            //Ищем добавленного пользователя, и авторезируем его
            var newList = await _userService.GetAsync();
            var newUser = newList.Single(x => x.UserId == resultId);

            await _signManager.SignInAsync(newUser, false);
            return "Вы успешно зарегестрировались.";
        }

        //Post: Вход в приложение
        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.Select(x => new {Key = x.Key, Error = x.Value.Errors.Select(a => a.ErrorMessage)});
            }
            var user = _authorizationService.Authorization(model.UserName, model.Password);
            if (user != null)
            {
                await _signManager.SignInAsync(user, model.RememberMe);
                return "Авторизация прошла успешна.";
            }


            return "Авторизация не удалась.";
        }

        //Выход из приложения
        [HttpDelete]
        public async Task<object> Logout()
        {
            await _signManager.SignOutAsync();
            return "Вы вышли из приложения";
        }
    }
}
