using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Dapper;

namespace Infrastructure
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly IAppDb _db;

        public UserRolesRepository(IAppDb db)
        {
            _db = db;
        }

        public IEnumerable<Roles> ReadAllRoles()
        {
            string sql = "SELECT * FROM Roles";

            var roles = _db.Connection.Query<Roles>(sql);

            return roles;
        }

        public bool CreateAccount(Account account)
        {
            string sql = "INSERT INTO Accounts (Name, PasswordHash, PasswordSalt) VALUES (@AccountName, @PwdHash, @PwdSalt)";
            var parameters = new {
                AccountName = account.Name,
                PwdHash = account.PasswordHash,
                PwdSalt = account.PasswordSalt
            };

            int rows = _db.Connection.Execute(sql, parameters);
            if (rows != 1)
                return false;
            return true;
        }

        public IEnumerable<Account> ReadAllAccounts()
        {
            string sql = "SELECT * FROM Accounts";

            var accounts = _db.Connection.Query<Account>(sql);

            return accounts;
        }

        public IEnumerable<AccountsNames> ReadAllAccountNames()
        {
            string sql = "SELECT Name FROM Accounts";

            var accountNames = _db.Connection.Query<AccountsNames>(sql);

            return accountNames;
        }

        public void UpdateAccount(Account account)
        {
            string sql = "UPDATE Accounts SET PasswordHash = @accountHash, PasswordSalt = @accountSalt WHERE Id = @accountId";
            var parameters = new {
                accountHash = account.PasswordHash,
                accountSalt = account.PasswordSalt,
                accountId = account.Id
            };

            _db.Connection.Execute(sql, parameters);
        }

        public void DeleteAccount(int accountId)
        {
            string sql = "DELETE FROM Accounts WHERE Id = @accountIdToDelete";
            var parameters = new {
                accountIdToDelete = accountId
            };

            _db.Connection.Execute(sql, parameters);
        }
    }
}
