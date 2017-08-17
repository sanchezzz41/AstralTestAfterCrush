using AstralTest.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для изменения пользователя
    /// </summary>
    public class EditUserModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Display(Name="Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Email 
        /// </summary>
        [Display(Name = "Email")]   
        public string Email { get; set; }

        /// <summary>
        /// Телефон 
        /// </summary>
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Display(Name ="Роль")]
        public RolesOption RoleId { get; set; }

        //Если надо будет изменять пароль
        //public string Password { get; set; }

        ///// <summary>
        ///// Id пользователя
        ///// </summary>
        //public Guid Id { get; set; }
    }
}
