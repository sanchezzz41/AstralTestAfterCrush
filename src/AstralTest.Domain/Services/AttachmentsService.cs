using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс позволяющий прикреплять файлы к задачам
    /// </summary>
    public class AttachmentsService : IAttachmentsService
    {

        public IEnumerable<Attachment> Attachments
        {
            get
            {
                return _context.Attachments
                    .Include(x => x.MasterFile)
                    .Include(x => x.MasterTask);;
            }
        }

        private readonly DatabaseContext _context;

        public AttachmentsService(DatabaseContext context)
        {
            _context = context;
        }


        public async Task AttachFileToTaskAsync(Guid taskId, Guid fileId)
        {
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == taskId);
            if (resultTask == null)
            {
                throw new InvalidOperationException($"Задачи с таким id({taskId}) не существует");
            }
            var resultFile = await _context.Files.SingleOrDefaultAsync(x => x.FileId == fileId);
            if (resultFile == null)
            {
                throw new InvalidOperationException($"Файла с таким id({fileId}) не существует");
            }
            var result = new Attachment(resultTask.TaskId, resultFile.FileId);
            await _context.Attachments.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Прикрепляет файл к задаче, используя id обоих
        /// </summary>
        /// <param name="attachModel"></param>
        /// <returns></returns>
        public async Task<List<Guid>> AttachFileToTaskAsync(AttachmentModel attachModel)
        {
            var resultTask = await _context.Tasks.SingleOrDefaultAsync(x => x.TaskId == attachModel.TaskId);
            if (resultTask == null)
            {
                throw new InvalidOperationException($"Задачи с таким id({attachModel.TaskId}) не существует");
            }
            var resultGuids = new List<Guid>();
            foreach (var fileId in attachModel.FileIds)
            {
                var resultFile = await _context.Files.SingleOrDefaultAsync(x => x.FileId == fileId);
                if (resultFile != null)
                {
                    var resultAttach = new Attachment(resultTask.TaskId, resultFile.FileId);
                    _context.Attachments.Add(resultAttach);
                    resultGuids.Add(resultAttach.AttachmentId);
                }
            }
            await _context.SaveChangesAsync();
            return resultGuids;
        }

        /// <summary>
        /// Удаляет привязку между задачей и файлом
        /// </summary>
        /// <param name="attachId"></param>
        /// <returns></returns>
        public async Task DeleteAttachmentAsync(Guid attachId)
        {
            var resultAttachment = await _context.Attachments
                .SingleOrDefaultAsync(x => x.AttachmentId == attachId);
            if (resultAttachment == null)
            {
                throw new InvalidOperationException($"Привязки с такими id({attachId}) не существует");
            }
            _context.Attachments.Remove(resultAttachment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список всех привязок
        /// </summary>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllattachmentsAsync()
        {
            return await _context.Attachments
                .Include(x => x.MasterFile)
                .Include(x => x.MasterTask)
                .ToListAsync();
        }

        /// <summary>
        /// Возвращает список привязок по Id задачи
        /// </summary>
        /// <param name="idTask"></param>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllattachmentsAsync(Guid idTask)
        {
            return await _context.Attachments
                .Where(x => x.TaskId == idTask)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> AttachmentsConvertToXssfAsync(IEnumerable<Attachment> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Attachments");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Id прикрепления");
            row.CreateCell(1).SetCellValue("Id файла");
            row.CreateCell(2).SetCellValue("Id задачи");

            rowIndex++;

            foreach (var attachment in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(attachment.AttachmentId.ToString());
                newRow.CreateCell(1).SetCellValue(attachment.FileId.ToString());
                newRow.CreateCell(2).SetCellValue(attachment.TaskId.ToString());
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
