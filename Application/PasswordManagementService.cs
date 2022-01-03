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
            /* foreach(var v in passwordSalt) { Console.WriteLine("Generated Salt byte: " + v); }                               // Debug. */
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
            /* Console.WriteLine("Generated hash: " + Encoding.UTF8.GetString(generatedPasswordHash));                          // Debug. */
            foreach(var v in generatedPasswordHash) { Console.WriteLine("GeneratePassword: " + v); }                            // Debug.
            return generatedPasswordHash;
        }

        public AccountsRoles ValidatePassword(AccountsTemp account)
        {
            Console.WriteLine("CheckPassword parameters, username: " + account.Name + " password: " + account.Name);            // Debug.
            byte[] passwordByte = Encoding.ASCII.GetBytes(account.Password);
            var allAccountsFromDb = _repository.ReadAllAccounts();

            // TODO: Måste if-checka allAccounts.
                // Alternativt är det kanske accountUser som måste kollas; om användaren inte finns blir det tråkigt.
            Accounts singleAccount = allAccountsFromDb.FirstOrDefault(x => x.Name == account.Name);
            Console.WriteLine("account (from database): " + singleAccount.Name + " pwd: " + singleAccount.PasswordHash + " salt: " + singleAccount.PasswordSalt + " rolesid: " + singleAccount.RolesId);       // Debug.
            var generatedPassword = GeneratePassword(account.Password, singleAccount.PasswordSalt);
            /* foreach(var v in account.PasswordHash) { Console.WriteLine("Password from DB: " + v); }                          // Debug. */
            bool isSame = generatedPassword.SequenceEqual(singleAccount.PasswordHash);
            Console.WriteLine("isSame (CheckPassword): " + isSame + " username: " + account.Name);                              // Debug.
            AccountsRoles returnAccountsRoles = new AccountsRoles(singleAccount.Id, singleAccount.RolesId);
            // TODO: Se över "AccountsRoles"; behövs den? Skulle kunna använda Accounts med speciell konstruktor?

            if (isSame == true)
                return returnAccountsRoles;
            else
                return null;
        }
    }
}
