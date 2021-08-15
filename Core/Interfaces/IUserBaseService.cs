using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface for base functionality for roles and sessions.</summary>
    /// <remarks>A Dictionary with active sessions should be instansiated in the class that implements this interface.</remarks>
    public interface IUserBaseService
    {
        // Lista med alla roller:
        // Behöver bara köras en gång (när programmet startar).
        /* List<Roles> allRoles(); */
        IEnumerable<Roles> allRoles { get; }        // private set. Alternativt änvänd "init"?
        // Lista med alla aktiva sessioner.
        // Används både för/av authentication och authorization.
        /* List<UserSession> activeSessions { get; } */                 // NOTE: Fungerar inte då man måste instansiera Listan mauellt.
        // TODO: Ta bort "set" om det fungerar med "internal"? Se implementationen.
        UserSession ReadSession(string sessionid);                // Används för att hämta lista med alla användare i activeSessions. För att kunna läsa om de är inloggade eller ej. TODO: Kanske räcker att kolla ett session-id och returnera true/false här?
        void AddToSession(UserSession usersession);                            // För att lägga till en ny session bundet till en användare i activeSessions.
        void RemoveFromSession();                       // Tar bort en användare från activeSessions.
            // TODO: Behöver en input-parameter. Behöver jag bättre identifierare i "UserSession"? Kanske Id från databasen för användaren?
    }
}

// TODO: Döp om till IAccountsBaseService
// TODO: Kan man göra activeSessions-listan och allRoles-listan "private" eller read-only? Ska endast gå att nå med metoder.
// TODO: Returnera List<UserSession> i activeSessions är värdelöst då man får en direkt referens till orginal-listan.
