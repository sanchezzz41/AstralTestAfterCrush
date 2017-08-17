using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using AstralTest.Domain.Services;
using AstralTest.Tests.Domain.Entities.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AstralTest.Tests.Domain.Entities.Tests
{
    /// <summary>
    /// Класс, который проверяет сервис для работы с заметками пользователей
    /// </summary>
    [TestFixture]
    public class NoteServiceTests
    {
        private DatabaseContext _context;

        //Тестируемый сервис
        private INoteService _service;

        //Для проверки значений
        private List<Note> _noteList;

        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<NoteDataFactory>().CreateNotes();
            _noteList = await _context.Notes.ToListAsync();

            //Services
            _service = new NoteService(_context);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<NoteDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }

        /// <summary>
        /// Тест на создание заметки(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Create_Note_Success()
        {
            var userId = _context.Users.First().UserId;
            var note = new NoteModel
            {
                Text = "Какой то текст"
            };

            //act
            var resultId = await _service.AddAsync(note, userId);
            var resultNote = await _context.Notes.SingleOrDefaultAsync(x => x.NoteId == resultId);

            //assert
            Assert.AreEqual(note.Text, resultNote.Text);
        }

        /// <summary>
        /// Тест на обновление заметки(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Update_Note_Success()
        {
            var noteId = _context.Notes.First().NoteId;
            var note = new NoteModel
            {
                Text = "Какой то новый текст"
            };

            //act
            await _service.EditAsync(note, noteId);
            var resultNote = await _context.Notes.SingleOrDefaultAsync(x => x.NoteId == noteId);

            //assert
            Assert.AreEqual(note.Text, resultNote.Text);
        }

        /// <summary>
        /// Тест на удаление заметки(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Delete_Note_Success()
        {
            var noteId = _context.Notes.First().NoteId;

            //act
            await _service.DeleteAsync(noteId);
            var resultNote = await _context.Notes.SingleOrDefaultAsync(x => x.NoteId == noteId);

            //assert
            Assert.IsNull(resultNote);
        }


        /// <summary>
        /// Тест на получение всех заметок(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_Users_Sueccess()
        {
            //Act
            var noteList = await _service.GetAsync();


            //Assrt
            for (int i = 0; i < _noteList.Count; i++)
            {
                Assert.AreEqual(_noteList[i].Text, noteList[i].Text);
            }
        }

    }
}
