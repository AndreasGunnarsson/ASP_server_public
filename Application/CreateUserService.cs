using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRolesRepository _repository;
        private readonly IPasswordManagementService _passwordManagementService;

        public CreateUserService(IUserRolesRepository repository, IPasswordManagementService passwordmanagement)
        {
            _repository = repository;
            _passwordManagementService = passwordmanagement;
        }

        public bool IsAccountNameAvailable(string username)
        {
            var allAccountNames = _repository.ReadAllAccountNames();

            foreach(var v in allAccountNames)
            {
                if (v.Name == username)
                {
                    return false;
                }
            }
            /* allAccountNames.FirstOrDefault() */
            
            return true;
        }

        public void CreateUser(AccountsTemp account)
        {
            var passwordSalt = _passwordManagementService.GenerateSalt();
            var passwordHash = _passwordManagementService.GeneratePassword(account.Password, passwordSalt);
            Account accountToDatebase = new Account(account.Name, passwordHash, passwordSalt);
            _repository.CreateAccount(accountToDatebase);
        }
    }
}

// TODO: Flytta salt-geneering och alla settings till egna metoder för att få de på mer enhetliga ställen (då vi vill använda samma inställningar för argon när vi loggar in någon). Lägg som service i egen klass.
