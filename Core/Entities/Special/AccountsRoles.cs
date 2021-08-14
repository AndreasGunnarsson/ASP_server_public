namespace Core.Entities
{
    /// <summary>Value object used between the PasswordManagementService and the LoginUserService to transfer a user Id and the associeated role to that user.</summary>
    public class AccountsRoles
    {
        public int Id { get; }
        /* string Name { get; set; } */
        public int RolesId { get; }

        public AccountsRoles(int id, int rolesid)
        {
            Id = id;
            RolesId = rolesid;
        }
    }
}

// TODO: Ska kanske inte vara ett value object då den innehåller ett id?
// Klass som används för inloggade användare; behöver inte ha information om lösenord men har information om specifik roll.
