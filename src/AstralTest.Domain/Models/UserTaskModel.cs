using System;
using System.ComponentModel.DataAnnotations;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Модель для работы с задачей из контейнера
    /// </summary>
    public class UserTaskModel
    {
        /// <summary>
        /// Текст задачи
        /// </summary>
        [Required]
        [Display(Name = "Текст задачи")]
        public string TextTask { get; set; }

        /// <summary>
        /// Время,когда надо закончить задачу
        /// </summary>
        [Required]
        [Display(Name = "Время, когда надо завершить задачу")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Время, когда закончили задачу
        /// </summary>
        [Display(Name = "Время, когда завершили задачу")]
        public DateTime? ActualTimeEnd { get; set; }
    }
}
