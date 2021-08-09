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
        private readonly IPasswordManagementService _passwordmanagement;

        public CreateUserService(IUserRolesRepository repository, IPasswordManagementService passwordmanagement)
        {
            _repository = repository;
            _passwordmanagement = passwordmanagement;
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

        public void CreateUser(AccountsTemp account)
        {
            var passwordSalt = _passwordmanagement.GenerateSalt();
            var passwordHash = _passwordmanagement.GeneratePassword(account.Password, passwordSalt);
            Accounts accountToDatebase = new Accounts(account.Name, passwordHash, passwordSalt);
            _repository.CreateAccount(accountToDatebase);

            Console.WriteLine("CreateUser method: " + account.Name + " " + account.Password);      // Debug.
            _passwordmanagement.CheckPassword(account.Name, account.Password);      // Debug.
            /* _passwordmanagement.CheckPassword("fis", "bobby");          // Debug. */

            // TODO: Testa så att det går att logga in igen.
        }
    }
}

// TODO: Sanitizea input i CreateUser.
// TODO: Flytta salt-geneering och alla settings till egna metoder för att få de på mer enhetliga ställen (då vi vill använda samma inställningar för argon när vi loggar in någon). Lägg som service i egen klass.
