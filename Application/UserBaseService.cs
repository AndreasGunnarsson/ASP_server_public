using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class UserBaseService : IUserBaseService
    {
        private readonly IUserRolesRepository _repository;
        public IEnumerable<Roles> roles { get; init; }
        private Dictionary<string, UserSession> activeSessions;
        // TODO: Kanske kan ha activeSessions som "internal set" om man implementerar en annan service? Tror inte något behöver sättas av middleware utan bara av en annan service? 
        // TODO: System.Collections.Concurrent. 

        public UserBaseService(IUserRolesRepository repository)
        {
            _repository = repository;
            // TODO: Hämtar alla roller från repository.
                // Måste ske genom DI (Startup.cs) på något sätt..
            activeSessions = new Dictionary<string, UserSession>();
            roles = _repository.ReadAllRoles();
        }


        public UserSession ReadSession(string sessionId)
        {
            var userSession = activeSessions[sessionId];
            return userSession;
            // TODO: Kanske räcker att man returnerar en UserSession istället för hela listan om man kan ta en input-parameter?
                // Problemet är att jag använder activeSession-listan för att ta reda på vem som är inloggad.
                    // Alternativ: Spara UserId eller UserName i session-cookien.
                        // Problem: Vill helst inte dela sådan information med klienten.
                        // Vad skulle man använda för delimiter? Smartast att lägga i slutet av sessionId?
                    // Alternativ: Två listor; en med UserId och sessionId och en annan med resten. Resultat: Ger inget.
            // TODO: Vi vill inte jämföra alla session-id utan endast för den aktiva användaren; blir mycket att gå igenom annars. Bäst vore om vi endast användet Id:t.
        }

        public void AddSession(UserSession usersession)
        {
            activeSessions.Add(usersession.sessionId, usersession);
        }

        public void RemoveSession(string sessionId)
        {
            activeSessions.Remove(sessionId);
        }
    }
}

// Kan vara ett problem:
    // Do not resolve a scoped service from a singleton and be careful not to do so indirectly, for example, through a transient service. It may cause the service to have incorrect state when processing subsequent requests. It's fine to..
    // https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
