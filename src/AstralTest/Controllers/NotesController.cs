using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Extensions;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с заметками
    /// </summary>
    [Route("Notes")]
    [Authorize(Roles =nameof(RolesOption.User))]
    public class NotesController : Controller
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService context)
        {
            _noteService = context;
        }

        //Возвращает все заметки для определенного пользователя
        [HttpGet("{id}")]
        public async Task<object> List(Guid id)
        {
            var result = await _noteService.GetAsync();
            return result.Where(x=>x.IdUser== id).Select(x=>x.NoteView());
        }

        //Возвращает заметки в опр. интервале
        [HttpGet]
        public async Task<object> List(int offSet, int count)
        {
            var prom = await _noteService.GetAsync();
            var result = prom.OrderBy(x => x.Master.UserName).Skip(offSet).Take(count).ToList();
            return result.Select(x=>x.NoteView());
        }

        //Добавляет заметку
        [HttpPost("{idMaster}")]
        public async Task<object> AddNote([FromBody] NoteModel mod, Guid idMaster)
        {
            var resultId = await _noteService.AddAsync(mod, idMaster);
            return resultId;
        }

        //Удаляет запись по id
        [HttpDelete("{id}")]
        public async Task DeleteNote(Guid id)
        {
            await _noteService.DeleteAsync(id);
        }

        //Изменяет запись
        [HttpPut("{id}")]
        public async Task EditNote([FromBody]NoteModel mod, Guid id)
        {
            await _noteService.EditAsync(mod, id);

        }
    }
}
