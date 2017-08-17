using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;



namespace AstralTest.GeoLocation
{
    /// <summary>
    /// Класс для добавления сервисов для работы с картой
    /// </summary>
    public static class GeoServices
    {
        /// <summary>
        /// Метод расширения добавляющий в DI класс для работы с гео данными
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddGeoService(this IServiceCollection service)
        {
            service.AddScoped<IGeoService, YandexGeoService>();
            return service;

        }
    }
}
