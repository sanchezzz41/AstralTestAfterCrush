using System.Security.Cryptography;
using System.Text;

namespace AstralTest.Identity
{
    /// <summary>
    /// Класс для хэширования пароля методом MD5
    /// </summary>
    public class Md5HashService : IHashProvider
    {
        //Хэширует пароль
        public string GetHash(string pass)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
