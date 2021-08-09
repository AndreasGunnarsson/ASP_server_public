namespace Core.Entities
{
    /// <summary>Value object used between the Controller and the Application layer when creating an account.</summary>
    public class AccountsTemp
    {
        public string Name { get; }
        public string Password { get; }

        public AccountsTemp(string username, string password)
        {
            Name = username;
            Password = password;
        }
    }
}

// TODO: Använd init istället?
