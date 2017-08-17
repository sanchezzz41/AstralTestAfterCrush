using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace AstralTest.Identity
{
    public class Md5PasswordHasher : IPasswordHasher<User>
    {
        private readonly IHashProvider _hashProvider;

        public Md5PasswordHasher(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        /// <summary>
        /// Хэширования пароля+соль
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(User user, string password)
        {
            //Передаем с солью
            if (user == null)
            {
                return _hashProvider.GetHash(password);
            }
            return _hashProvider.GetHash(password + user.PasswordSalt);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return _hashProvider.GetHash(providedPassword + user.PasswordSalt) == hashedPassword
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
