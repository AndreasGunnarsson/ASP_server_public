namespace Core.Entities
{
    /// <summary>Entity that represents an account.</summary>
    public class Account
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public byte[] PasswordHash { get; init; }
        public byte[] PasswordSalt { get; init; }
        public int RolesId { get; }

        public Account() { }

        public Account(int id, string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public Account(string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
