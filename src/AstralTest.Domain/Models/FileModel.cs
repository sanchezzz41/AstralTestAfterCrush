using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AstralTest.Domain.Models
{
    /// <summary>
    /// Модель файлa
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Поток для файла
        /// </summary>
        public Stream StreamFile { get; set; }
        
        /// <summary>
        /// Тип файла
        /// </summary>
        public string TypeFile { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string NameFile { get; set; }

        public byte[] Buffer { get; set; }
    }
}
