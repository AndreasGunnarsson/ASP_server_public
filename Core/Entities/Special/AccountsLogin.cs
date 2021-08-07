namespace Core.Entities
{
    public class AccountsLogin
    {
        int Id { get; set; }
        string Name { get; set; }
        int RolesId { get; set; }
    }
}

// Klass som används för inloggade användare; behöver inte ha information om lösenord men har information om specifik roll.
