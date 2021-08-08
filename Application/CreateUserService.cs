using System;
using Core.Entities;
using Core.Interfaces;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Application
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRolesRepository _repository;

        public CreateUserService(IUserRolesRepository repository)
        {
            _repository = repository;
        }

        public bool IsAccountNameAvailable(string username)
        {
            var allAccountNames = _repository.ReadAllAccountNames();

            foreach(var v in allAccountNames)
            {
                if (v.Name == username)
                {
                    Console.WriteLine("Name: " + v.Name + " is already in use and wont register as a new user.");       // Debug.
                    return false;
                }
                /* Console.WriteLine("Name: " + v.Name + " is a registered account name.");                // Debug. */
            }
            /* allAccountNames.FirstOrDefault() */
            
            return true;
        }

        public byte[] GenerateSalt()
        {
            var randomGenerator = RandomNumberGenerator.Create();
            byte[] passwordSalt = new Byte[16];
            randomGenerator.GetBytes(passwordSalt);
            Console.WriteLine("Generated salt: " + Encoding.UTF8.GetString(passwordSalt));             // Debug.
            return passwordSalt;
        }

        public byte[] GeneratePassword(string password, byte[] salt)
        {
            Console.WriteLine("GeneratePassword (parameter): " + password + " salt: " + Encoding.UTF8.GetString(salt));      // Debug.
            byte[] passwordByte = Encoding.ASCII.GetBytes(password);

            var argon = new Argon2d(passwordByte);
            argon.Salt = salt;
            argon.DegreeOfParallelism = 2;
            argon.Iterations = 4;
            argon.MemorySize = 1024 * 64;
            var generatedPasswordHash = argon.GetBytes(30);
            Console.WriteLine("Generated hash: " + Encoding.UTF8.GetString(generatedPasswordHash));             // Debug.
            return generatedPasswordHash;
            // TODO: Se över DegreeOfParallelism, Iterations och MemorySize.
            // TODO: Gör det någon skillnad på outputen om man ändrar dem?
        }

        public void CheckPassword(string username, string password)
        {
            Console.WriteLine("CheckPassword: " + username + " " + password);       // Debug.
            byte[] passwordByte = Encoding.ASCII.GetBytes(password);
            var allAccounts = _repository.ReadAllAccounts();

            // TODO: Måste null-checka/if-checka allAccounts.
            Accounts account = allAccounts.FirstOrDefault(x => x.Name == username);
            Console.WriteLine("account (from database): " + account.Name + " pwd: " + account.PasswordHash + " salt: " + account.PasswordSalt);       // Debug.
            var generatedPassword = GeneratePassword(password, Encoding.ASCII.GetBytes(account.PasswordSalt));
            var IsSame = generatedPassword.SequenceEqual(Encoding.ASCII.GetBytes(account.PasswordHash));
            Console.WriteLine("IsSame: " + IsSame + " username: " + username);                        // Debug.


            // TODO: Hämta alla accounts (repository)
                // Använd GeneratePassword och mata in det "password" och det salt som är sparat i databasen.
                // Jämför resultatet med det passwordHash som är sparat i databasen.
                // Ska man ha en ny metod i UserRolesRepository?

            // TODO: Flytta till annan klass; endast för att testa här.
        }

        public void CreateUser(Accounts account)
        {
            /* var ascii = Encoding.ASCII.GetBytes(account.Name); */
            /* var ascii2 = Encoding.UTF8.GetBytes(account.Name); */
            /* var convert = Convert.ToBase64String(ascii);     // Funkar inte! */
            /* var convert2 = Encoding.UTF8.GetString(ascii); */
            /* var convert3 = Convert.ToBase64String(ascii2); */
            /* var convert4 = Encoding.UTF8.GetString(ascii2); */
            /* foreach(var v in ascii) { Console.WriteLine(v); } */
            /* foreach(var v in ascii2) { Console.WriteLine(v); } */
            /* Console.WriteLine(ascii); */
            /* Console.WriteLine(ascii2); */
            /* Console.WriteLine(ascii2.SequenceEqual(ascii2)); */
            /* Console.WriteLine(convert); */
            /* Console.WriteLine(convert2); */
            /* Console.WriteLine(convert3); */
            /* Console.WriteLine(convert4); */

            var passwordSalt = GenerateSalt();
            var passwordHash = GeneratePassword(account.PasswordHash, passwordSalt);
            Accounts toDatebaseAccount = new Accounts(account.Name, Encoding.UTF8.GetString(passwordHash), Encoding.UTF8.GetString(passwordSalt));
            _repository.CreateAccount(toDatebaseAccount);

            Console.WriteLine("CreateUser method: " + account.Name + " " + account.PasswordHash);      // Debug.
            CheckPassword(account.Name, account.PasswordHash);

            // TODO: Testa så att det går att logga in igen.
        }
    }
}

// TODO: Sanitizea input i CreateUser.
// TODO: Flytta salt-geneering och alla settings till egna metoder för att få de på mer enhetliga ställen (då vi vill använda samma inställningar för argon när vi loggar in någon). Lägg som service i egen klass.
