namespace Core.Entities
{
    /// <summary>Entity that represents an account.</summary>
    public class Accounts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public Accounts(int id, string name, string passwordhash, string passwordsalt)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordhash;
            PasswordSalt = passwordsalt; 
        }

        /// <summary>Overload used when converting from a MVC model to use in the Application layer. Still not a password hash, just plain text password.</summary>
        public Accounts(string username, string password)
        {
            Name = username;
            PasswordHash = password;
        }
    }
}

// Behövs overloaden med passwordhash och passwordsalt? Antagligen senare.
// TODO: Använd private set?
