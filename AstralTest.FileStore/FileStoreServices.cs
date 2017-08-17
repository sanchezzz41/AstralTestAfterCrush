using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AstralTest.FileStore
{
    /// <summary>
    /// Статический класс для добавленя сервисов
    /// </summary>
    public  static class FileStoreServices
    {
        /// <summary>
        /// Метод расширения, который добавляет сервисы в IServiceCollection
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configure">Конфигурации для FileStore</param>
        /// <returns></returns>
        public static IServiceCollection AddFileStoreServices(this IServiceCollection service,
            Action<FileStoreOptions> configure)
        {
            service.AddScoped<IFileStore, FileStore>();

            var fileStoreOptions = new FileStoreOptions();
            configure(fileStoreOptions);
            service.AddSingleton(fileStoreOptions);

            //service.Configure(configure);
            //service.AddScoped(x => x.GetService<IOptionsSnapshot<FileStoreOptions>>().Value);
            return service;
        }
    }
}
