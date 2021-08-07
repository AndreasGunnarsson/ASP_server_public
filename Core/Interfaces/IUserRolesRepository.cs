using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Repository interface for roles and accounts.</summary>
    public interface IUserRolesRepository
    {
        IEnumerable<Roles> ReadAllRoles();                                      // Används bara i koden och potentiellt av admin (om man listar alla användare). Borde finnas en annan metod än ReadAllAccounts som admin kan använda; behöver inte hämta alla lösenord.
        void CreateAccount(Accounts account);
        IEnumerable<Accounts> ReadAllAccounts();                                // Används för att jämföra lösenord vid inloggning. Behöver inte läsa Accounts från databasen någonannanstans då de aktiva finns i session-collectionen.
        IEnumerable<AccountsNames> ReadAllAccountNames();                       // Används för att hämta alla konto-namn så att man kan jämföra dem när man skapar nytt konto.
        void UpdateAccount(Accounts account);                                   // För att uppdaera lösnord och användarnamn.
        void DeleteAccount(Accounts account);
    }
}

// TODO: Döp om till IAccountsRolesRepository.
// TODO: Speciell entitet för att Accounts som inte skickar med password när man bara ska authentisera och hämta ut alla konton?
    // Behöver kunna jämföra användarnamn så att inga duplicerade skapas.
