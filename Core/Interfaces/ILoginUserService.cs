using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to log in and log out a user.</summary>
    /// <remarks>GenerateSessionHash() generates a hash value stored in the session when a user logs in.</remarks>
    public interface ILoginUserService
    {
        UserSession CheckLogin(string sessionId);
        /* bool CheckLogin(string sessionId); */
        string GenerateSessionHash();
        string LoginUser(AccountsTemp account);
        void LogoutUser(string sessionId);
    }
}
