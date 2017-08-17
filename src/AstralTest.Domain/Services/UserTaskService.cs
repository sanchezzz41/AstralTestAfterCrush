using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для работы с задачами
    /// </summary>
    public class UserTaskService : IUserTaskService
    {
        private readonly DatabaseContext _context;

        public UserTaskService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список задач
        /// </summary>
        public IEnumerable<UserTask> Tasks
        {
            get
            {
                return _context.Tasks
                    .Include(x => x.MasterList)
                    .Include(x => x.MasterList.Master)
                    .Include(x => x.Attachments)
                    .ToList();
            }
        }

        /// <summary>
        /// Добавляет задачу в список
        /// </summary>
        /// <param name="idContainer"></param>
        /// <param name="task">Модель задачи</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idContainer, UserTaskModel task)
        {
            if (task == null)
            {
                throw new NullReferenceException("Модель ссылается на Null");
            }
            var taskContainer = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == idContainer);
            if (taskContainer == null)
            {
                throw new NullReferenceException($"Контейнера с Id{idContainer} не существует");
            }
            var resultTask = new UserTask(taskContainer.ListId, task.TextTask, task.EndTime);
            await _context.Tasks.AddAsync(resultTask);

            await _context.SaveChangesAsync();

            return resultTask.TaskId;
        }

        /// <summary>
        /// Изменяет задачу
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task EditAsync(Guid idTask, UserTaskModel task)
        {
            if (task == null)
            {
                throw new NullReferenceException("Модель ссылается на Null");
            }
            var result = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == idTask);
            if (result == null)
            {
                throw new NullReferenceException($"Задачи с Id{idTask} не существует");
            }

            if (!string.IsNullOrEmpty(task.TextTask))
            {
                result.TextTask = task.TextTask;
            }

            result.EndTime = task.EndTime;

            if (task.ActualTimeEnd != null) result.ActualTimeEnd = task.ActualTimeEnd.Value;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <param name="idTask"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idTask)
        {
            var result = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == idTask);
            if (result != null)
            {
                _context.Tasks.Remove(result);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает все задачи
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTask>> GetAsync()
        {
            return await _context.Tasks
                //.Include(x => x.MasterList)
                //.Include(x => x.Attachments)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> TasksConvertToXssfAsync(IEnumerable<UserTask> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Tasks");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Текст задачи");
            row.CreateCell(1).SetCellValue("Время когда надо закончить");
            row.CreateCell(2).SetCellValue("Время когда закончили");
            row.CreateCell(3).SetCellValue("Id задачи");
            row.CreateCell(4).SetCellValue("Id списка");
            rowIndex++;

            foreach (var task in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(task.TextTask);
                newRow.CreateCell(1).SetCellValue(task.EndTime);
                newRow.CreateCell(2).SetCellValue(task.ActualTimeEnd);
                newRow.CreateCell(3).SetCellValue(task.TaskId.ToString());
                newRow.CreateCell(4).SetCellValue(task.ListId.ToString());
                rowIndex++;
            }


            for (int i = 0; i < 5; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);
        }
    }
}
