using AstralTest.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс модель для регистрации пользователя в приложение
    /// </summary>
    public class UserRegisterModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Email 
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        public RolesOption RoleId { get; set; } = RolesOption.User;
    }
}
