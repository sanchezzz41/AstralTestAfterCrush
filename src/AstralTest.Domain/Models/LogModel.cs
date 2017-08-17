using System.ComponentModel.DataAnnotations;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Модель содержащя информацию о пользователи, которого надо отследить
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Название контроллера
        /// </summary>
        [Required]
        public string NameController { get; set; }

        /// <summary>
        /// Название метода действия
        /// </summary>
        [Required]
        public string NameAction { get; set; }

        /// <summary>
        /// Параметры для метода действия
        /// </summary>
        public  string Parametrs { get; set; }
    }
}
