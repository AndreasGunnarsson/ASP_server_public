namespace Core.Interfaces
{
    /// <summary>Interface with methods to create passwords and check password validity.</summary>
    public interface IPasswordManagementService
    {
        byte[] GenerateSalt();
        byte[] GeneratePassword(string password, byte[] salt);
        void CheckPassword(string username, string password);
    }
}

// TODO: CheckPassword måste returnera något.
