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
    /// Контроллер для работы с контейнером для задач
    /// </summary>
    [Route("Containers")]
    [Authorize(Roles = nameof(RolesOption.User))]
    public class TasksContainersController : Controller
    {
        private readonly ITasksContainerService _tasksContainerService;

        public TasksContainersController(ITasksContainerService tasksContainerService)
        {
            _tasksContainerService = tasksContainerService;
        }

        //Возвращает все контейнеры данного пользователя
        [HttpGet("{idUser}")]
        public async Task<object> GetContainers(Guid idUser)
        {
            var containers = await _tasksContainerService.GetAsync();
            var result = containers.Where(x => x.UserId == idUser);
            return result.Select(x =>x.TasksContainerView());
        }

        //Добавляет контейнер пользователю
        [HttpPost("{idUser}")]
        public async Task<object> AddContainer([FromBody] TasksContainerModel model, Guid idUser)
        {
            return await _tasksContainerService.AddAsync(idUser, model);
        }

        //Изменяет контейнер
        [HttpPut("{idContainer}")]
        public async Task EditContainer([FromBody] TasksContainerModel model, Guid idContainer)
        {
            await _tasksContainerService.EditAsyc(idContainer, model);
        }

        //Удаляет контейнер
        [HttpDelete("{idContainer}")]
        public async Task DeleteContainer(Guid idContainer)
        {
            await _tasksContainerService.DeleteAsync(idContainer);
        }
    }
}
