using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с заметками пользователя, включает стандартные операции CRUD
    /// </summary>
    public interface INoteService
    {
        IEnumerable<Note> Notes { get; }

        /// <summary>
        /// Добавляет заметку в бд
        /// </summary>
        /// <param name="noteModel"></param>
        /// <param name="idMaster"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(NoteModel noteModel, Guid idMaster);

        /// <summary>
        /// Удаляет заметку из бд
        /// </summary>
        /// <param name="id">id Заметки</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Изменяет заметку
        /// </summary>
        /// <param name="idNote"></param>
        /// <param name="newNote"></param>
        /// <returns></returns>
        Task EditAsync(NoteModel newNote, Guid idNote);

        /// <summary>
        /// Получает заметки из БД
        /// </summary>
        /// <returns></returns>
        Task<List<Note>> GetAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        Task<byte[]> NotesConvertToXssfAsync(IEnumerable<Note> list);
    }
}
