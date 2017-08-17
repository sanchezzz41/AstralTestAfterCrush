using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для востановления пароля
    /// </summary>
    [Route("ResetPassword")]
    [AllowAnonymous]
    public class ResetPasswordController : Controller
    {
        private readonly Domain.Interfaces.IAuthorizationService _authorizationService;

        public ResetPasswordController(Domain.Interfaces.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        //Отправляет код для смены пароля
        [HttpPost("SendSms")]
        public async Task<Guid> SendCodeToReset([FromQuery] string userName)
        {
            var resultGuid = await _authorizationService.SendSmsToResetPassword(userName);
            return resultGuid;
        }

        //Проверяет код для смены пароля
        [HttpPost("ConfirmCode/{idOperation}")]
        public async Task<object> ConfirmCode(Guid idOperation, [FromQuery] string code)
        {
            //Id для подтверждения операции по смене пароля
            var confirmId = await _authorizationService.ConfirmCodeFromSms(idOperation, code);
            return confirmId;
        }

        [HttpPost("EnterPassword/{confirmId}")]
        public async Task ResetPawwrod(Guid confirmId, string newPassword)
        {
            await _authorizationService.ResetPassword(confirmId, newPassword);
        }
    }
}
