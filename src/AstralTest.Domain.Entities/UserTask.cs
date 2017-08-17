using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий задачу
    /// </summary>
    public class UserTask
    {
        /// <summary>
        /// Id задачи
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid TaskId { get; set; }

        /// <summary>
        /// Текст задачи
        /// </summary>
        [Required]
        public string TextTask { get; set; }

        /// <summary>
        /// Время,когда надо закончить задачу
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Время, когда закончили задачу
        /// </summary>
        public DateTime ActualTimeEnd { get; set; }

        /// <summary>
        /// ID списка
        /// </summary>
        [ForeignKey(nameof(MasterList))]
        public Guid ListId { get; set; }

        /// <summary>
        /// Список задач
        /// </summary>
        public virtual TasksContainer MasterList { get; set; }

        /// <summary>
        /// Привязки файлов закрепленные за данной задачей
        /// </summary>
        public virtual List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Иницилизирует класс задача, автоматически создаёт Id
        /// </summary>
        public UserTask()
        {
            TaskId = Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует класс задача, автоматически создаёт Id
        /// </summary>
        /// <param name="idList">Id списка, в который надо добавить задачу</param>
        /// <param name="text">Текст задачи</param>
        /// <param name="endTime">Время, до которого надо закончить задачу</param>
        public UserTask(Guid idList, string text, DateTime endTime)
        {
            TaskId = Guid.NewGuid();
            ListId = idList;
            TextTask = text;
            EndTime = endTime;
        }
    }
}
