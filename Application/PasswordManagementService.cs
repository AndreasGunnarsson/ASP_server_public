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
            return passwordSalt;
        }

        public byte[] GeneratePassword(string password, byte[] salt)
        {
            byte[] passwordByte = Encoding.ASCII.GetBytes(password);

            var argon = new Argon2d(passwordByte);
            argon.Salt = salt;
            argon.DegreeOfParallelism = 2;
            argon.Iterations = 4;
            argon.MemorySize = 1024 * 64;
            var generatedPasswordHash = argon.GetBytes(30);
            return generatedPasswordHash;
        }

        public AccountsRoles ValidatePassword(AccountsTemp accountTemp)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(accountTemp.Password);
            var accounts = _repository.ReadAllAccounts();

            Account account = accounts.FirstOrDefault(x => x.Name == accountTemp.Name);
            if (account != null)
            {
                var generatedPassword = GeneratePassword(accountTemp.Password, account.PasswordSalt);
                bool isPasswordMatch = generatedPassword.SequenceEqual(account.PasswordHash);
                AccountsRoles returnAccountsRoles = new AccountsRoles(account.Id, account.RolesId);

                if (isPasswordMatch == true)
                    return returnAccountsRoles;
                else
                    return null;
            }
            else
                return null;
        }
    }
}
