using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий роль для пользователя
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id Роли
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RolesOption RoleId { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }
    
        /// <summary>
        /// Пользователи, которые имеют эту роль
        /// </summary>
        public virtual List<User> Users { get; set; }

        public Role()
        {

        }
        /// <summary>
        /// Иницилизурет новый экземляр класса Role 
        /// </summary>
        /// <param name="role">Сама роль</param>
        /// <param name="RoleName">Имя роли</param>
        public Role(RolesOption role,string roleName)
        {

            RoleName = roleName;
            RoleId = role;
        }
    }
}
 