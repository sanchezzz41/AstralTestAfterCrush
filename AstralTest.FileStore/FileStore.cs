using System;
using System.IO;
using System.Threading.Tasks;

namespace AstralTest.FileStore
{
    /// <summary>
    /// Класс для работы с файлами. Все файлы храняться в одном месте.
    /// </summary>
    public class FileStore : IFileStore
    {
        private readonly FileStoreOptions _fileStoreOption;

        /// <summary>
        /// Иницилизирует новый экземпляр класс
        /// </summary>
        /// <param name="opt">Опции, которые будут использоваться при работе класса</param>
        public FileStore(FileStoreOptions opt)
        {
            _fileStoreOption = opt;
            if (!File.Exists(opt.RootPath))
            {
                Directory.CreateDirectory(opt.RootPath);
            }
        }

        /// <summary>
        /// Создаёт файл с указанным именем из текущего потока
        /// </summary>
        /// <param name="stream">Поток из которого создаётся файл</param>
        /// <param name="nameFile">Название файла</param>
        /// <returns></returns>
        public async Task Create(Stream stream, string nameFile)
        {
            if (stream.Length == 0)
            {
                throw new Exception("В потоке нету данных.");
            }
            var path = Path.Combine(_fileStoreOption.RootPath, nameFile);

            stream.Seek(0, SeekOrigin.Begin);

            byte[] resultMass;

            using (var resultStream = new MemoryStream())
            {

                await stream.CopyToAsync(resultStream);

                resultStream.Seek(0, SeekOrigin.Begin);

                resultMass = new byte[resultStream.Length];

                resultStream.Read(resultMass, 0, resultMass.Length);
            }

            using (var fileStream = new FileStream(path, FileMode.Create,
                FileAccess.Write))
            {
                await fileStream.WriteAsync(resultMass, 0, resultMass.Length);
            }
        }

        /// <summary>
        /// Возвращает файл по указанному имени 
        /// </summary>
        /// <param name="nameFile">Название файла, который будет загружаться</param>
        /// <returns></returns>
        public async Task<byte[]> Download(string nameFile)
        {
            var path = Path.Combine(_fileStoreOption.RootPath, nameFile);
            if (!File.Exists(path))
            {
                throw new Exception($"Такого файла {nameFile}  не существует!");
            }
            return await Task.FromResult(File.ReadAllBytes(path));
        }

        /// <summary>
        /// Копирует файл в указанный путь
        /// </summary>
        /// <param name="pathFrom">Путь к файлу который надо копировать</param>
        /// <param name="pathTo">Путь по которому копируемый файл будет перемещен</param>
        /// <returns></returns>
        public async Task Copy(string pathFrom, string pathTo)
        {

        }

        /// <summary>
        /// Записывает массив байтов в поток, и возвращает его
        /// </summary>
        /// <param name="bytes">Массив байтов, которые будут записаны в поток</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamFromBytes(byte[] bytes)
        {
            if (bytes.Length == 0)
            {
                return null;
            }
            return await Task.FromResult(new MemoryStream(bytes));
        }

        /// <summary>
        /// Удаляет файл по имени
        /// </summary>
        /// <param name="nameFile">Имя файла</param>
        /// <returns></returns>
        public async Task Delete(string nameFile)
        {
            var path = Path.Combine(_fileStoreOption.RootPath, nameFile);
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }
        }
    }
}
