using System;
using System.Collections.Generic;
using System.Text;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Модель для прикрепления файла к задаче
    /// </summary>
    public class AttachmentModel
    {
        /// <summary>
        /// Id задачи
        /// </summary>
        public Guid TaskId { get; set; }
        
        /// <summary>
        /// Id файлов для добавления в задачу
        /// </summary>
        public List<Guid> FileIds { get; set; }
    }
}
