namespace Core.Entities
{
    /// <summary>Value object used between the PasswordManagementService and the LoginUserService to transfer a user Id and the associated role to that user.</summary>
    public class AccountsRoles
    {
        public int Id { get; }
        public int RolesId { get; }

        public AccountsRoles(int id, int rolesid)
        {
            Id = id;
            RolesId = rolesid;
        }
    }
}
