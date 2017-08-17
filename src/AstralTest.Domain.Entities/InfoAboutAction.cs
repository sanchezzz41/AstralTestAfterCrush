using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий информацию о том, к чему обращается пользователь
    /// </summary>
    public class InfoAboutAction
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Параметры для передачи методу в контроллере
        /// </summary>
        public string JsonParameters { get; set; }

        /// <summary>
        /// Внешний ключ который ссылается на пользователя, который обращается к приложению
        /// </summary>
        [ForeignKey(nameof(Action))]
        public Guid IdAction { get; set; }

        public ActionLog Action { get; set; }

        public InfoAboutAction()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует новый экземпляр класса
        /// </summary>
        /// <param name="idEntereUser">Id действия для приложения</param>
        /// <param name="paremetrsAction">Параметры передаваемые методу действия</param>
        public InfoAboutAction(Guid idEntereUser, string paremetrsAction)
        {
            Id = Guid.NewGuid();
            IdAction = idEntereUser;
            JsonParameters = paremetrsAction;
        }
    }
}
