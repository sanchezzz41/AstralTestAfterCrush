using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;

namespace AstralTest.Domain.Services
{
    public class LogService : ILogService
    {
        private readonly IActionService _actionService;
        private readonly IInfoActionService _infoActionService;

        public LogService(IActionService actionService, IInfoActionService infoActionService)
        {
            _actionService = actionService;
            _infoActionService = infoActionService;
        }

        /// <summary>
        /// Создаёт логи на основе модели, которая включает название контроллера, действия, имя пользоватея
        /// и параметры
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public async Task Log(LogModel logModel)
        {
            if (logModel == null)
            {
                throw new NullReferenceException("Ссылка на модель указывает на null");
            }
            var actionId = await _actionService.AddAsync(logModel.UserName, logModel.NameController,
                logModel.NameAction);

            await _infoActionService.AddAsync(logModel.Parametrs, actionId);
        }
    }
}
