using System;
using System.Collections.Generic;
using System.Text;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Класс для востановления пароля
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Id пользователя, чей пароль будет востановлен
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// Код который отправляется пользователю для воставновления пароля
        /// </summary>
        public string Code { get; set; }
    }
}
