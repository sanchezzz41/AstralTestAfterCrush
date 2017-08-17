using System;
using System.Collections.Generic;
using System.IO;
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
    /// Контроллер для работы с файлами
    /// </summary>
    [Route("Files")]
    [Authorize(Roles = nameof(RolesOption.User))]
    public class FilesController : Controller
    {
        private readonly IFileService _service;

        public FilesController(IFileService service)
        {
            _service = service;
        }

        //Загружает файл на сервер в локальное хранилище
        [HttpPost]
        public async Task<object> DownloadFile(IFormFile file)
        {

            var resultStream = new MemoryStream();

            await file.CopyToAsync(resultStream);

            FileModel model = new FileModel
            {
                NameFile = file.FileName,
                TypeFile = file.ContentType,
                StreamFile = resultStream
            };
            return await _service.AddAsync(model);
        }

        //Возвращает файл по id
        [HttpGet("{idFile}")]
        public async Task<object> GetFile(Guid idFile)
        {
            var result = await _service.GetFileAsync(idFile);

            return File(result.StreamFile, result.TypeFile, result.NameFile);
        }

        //Возвращает информацию о всех файлах
        [HttpGet]
        public async Task<object> GetAllFiles()
        {
            var resultView = await _service.GetInfoAboutAllFilesAsync();
            return resultView
                .Select(x => x.FileView());
        }

        //Удаляет файл по Id
        [HttpDelete("{idFile}")]
        public async Task DeleteFile(Guid idFile)
        {
            await _service.DeleteAsync(idFile);
        }

    }
}
