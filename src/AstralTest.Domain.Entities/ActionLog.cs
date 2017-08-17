using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий действие пользователя в приложение 
    /// </summary>
    public class ActionLog
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Название контроллера к которому общараются
        /// </summary>
        [Required]
        public string NameOfController { get; set; }

        /// <summary>
        /// Название метода действия, к которому обращаются
        /// </summary>
        [Required]
        public string NameOfAction { get; set; }

        /// <summary>
        /// Время обращения пользователя к приложению
        /// </summary>
        [Required]
        public DateTime EnteredTime { get; set; }

        /// <summary>
        /// Внеший ключ указывающий на пользователя
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid IdUser { get; set; }

        /// <summary>
        /// Пользователь, который обращается к приложению
        /// </summary>
        [Required]
        public User User { get; set; }

        public List<InfoAboutAction> InfoAboutActions { get; set; }

        public ActionLog()
        {
            Id = Guid.NewGuid();
            EnteredTime = DateTime.Now;
        }

        public ActionLog(Guid idUser, string nameController, string nameAction)
        {
            Id = Guid.NewGuid();
            EnteredTime = DateTime.Now;
            IdUser = idUser;
            NameOfController = nameController;
            NameOfAction = nameAction;
        }
    }
}
