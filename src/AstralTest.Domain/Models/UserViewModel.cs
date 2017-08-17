using AstralTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для отображения пользователей
    /// </summary>
    public class UserViewModel
    {
        [Display(Name ="Имя пользователя")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email пользователя")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Телефон пользователя")]
        public string ProneNumber { get; set; }


        public Guid Id { get; set; }

        
        public List<Note> Notes { get; set; }
    }
}
