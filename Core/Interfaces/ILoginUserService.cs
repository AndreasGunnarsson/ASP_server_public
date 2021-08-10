using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to log in and log out a user.</summary>
    public interface ILoginUserService
    {
        void LoginUser(AccountsTemp account);
        void LogoutUser(Accounts account);
    }
}
