using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Tests.Domain.Entities.Factory
{
    //Заполняет бд пользователями и заметками
    public class NoteDataFactory
    {
        private readonly DatabaseContext _context;

        public NoteDataFactory()
        {
            _context = TestInitializer.Provider.GetService<DatabaseContext>();
        }

        //Заполняет бд заметками
        public async Task CreateNotes()
        {
            var user = await _context.Users.FirstAsync();
            var notes = new List<Note>
            {
                new Note("text1",user.UserId),
                new Note("text2",user.UserId)
            };
            await _context.Notes.AddRangeAsync(notes);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Dispose()
        {
            var notes = await _context.Notes.ToListAsync();
            _context.Notes.RemoveRange(notes);
            await _context.SaveChangesAsync();
        }
    }
}
