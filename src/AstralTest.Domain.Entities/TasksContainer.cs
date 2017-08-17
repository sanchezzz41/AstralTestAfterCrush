using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс, содержащий список задач
    /// </summary>
    public class TasksContainer
    {
        /// <summary>
        /// Id контейнера задач
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ListId { get; set; }

        /// <summary>
        /// Название контейнера
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Содержит задачи
        /// </summary>
        public virtual List<UserTask> Tasks { get; set; }

        /// <summary>
        /// ID хозяина
        /// </summary>
        [ForeignKey(nameof(Master))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Хозяин списка
        /// </summary>
        public virtual User Master { get; set; }

        /// <summary>
        /// Иницилизирует контейнер задач, автоматически создаёт Id
        /// </summary>
        public TasksContainer()
        {
            ListId = Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует контейнер задач, автоматически создаёт Id
        /// </summary>
        /// <param name="idUser">Id пользователя, которому добавляется контейненр</param>
        /// <param name="name">Имя контейнера</param>
        public TasksContainer(Guid idUser,string name)
        {
            ListId = Guid.NewGuid();
            UserId = idUser;
            Name = name;
        }
    }
}
