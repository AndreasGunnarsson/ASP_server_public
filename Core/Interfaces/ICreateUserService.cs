using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to create a new account.</summary>
    public interface ICreateUserService
    {
        void CreateUser(AccountsTemp account);
        bool IsAccountNameAvailable(string username);
    }
}
