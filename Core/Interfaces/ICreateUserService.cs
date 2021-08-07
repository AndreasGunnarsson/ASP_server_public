using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to create a new account.</summary>
    public interface ICreateUserService
    {
        // Method to compare all account user names in the database with the parameter "username".
        bool IsAccountNameAvailable(string username);
        // Method to create a new user. Does password hashing and salting.
        void CreateUser(Accounts account);
    }
}

// TODO: Borde heta CreateAccount?
// TODO: Borde kolla email-address i readAllAccountNames i framtiden.
