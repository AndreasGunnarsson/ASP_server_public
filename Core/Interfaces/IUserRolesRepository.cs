using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Repository interface for roles and accounts.</summary>
    public interface IUserRolesRepository
    {
        IEnumerable<Roles> ReadAllRoles();
        bool CreateAccount(Account account);
        IEnumerable<Account> ReadAllAccounts();
        IEnumerable<AccountsNames> ReadAllAccountNames();
        void UpdateAccount(Account account);
        void DeleteAccount(int accountId);
    }
}
