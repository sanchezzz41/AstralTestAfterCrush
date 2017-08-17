using System.ComponentModel.DataAnnotations;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для работы с заметкой в контроллере
    /// </summary>
    public class NoteModel
    {

        /// <summary>
        /// Текст заметки
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
