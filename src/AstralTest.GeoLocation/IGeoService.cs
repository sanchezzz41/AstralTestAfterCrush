using System.Threading.Tasks;

namespace AstralTest.GeoLocation
{
    /// <summary>
    /// Интерфейс для работы с гео данными
    /// </summary>
    public interface IGeoService
    {
        /// <summary>
        /// Возвращает модель содержащию широту и долготу
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        Task<GeoPosition> GetLatLon(string adress);

        /// <summary>
        /// Возвращает массив байтов содержащий картинку по данной  широте и долготе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<byte[]> GetImage(GeoPosition model);
    }
}
