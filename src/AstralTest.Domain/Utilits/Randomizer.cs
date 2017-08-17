using System;
using System.Text;

namespace AstralTest.Domain.Utilits
{
    /// <summary>
    /// Статический класс с методами для Domain.Entites
    /// </summary>
    public static class Randomizer
    {
        /// <summary>
        /// Возвращает рандомную строку
        /// </summary>
        /// <param name="lenght">Длина возвращаемой строки</param>
        /// <returns></returns>
        public static string GetString(int lenght)
        {
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < lenght; i++)
            {
                result.Append((char)rand.Next('a', 'z'));
            }
            return result.ToString();
        }

        public static string GetNumbers(int lenght)
        {
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < lenght; i++)
            {
                result.Append(rand.Next(0,9));
            }
            return result.ToString();
        }
    }
}
