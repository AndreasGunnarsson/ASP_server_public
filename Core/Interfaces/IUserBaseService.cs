using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface for base functionality for roles and sessions.</summary>
    /// <remarks>A Dictionary with active sessions should be instansiated in the class that implements this interface.</remarks>
    public interface IUserBaseService
    {
        IEnumerable<Roles> allRoles { get; }                            // set: init. Lista med alla roller; hämtad från databasen i konstruktorn för UserBaseService.
        // Dictionary<string, UserSession>
        UserSession ReadSession(string sessionid);                      // Används för att hämta en specifik session från samlingen med alla sessioner.
        void AddSession(UserSession usersession);                       // För att lägga till en ny session bundet till en användare i listan med sessioner.
        void RemoveSession();                                           // Tar bort en användare från listan med sessioner.
            // TODO: Behöver en input-parameter. Behöver jag bättre identifierare i "UserSession"? Kanske Id från databasen för användaren?
    }
}

// TODO: Döp om till IAccountsBaseService
// TODO: Kan man göra activeSessions-listan och allRoles-listan "private" eller read-only? Ska endast gå att nå med metoder.
// TODO: Returnera List<UserSession> i activeSessions är värdelöst då man får en direkt referens till orginal-listan.
