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

        public void CreateAccount(Account account)
        {
            string sql = "INSERT INTO Accounts (Name, PasswordHash, PasswordSalt) VALUES (@AccountName, @PwdHash, @PwdSalt)";
            var parameters = new {
                AccountName = account.Name,
                PwdHash = account.PasswordHash,
                PwdSalt = account.PasswordSalt
            };

            var testresult = _db.Connection.Execute(sql, parameters);       // TODO: Får man något tillbaka?
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

            var accountnames = _db.Connection.Query<AccountsNames>(sql);

            return accountnames;
        }

        public void UpdateAccount(Account account)
        {
        }

        public void DeleteAccount(Account account)
        {
        }
    }
}

// TODO: Behöver man try/catch för att se så att uppkopplingen gick rätt till? "using" som alternativ?
