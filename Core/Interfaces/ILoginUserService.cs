using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to log in and log out a user.</summary>
    public interface ILoginUserService
    {
        byte[] GenerateSessionHash();                       // Used to generate a hash that's used for the sessionid in a cookie for a logged in user.
        void LoginUser(AccountsTemp account);
        void LogoutUser(Accounts account);
    }
}
