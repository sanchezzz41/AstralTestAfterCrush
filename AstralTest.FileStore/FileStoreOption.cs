using System;
using System.Collections.Generic;
using System.Text;

namespace AstralTest.FileStore
{
    /// <summary>
    /// Класс, который содержит настройки для FileStore
    /// </summary>
    public class FileStoreOptions
    {
        /// <summary>
        /// Локальный путь, где будут храниться файлы
        /// </summary>
        public string RootPath { get; set; }
    }
}
