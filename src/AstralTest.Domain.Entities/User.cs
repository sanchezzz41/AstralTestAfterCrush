using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{

    /// <summary>
    /// Предоставляет класс для пользоваетля
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }


        /// <summary>
        /// Имя(Nick name)
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Соль для хэша
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Id роли
        /// </summary>
        [ForeignKey(nameof(Role))]
        public RolesOption RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Список заметок
        /// </summary>
        public virtual List<Note> Notes { get; set; }

        /// <summary>
        /// Список задач
        /// </summary>
        public List<TasksContainer> TasksContainers { get; set; }


        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        public User()
        {
            UserId = Guid.NewGuid();
        }

        /// <summary>
        /// Создаёт экземпляр класса User 
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Еmail пользователя</param>
        /// <param name="passwordSalt">Cоль для пароля</param>
        /// <param name="passworhHash">Хэш пароля</param>
        /// <param name="role">Роль</param>
        public User(string userName, string email, string phoneNumber, string passwordSalt, string passworhHash,
            RolesOption role)
        {
            UserId = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordSalt = passwordSalt;
            PasswordHash = passworhHash;
            RoleId = role;
        }
    }
}
