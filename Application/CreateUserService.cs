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
            
            return true;
        }

        public void CreateUser(AccountsTemp account)
        {
            var passwordSalt = _passwordManagementService.GenerateSalt();
            var passwordHash = _passwordManagementService.GeneratePassword(account.Password, passwordSalt);
            Account accountToDatebase = new Account(account.Name, passwordHash, passwordSalt);
            _repository.CreateAccount(accountToDatebase);
        }

        public bool UpdatePassword(string oldPassword, string newPassword, int accountId)
        {
            var tempAccount = new AccountsTemp("", oldPassword);
            var accountWithRole = _passwordManagementService.ValidatePassword(tempAccount, accountId);
            if (accountWithRole != null)
            {
                var passwordSalt = _passwordManagementService.GenerateSalt();
                var passwordHash = _passwordManagementService.GeneratePassword(newPassword, passwordSalt);
                Account accountToDatebase = new Account(accountId, "", passwordHash, passwordSalt);
                _repository.UpdateAccount(accountToDatebase);
                return true;
            }
            return false;
        }

        public void RemoveUser(int userId)
        {
            _repository.DeleteAccount(userId);
        }
    }
}
