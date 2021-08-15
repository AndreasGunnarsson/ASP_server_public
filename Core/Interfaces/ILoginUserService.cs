using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to log in and log out a user.</summary>
    /// <remarks>GenerateSessionHash() is used to generate a hash value stored in the session when a user logs in.
    /// The generated hash value is sent back to the Controller so a cookie can be generated and sent to the client.</remarks>
    public interface ILoginUserService
    {
        string GenerateSessionHash();                       // Used to generate a hash that's used for the sessionid in a cookie for a logged in user.
        string LoginUser(AccountsTemp account);
        void LogoutUser(Accounts account);
    }
}
