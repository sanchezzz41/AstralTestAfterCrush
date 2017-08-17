namespace AstralTest.Identity
{
    /// <summary>
    /// Интерфейс для хэширования пароля методом MD5
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Возвращает хэш пароля методом MD5
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        string GetHash(string hashString);
    }
}
