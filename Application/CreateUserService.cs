using System;
using Core.Entities;
using Core.Interfaces;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;

namespace Application
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRolesRepository _repository;

        public CreateUserService(IUserRolesRepository repository)
        {
            _repository = repository;
            Console.WriteLine("DEBUG: Inside CreateUser");                              // Debug.
        }

        public bool IsAccountNameAvailable(string username)
        {
            var allAccountNames = _repository.ReadAllAccountNames();

            foreach(var v in allAccountNames)
            {
                if (v.Name == username)
                {
                    Console.WriteLine("Name: " + v.Name + " is already in use!");       // Debug.
                    return false;
                }
                Console.WriteLine("Name: " + v.Name + " is available.");                // Debug.
            }
            /* allAccountNames.FirstOrDefault */
            
            return true;
        }

        public void CreateUser(Accounts account)
        {
            // Skicka med som objekt till repository.

            var randomGenerator = RandomNumberGenerator.Create();
            byte[] password = Encoding.ASCII.GetBytes(account.PasswordHash);
            byte[] passwordSalt = new Byte[16];
            randomGenerator.GetBytes(passwordSalt);

            Console.WriteLine("Generated salt: " + Convert.ToBase64String(passwordSalt));             // Debug.
            var argon = new Argon2d(password);
            argon.Salt = passwordSalt;
            argon.DegreeOfParallelism = 2;
            argon.Iterations = 4;
            argon.MemorySize = 1024 * 64;
            var finalpwd = argon.GetBytes(30);
            Console.WriteLine("Generated pwd: " + Convert.ToBase64String(finalpwd));             // Debug.


            // TODO: Testa så att det går att logga in igen.
            // TODO: Spara i repository.
            /* Accounts test = new Accounts(0, account.Name, account.PasswordHash, "salt");            // Debug. */
            // TODO: Se över DegreeOfParallelism, Iterations och MemorySize.
            /* _repository.CreateAccount(test); */
        }
    }
}

// TODO: Sanitizea input i CreateUser.
// TODO: Flytta salt-geneering och alla settings till egna metoder för att få de på mer enhetliga ställen (då vi vill använda samma inställningar för argon när vi loggar in någon). Lägg som service i egen klass.
