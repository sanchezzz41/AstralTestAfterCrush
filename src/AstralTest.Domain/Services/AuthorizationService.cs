using System;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Utilits;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для авторизации пользователя
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMemoryCache _memoryCache;
        private readonly ISmsService _smsService;

        public AuthorizationService(IUserService userService, IPasswordHasher<User> passwordHasher,
            IMemoryCache memoryCache, ISmsService smsService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _memoryCache = memoryCache;
            _smsService = smsService;
        }

        /// <summary>
        /// Возвращает пользователя, если он успешно прошёл авторизацию, в противном случае null
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public User Authorization(string userName, string password)
        {
            if (userName == null)
            {
                return null;
            }
            var user = _userService.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return null;
            }
            var resultHash = _passwordHasher.HashPassword(user, password);

            if (resultHash != user.PasswordHash)
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// Отправляет и добавляет в memoryCashe код для смены пароля
        /// </summary>
        /// <param name="userName">Имя пользователя, которйы меняет пароль</param>
        /// <returns></returns>
        public async Task<Guid> SendSmsToResetPassword(string userName)
        {
            var userList = await _userService.GetAsync();
            var resultUser = userList.SingleOrDefault(x => x.UserName == userName);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с таким именем {userName} не существует.");
            }

            var codeToSend = Randomizer.GetNumbers(5);
            var resultModel = new ResetPasswordModel {IdUser = resultUser.UserId, Code = codeToSend};
            //Этот индификатор будет применяться как ключ в memorycashe
            var guidAction = Guid.NewGuid();
            _memoryCache.Set(guidAction, resultModel,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(3)));

            await _smsService.SendSmsAsync(resultUser.PhoneNumber, codeToSend);

            return guidAction;
        }

        /// <summary>
        /// Проверяет код который отправляется по смс
        /// </summary>
        /// <param name="idAction">Id операции, которая отправляла код</param>
        /// <param name="code">Код для подтверждения</param>
        /// <returns>Возвращает id для подтверждение смены пароля</returns>
        public async Task<Guid> ConfirmCodeFromSms(Guid idAction, string code)
        {
            var resultModel = _memoryCache.Get<ResetPasswordModel>(idAction);
            if (resultModel == null)
            {
                throw new NullReferenceException($"Операции с данным Id:{idAction} нету.");
            }
            if (String.Compare(resultModel.Code, code, StringComparison.Ordinal) != 0)
            {
                throw new InvalidOperationException($"Код для подтверждения не совпадает.");
            }

            var resultActionGuid = Guid.NewGuid();

            _memoryCache.Set(resultActionGuid, resultModel.IdUser,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return await Task.FromResult(resultActionGuid);
        }

        /// <summary>
        /// Меняет пароль пользователя на новый
        /// </summary>
        /// <param name="idAction">Id операции, которая подтверждает смену пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        public async Task ResetPassword(Guid idAction, string newPassword)
        {
            var resultUserId = _memoryCache.Get<Guid>(idAction);
            if (resultUserId == Guid.Empty)
            {
                throw new NullReferenceException($"Операции с данным Id:{idAction} нету.");
            }
            await _userService.ResetPassword(resultUserId, newPassword);

        }
    }
}
