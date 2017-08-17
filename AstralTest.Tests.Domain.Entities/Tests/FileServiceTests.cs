using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Services;
using AstralTest.FileStore;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using File = AstralTest.Domain.Entities.File;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс для проверки сервсиса для работы с задачами 
    /// </summary>
    [TestFixture]
    public class FileServiceTests
    {
        private DatabaseContext _context;

        //Тестируемый сервис
        private IFileService _service;

        //Для проверки значений
        private List<File> _files;

        //Вместо хранилища не компе
        private List<string> _filesStore;

        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<FileDataFactory>().CreateFiles();
            _files = await _context.Files.ToListAsync();
            _filesStore = await _context.Files.Select(x=>x.FileId.ToString()).ToListAsync();
            //Services
            _service = new FileService(_context, GetIFileStore());
        }

        [TearDown]
        public async Task CleanUp()
        {
            await TestInitializer.Provider.GetService<FileDataFactory>().Dispose();
            _filesStore.RemoveAll(x => true);
        }

        /// <summary>
        /// Подготовка сервсиса IFileStore к работе с помощью Moq
        /// </summary>
        /// <returns></returns>
        public IFileStore GetIFileStore()
        {
            Mock<IFileStore> result = new Mock<IFileStore>();

            result.Setup(x => x.Create(It.IsAny<Stream>(), It.IsAny<string>()))
                .Returns<Stream, string>(async (a, b) =>
                {
                    await Task.Run(() => _filesStore.Add(b));
                });

            result.Setup(x => x.Download(It.IsAny<string>())).Returns<string>(async x =>
                await Task.FromResult(new byte[] {0, 1, 2}));

            result.Setup(x => x.Delete(It.IsAny<string>())).Returns<string>(async x =>
            {
                var file = _filesStore.SingleOrDefault(a => a == x);
                if (file != null)
                    await Task.Run(() => _filesStore.Remove(file));
            });

            return result.Object;
        }

        /// <summary>
        /// Тест на создани файла(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreateFile_Success()
        {
            var mass = new byte[] {0, 1, 2, 3, 4, 5};
            var expectFile=new FileModel
            {
                NameFile = "Test",
                StreamFile = new MemoryStream(mass),
                TypeFile = "jpg"
            };

            //Act
            var resultId = await _service.AddAsync(expectFile);
            var resuldFile = await _context.Files.SingleOrDefaultAsync(x => x.FileId == resultId);
            var resultStoreFile = _filesStore.SingleOrDefault(x => x == resultId.ToString());

            //Assert
            Assert.AreEqual(expectFile.NameFile, resuldFile.NameFile);
            Assert.AreEqual(expectFile.TypeFile, resuldFile.TypeFile);
            //Такое сравнение, потому что в моке при добавление записываем тип длину(для примера), а название это Id
            Assert.AreEqual(resultId.ToString(), resultStoreFile);
        }

        /// <summary>
        /// Тест на загрузку файла из хранилища(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DownloadFile_Success()
        {
            var expected = _files.First();
            var file = await _context.Files.SingleOrDefaultAsync(x => x.FileId == expected.FileId);
            //Act
            var result = await _service.GetFileAsync(file.FileId);
            var resustMass=new byte[result.StreamFile.Length];
            await result.StreamFile.ReadAsync(resustMass, 0, resustMass.Length);

            //Assert
            Assert.AreEqual(expected.NameFile, result.NameFile);
            Assert.AreEqual(expected.TypeFile, result.TypeFile);
            CollectionAssert.AreEqual(new byte[] { 0, 1, 2 }, resustMass );
        }

        /// <summary>
        /// Тест на удаление файла из хранилища(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteFile_Success()
        {
            var fileId = _context.Files.First().FileId;

            //Act
            await _service.DeleteAsync(fileId);
            var result1 = await _context.Files.SingleOrDefaultAsync(x => x.FileId == fileId);
            var result2 = _filesStore.SingleOrDefault(x => x == fileId.ToString());

            //Assert
            Assert.IsNull(result1);
            Assert.IsNull(result2);

        }

        /// <summary>
        /// Тест на удаление файла из хранилища, при некорректном Id(ожидается провал)
        /// </summary>
        /// <returns></returns>
        [Test]
        public void DeleteFile_UnCurrentId_Fail()
        {
            var randomGuid = Guid.NewGuid();
            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.DeleteAsync(randomGuid), "Файла с таким id нету.");
        }

        /// <summary>
        /// Тест на получение файла (ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetFile_Success()
        {
            var testFile = _files.First();

            //Act
            var resultFile = await _service.GetFileAsync(testFile.FileId);

            //Assert
            Assert.AreEqual(testFile.NameFile, resultFile.NameFile);
            Assert.AreEqual(testFile.TypeFile, resultFile.TypeFile);
        }
    }
}
    

    

    

    

    

    

    

    
