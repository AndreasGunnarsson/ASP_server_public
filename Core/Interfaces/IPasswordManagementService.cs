using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to create passwords and check password validity.</summary>
    public interface IPasswordManagementService
    {
        byte[] GenerateSalt();
        byte[] GeneratePassword(string password, byte[] salt);
        AccountsRoles ValidatePassword(AccountsTemp account);           // Used when logging in to check the validity of a password for a specific account (based on the username).
        // TODO: Lägg till ChangePassword-metod.
    }
}

// TODO: CheckPassword måste returnera något.
