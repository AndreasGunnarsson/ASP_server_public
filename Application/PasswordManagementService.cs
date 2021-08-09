using System;
using Core.Entities;
using Core.Interfaces;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Application
{
    public class PasswordManagementService : IPasswordManagementService
    {
        private readonly IUserRolesRepository _repository;

        public PasswordManagementService(IUserRolesRepository repository)
        {
            _repository = repository;
        }

        public byte[] GenerateSalt()
        {
            var randomGenerator = RandomNumberGenerator.Create();
            byte[] passwordSalt = new Byte[16];
            randomGenerator.GetBytes(passwordSalt);
            /* foreach(var v in passwordSalt) { Console.WriteLine("Generated Salt byte: " + v); }                          // Debug. */
            return passwordSalt;
        }

        public byte[] GeneratePassword(string password, byte[] salt)
        {
            /* Console.WriteLine("GeneratePassword (parameter): " + password + " salt: " + Encoding.UTF8.GetString(salt));      // Debug. */
            byte[] passwordByte = Encoding.ASCII.GetBytes(password);

            var argon = new Argon2d(passwordByte);
            argon.Salt = salt;
            argon.DegreeOfParallelism = 2;
            argon.Iterations = 4;
            argon.MemorySize = 1024 * 64;
            var generatedPasswordHash = argon.GetBytes(30);
            /* Console.WriteLine("Generated hash: " + Encoding.UTF8.GetString(generatedPasswordHash));             // Debug. */
            foreach(var v in generatedPasswordHash) { Console.WriteLine("GeneratePassword: " + v); }                          // Debug.
            return generatedPasswordHash;
            // TODO: Se över DegreeOfParallelism, Iterations och MemorySize.
            // TODO: Gör det någon skillnad på outputen om man ändrar dem?
        }

        public void CheckPassword(string username, string password)
        {
            Console.WriteLine("CheckPassword parameters, username: " + username + " password: " + password);       // Debug.
            byte[] passwordByte = Encoding.ASCII.GetBytes(password);
            var allAccounts = _repository.ReadAllAccounts();

            // TODO: Måste if-checka allAccounts.
            Accounts account = allAccounts.FirstOrDefault(x => x.Name == username);
            Console.WriteLine("account (from database): " + account.Name + " pwd: " + account.PasswordHash + " salt: " + account.PasswordSalt);       // Debug.
            var generatedPassword = GeneratePassword(password, account.PasswordSalt);
            /* foreach(var v in account.PasswordHash) { Console.WriteLine("Password from DB: " + v); }                          // Debug. */
            var isSame = generatedPassword.SequenceEqual(account.PasswordHash);
            Console.WriteLine("isSame: " + isSame + " username: " + username);                        // Debug.
        }
    }
}
