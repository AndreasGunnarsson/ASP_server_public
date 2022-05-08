namespace Core.Entities
{
    /// <summary>Entity that represents an account.</summary>
    public class Account
    {
        public int Id { get; }
        public string Name { get; init; }
        public byte[] PasswordHash { get; init; }
        public byte[] PasswordSalt { get; init; }
        public int RolesId { get; }
        // TODO: RolesId as uint?

        public Account() { }

        public Account(string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
