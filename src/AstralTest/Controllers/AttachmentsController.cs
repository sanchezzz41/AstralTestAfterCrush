using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с привязками файлов к задачам
    /// </summary>
    [Route("Attachments")]
    [Authorize(Roles = nameof(RolesOption.User))]
    public class AttachmentsController : Controller
    {
        private readonly IAttachmentsService _service;
        public AttachmentsController(IAttachmentsService service)
        {
            _service = service;
        }
        //Прикрепляет файлы к задаче по Id's
        [HttpPost]
        public async Task<List<Guid>> AttachFile([FromBody]AttachmentModel attachmentModel)
        {
           return await _service.AttachFileToTaskAsync(attachmentModel);
        }

        //Удаляет связывание 
        [HttpDelete("{attachId}")]
        public async Task DeleteAttachment(Guid attachId)
        {
            await _service.DeleteAttachmentAsync(attachId);
        }

        //Возвращает все прикрепления
        [HttpGet]
        public async Task<object> GetAttachments()
        {
            var result = await _service.GetAllattachmentsAsync();
            return result.Select(x => x.AttachmentView());
        }

        //Возвращает прикрепления одной задачи
        [HttpGet("{idTask}")]
        public async Task<object> GetAttachments(Guid idTask)
        {
            var result = await _service.GetAllattachmentsAsync(idTask);
            return result.Select(x => x.AttachmentView());
        }
    }
}
