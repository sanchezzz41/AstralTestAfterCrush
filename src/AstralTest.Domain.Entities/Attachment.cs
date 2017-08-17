using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс который связывает задачу и файл
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// PК Id прикрепления
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AttachmentId { get; set; }

        /// <summary>
        /// Id задачи
        /// </summary>
        [ForeignKey(nameof(MasterTask))]
        public Guid TaskId { get; set; }

        /// <summary>
        /// Id файла
        /// </summary>
        [ForeignKey(nameof(MasterFile))]
        public Guid FileId { get; set; }

        public UserTask MasterTask { get; set; }
        public File MasterFile { get; set; }

        public Attachment()
        {
            AttachmentId=Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует новый экземпрял класса
        /// </summary>
        /// <param name="taskId">Id задачаи</param>
        /// <param name="fileId">Id файла</param>
        public Attachment(Guid taskId, Guid fileId)
        {
            AttachmentId = Guid.NewGuid();
            TaskId = taskId;
            FileId = fileId;
        }
    }
}
