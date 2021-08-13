using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface for base functionality for roles and sessions.</summary>
    public interface IUserBaseService
    {
        // Lista med alla roller:
        // Behöver bara köras en gång (när programmet startar).
        /* List<Roles> allRoles(); */
        IEnumerable<Roles> allRoles { get; }        // private set. Alternativt änvänd "init"?
        // Lista med alla aktiva sessioner.
        // Används både för/av authentication och authorization.
        List<AccountsLogin> activeSessions { get; }
        // TODO: Ta bort "set" om det fungerar med "internal"? Se implementationen.
        void ReadAllSessions();                // Används för att hämta lista med alla användare i activeSessions. För att kunna läsa om de är inloggade eller ej. TODO: Kanske räcker att kolla ett session-id och returnera true/false här?
        void AddToSession(UserSession usersession);                            // För att lägga till en ny session bundet till en användare i activeSessions.
        void RemoveFromSession();                       // Tar bort en användare från activeSessions.
            // TODO: Behöver en input-parameter. Behöver jag bättre identifierare i "UserSession"? Kanske Id från databasen för användaren?
    }
}

// TODO: Döp om till IAccountsBaseService
