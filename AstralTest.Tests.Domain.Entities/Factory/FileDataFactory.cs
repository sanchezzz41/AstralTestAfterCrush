using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Tests.Domain.Entities.Factory
{
    //Заполняет бд файлами
    public class FileDataFactory
    {
        private readonly DatabaseContext _context;

        public FileDataFactory()
        {
           _context = TestInitializer.Provider.GetService<DatabaseContext>();
        }

        //Заполняет бд заметками
        public async Task CreateFiles()
        {
            var files = new List<File>
            {
                new File("testType1","testName1"),
                new File("testType2","testName2")
            };
            await _context.Files.AddRangeAsync(files);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var files = await _context.Files.ToListAsync();
            _context.Files.RemoveRange(files);
            await _context.SaveChangesAsync();
        }
    }
}
