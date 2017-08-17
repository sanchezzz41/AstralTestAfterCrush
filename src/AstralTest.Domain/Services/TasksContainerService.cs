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
    /// Класс для работы с контейнером задач
    /// </summary>
    public class TasksContainerService : ITasksContainerService
    {
        private readonly DatabaseContext _context;

        public TasksContainerService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Контейнеры для задач
        /// </summary>
        public IEnumerable<TasksContainer> TasksContainers
        {
            get
            {
                return _context.TasksContainers
                    .Include(x => x.Master)
                    .Include(x => x.Tasks)
                    .ToList();
            }
        }

        /// <summary>
        /// Добавления контейнер для задач
        /// </summary>
        /// <param name="idMaster">Id пользователя, которому будет добавляться контейнер</param>
        /// <param name="containerModel">Модель для добавления контейнера</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idMaster, TasksContainerModel containerModel)
        {
            if (containerModel == null)
            {
                throw new NullReferenceException("Модель равна Null");
            }
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == idMaster);
            if (user == null)
            {
                throw new NullReferenceException($"Пользователя с таким Id({idMaster}) нету");
            }
            var resultContainer = new TasksContainer(idMaster, containerModel.Name);
            await _context.TasksContainers.AddAsync(resultContainer);

            await _context.SaveChangesAsync();

            return resultContainer.ListId;
        }

        /// <summary>
        /// Изменяет контейнер для задач
        /// </summary>
        /// <param name="idContainer">Id контейнера, который надо изменить</param>
        /// <param name="containerModel">Модель для изменения контейнера</param>
        /// <returns></returns>
        public async Task EditAsyc(Guid idContainer, TasksContainerModel containerModel)
        {

            if (containerModel == null)
            {
                throw new NullReferenceException();
            }
            var result = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == idContainer);
            if (result == null)
            {
                throw new NullReferenceException();
            }
            result.Name = containerModel.Name;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет контейнер
        /// </summary>
        /// <param name="idContainer">Id контейнера, который надо удалить</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idContainer)
        {
            var result = await _context.TasksContainers.SingleOrDefaultAsync(x => x.ListId == idContainer);
            if (result != null)
            {
                _context.TasksContainers.Remove(result);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает список контейнеров
        /// </summary>
        /// <returns></returns>
        public async Task<List<TasksContainer>> GetAsync()
        {
            return await _context.TasksContainers
                .Include(x => x.Master)
                .Include(x => x.Tasks)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> TaskContainersConvertToXssfAsync(IEnumerable<TasksContainer> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("TasksContainer");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Название");
            row.CreateCell(1).SetCellValue("Id списка");
            row.CreateCell(2).SetCellValue("Id пользователя");

            rowIndex++;

            foreach (var tasksContainer in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(tasksContainer.Name);
                newRow.CreateCell(1).SetCellValue(tasksContainer.ListId.ToString());
                newRow.CreateCell(2).SetCellValue(tasksContainer.UserId.ToString());
                rowIndex++;
            }

            for (int i = 0; i < 3; i++)
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
