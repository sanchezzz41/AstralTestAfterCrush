using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для работы с контейнером задач(Add и Edit)
    /// </summary>
    public class TasksContainerModel
    {
        /// <summary>
        /// Название контейнера
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
