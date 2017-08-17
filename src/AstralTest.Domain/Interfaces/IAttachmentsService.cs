using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace AstralTest.Domain.Interfaces
{
    /// <summary>
    /// Сервер для привязки файла к задаче
    /// </summary>
    public interface IAttachmentsService
    {
        /// <summary>
        /// Список прикреплений
        /// </summary>
        IEnumerable<Attachment> Attachments { get; }

        /// <summary>
        /// Прикрепляет файл к задаче, используя id обоих
        /// </summary>
        /// <param name="attachModel"></param>
        /// <returns></returns>
        Task<List<Guid>> AttachFileToTaskAsync(AttachmentModel attachModel);

        /// <summary>
        /// Удаляет привязку между задачей и файлом
        /// </summary>
        /// <param name="attachId"></param>
        /// <returns></returns>
        Task DeleteAttachmentAsync(Guid attachId);
                                                    
        /// <summary>
        /// Возвращает список всех привязок
        /// </summary>
        /// <returns></returns>
        Task<List<Attachment>> GetAllattachmentsAsync();

        /// <summary>
        /// Возвращает список привязок по Id задачи
        /// </summary>
        /// <param name="idTask"></param>
        /// <returns></returns>
        Task<List<Attachment>> GetAllattachmentsAsync(Guid idTask);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<byte[]> AttachmentsConvertToXssfAsync(IEnumerable<Attachment> list);
    }
}
