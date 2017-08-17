using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralTest.Domain.Entities
{
    /// <summary>
    /// Класс, предоставляющий файл 
    /// </summary>
    public class File
    {
        /// <summary>
        /// Id файла, под ним файл храниться в локальном хранилище
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FileId { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        [Required]
        public string TypeFile { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        [Required]
        public string NameFile { get; set; }

        /// <summary>
        /// Время создания файла
        /// </summary>
        public DateTime CreatedTime { get; set; }

   
        /// <summary>
        /// Иницилизирует новый экземпрял AstralFile
        /// </summary>
        public File()
        {
            FileId = Guid.NewGuid();
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        ///  Иницилизирует новый экземпрял AstralFile
        /// </summary>
        /// <param name="typeFile">Тип файла</param>
        /// <param name="nameFile">Название файла</param>
        public File(string typeFile,string nameFile)
        {
            FileId = Guid.NewGuid();
            TypeFile = typeFile;
            NameFile = nameFile;
            CreatedTime = DateTime.Now;
        }
    }
}
