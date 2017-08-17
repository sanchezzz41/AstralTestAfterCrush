using System.Threading.Tasks;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Domain.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers.Admin
{
    /// <summary>
    /// Контроллер для конвертации данных в Excel формат
    /// </summary>
    [Route("Admin/Converter")]
    [Authorize(Roles = nameof(RolesOption.Admin))]
    public class ConvertToExcelController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserTaskService _userTaskService;
        private readonly INoteService _noteService;
        private readonly IFileService _fileService;
        private readonly ITasksContainerService _tasksContainerService;
        private readonly IAttachmentsService _attachmentsService;

        private static string typeXssf = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ConvertToExcelController(
            IUserService userService,
            IUserTaskService taskService,
            INoteService noteService,
            IFileService fileService,
            ITasksContainerService tasksContainerService,
            IAttachmentsService attachmentsService
            )
        {
            _userService = userService;
            _userTaskService = taskService;
            _noteService = noteService;
            _fileService = fileService;
            _tasksContainerService = tasksContainerService;
            _attachmentsService = attachmentsService;
        }

        //Возвращает эксель документ с пользователями
        [HttpGet("Users")]
        public async Task<object> GetUsers()
        {
            var resultList = await _userService.GetAsync();
            var resultMass = await _userService.UsersConvertToXssfAsync(resultList);
            return File(resultMass,typeXssf,"Users.xlsx");
        }

        //Возвращает эксель документ с заметками
        [HttpGet("Notes")]
        public async Task<object> GetNotes()
        {
            var resultList = await _noteService.GetAsync();
            var resultMass = await _noteService.NotesConvertToXssfAsync(resultList);
            return File(resultMass, typeXssf, "Notes.xlsx");
        }

        //Возвращает эксель документ с задачами
        [HttpGet("Tasks")]
        public async Task<object> GetTasks()
        {
            var resultList = await _userTaskService.GetAsync();
            var resultMass = await _userTaskService.TasksConvertToXssfAsync(resultList);
            return File(resultMass, typeXssf, "Tasks.xlsx");
        }

        //Возвращает эксель документ с информацией о файлах
        [HttpGet("Files")]
        public async Task<object> GetFiles()
        {
            var resultList = await _fileService.GetInfoAboutAllFilesAsync();
            var resultMass = await _fileService.FilesConvertToXssfAsync(resultList);
            return File(resultMass, typeXssf, "Files.xlsx");
        }

        //Возвращает эксель документ с прикреплениями
        [HttpGet("Attachments")]
        public async Task<object> GetAttachments()
        {
            var resultList = await _attachmentsService.GetAllattachmentsAsync();
            var resultMass = await _attachmentsService.AttachmentsConvertToXssfAsync(resultList);
            return File(resultMass, typeXssf, "Attachments.xlsx");
        }

        //Возвращает эксель документ с контейнерами задач
        [HttpGet("TaskContainers")]
        public async Task<object> GetTasksContainer()
        {
            var resultList = await _tasksContainerService.GetAsync();
            var resultMass = await _tasksContainerService.TaskContainersConvertToXssfAsync(resultList);
            return File(resultMass, typeXssf, "TaskContainers.xlsx");
        }
    }
}
