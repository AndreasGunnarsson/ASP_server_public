using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with methods to create passwords and check password validity.</summary>
    public interface IPasswordManagementService
    {
        byte[] GenerateSalt();
        byte[] GeneratePassword(string password, byte[] salt);
        AccountsRoles ValidatePassword(AccountsTemp accountTemp, int? accountId = null);
    }
}
