using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.FileStore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;
using File = AstralTest.Domain.Entities.File;

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public class FileService : IFileService
    {

        public IEnumerable<File> Files
        {
            get { return _context.Files; }
        }

        private readonly DatabaseContext _context;
        private readonly IFileStore _fileStore;

        public FileService(DatabaseContext context, IFileStore filestroe)
        {
            _context = context;
            _fileStore = filestroe;
        }

        /// <summary>
        /// Добавляет файл в бд и в локальное хранилище
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(FileModel fileModel)
        {
            if (fileModel == null)
            {
                throw new NullReferenceException("Файла для добавления нету.");
            }

            var result = new File(fileModel.TypeFile, fileModel.NameFile);
            await _context.Files.AddAsync(result);

            await _fileStore.Create(fileModel.StreamFile, result.FileId.ToString());

            await _context.SaveChangesAsync();

            return result.FileId;
        }

        /// <summary>
        /// Возвращает модель в которой находится файл
        /// </summary>
        /// <param name="idFile">Id файла, который надо вернуть</param>
        /// <returns></returns>
        public async Task<FileModel> GetFileAsync(Guid idFile)
        {
            var resultFile = await _context.Files.SingleOrDefaultAsync(x => x.FileId == idFile);
            if (resultFile == null)
            {
                throw new NullReferenceException($"Файла с таким id({idFile}) нету.");
            }
            var resultMass = await _fileStore.Download(resultFile.FileId.ToString());
            if (resultMass == null || resultMass.Length == 0)
            {
                throw new InvalidOperationException("Файла с таким id нету в хранилище.");
            }

            var result = new FileModel
            {
                StreamFile = new MemoryStream(resultMass),
                NameFile = resultFile.NameFile,
                TypeFile = resultFile.TypeFile
            };
            return result;
        }

        /// <summary>
        /// Удаляет файл
        /// </summary>
        /// <param name="idFile">Id файла, по которому будет производиться удаление</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idFile)
        {
            var result = await _context.Files.SingleOrDefaultAsync(x => x.FileId == idFile);
            if (result == null)
            {
                throw new NullReferenceException($"Файла с таким id({idFile}) нету.");
            }
            await _fileStore.Delete(idFile.ToString());
            _context.Files.Remove(result);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает из бд все записи о файлах
        /// </summary>
        /// <returns></returns>
        public async Task<List<File>> GetInfoAboutAllFilesAsync()
        {
            return await _context.Files
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> FilesConvertToXssfAsync(IEnumerable<File> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Users");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Название файла");
            row.CreateCell(1).SetCellValue("Тип файла");
            row.CreateCell(2).SetCellValue("Время создания");
            row.CreateCell(3).SetCellValue("Id файла");

            rowIndex++;

            foreach (var file in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(file.NameFile);
                newRow.CreateCell(1).SetCellValue(file.TypeFile);
                newRow.CreateCell(2).SetCellValue(file.CreatedTime);
                newRow.CreateCell(3).SetCellValue(file.FileId.ToString());
                rowIndex++;
            }


            for (int i = 0; i < 4; i++)
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
