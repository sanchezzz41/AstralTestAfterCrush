using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Models;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для созд. логов
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Создаёт логи на основе модели, которая включает название контроллера, действия, имя пользоватея
        /// и параметры
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        Task Log(LogModel logModel);
    }
}
