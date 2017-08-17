using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AstralTest.Extensions
{
    /// <summary>
    /// Фильтр для логгирования пользователей
    /// </summary>
    public class LoggerUsersFilter : IAsyncActionFilter
    {
        private readonly ILogService _logService;
        public LoggerUsersFilter( ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        /// <param name="next">
        /// The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate" />. Invoked to execute the next action filter or the action itself.
        /// </param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultName = context.HttpContext.User.Identity.Name;
            //Метод работает только тогда, когда пользователь авторизирован, скорей всего
            //надо сделать так, что бы анонимусов тоже записывало, уточнить!
            if (resultName != null)
            {
                var controllerName = context.RouteData.Values["controller"].ToString();
                var actionName = context.RouteData.Values["action"].ToString();
                var parameters = JsonConvert.SerializeObject(context.ActionArguments);

                if (context.RouteData.Values.Keys.First() != "action")
                {
                    parameters += JsonConvert.SerializeObject(context.RouteData.Values.First());
                }
                var resultModel = new LogModel
                {
                    NameAction = actionName,
                    NameController = controllerName,
                    Parametrs = parameters,
                    UserName = resultName
                };
                await _logService.Log(resultModel);
            }
            await next();
        }
    }
}
