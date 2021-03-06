using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to create a new account.</summary>
    public interface ICreateUserService
    {
        bool CreateUser(AccountsTemp account);
        bool IsAccountNameAvailable(string username);
        bool UpdatePassword(string oldPassword, string newPassword, int accountId);
        void RemoveUser(int userId);
    }
}
