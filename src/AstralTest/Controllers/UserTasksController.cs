using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Services;
using AstralTest.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с задачами пользователей
    /// </summary>
    [Route("Tasks")]
    [Authorize(Roles = nameof(RolesOption.User))]
    public class UserTasksController : Controller
    {    
        private readonly IUserTaskService _userTaskService;

        public UserTasksController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        //Возвращает задачу из одного контейнера
        [HttpGet("{idContainer}")]
        public async Task<object> GetTasks(Guid idContainer)
        {
            var tasks = await _userTaskService.GetAsync();
            var result = tasks.Where(x => x.ListId == idContainer);
            return result.Select(x => x.UserTaskView());
        }

        //Добавляет задачу в контейнер
        [HttpPost("{idContainer}")]
        public async Task<object> AddTask([FromBody] UserTaskModel model, Guid idContainer)
        {
            return await _userTaskService.AddAsync(idContainer, model);
        }

        [HttpPut("{idTask}")]
        public async Task EditTask([FromBody] UserTaskModel model, Guid idTask)
        {
            await _userTaskService.EditAsync(idTask, model);
        }

        [HttpDelete("{idTask}")]
        public async Task DeleteTask(Guid idTask)
        {
            await _userTaskService.DeleteAsync(idTask);
        } 
    }
}
