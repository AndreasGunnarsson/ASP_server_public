using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class UserBaseService : IUserBaseService
    {
        private readonly IUserRolesRepository _repository;

        public UserBaseService(IUserRolesRepository repository)
        {
            _repository = repository;
            // TODO: Hämtar alla roller från repository.
                // Måste ske genom DI (Startup.cs) på något sätt..
            /* allRoles.Add(new Roles { Id = 50, Priveledge = "fis" }); */
            /* Console.WriteLine("allRoles: " + allRoles[0]);      // Debug. */

            allRoles = _repository.ReadAllRoles();
            /* Console.WriteLine("DEBUG: Inside UserBaseService");     // Debug. */
            foreach (var f in allRoles) {                           // Debug.
                Console.WriteLine("allRoles: " + f.Priveledge);     // Debug.
            }
        }

        public IEnumerable<Roles> allRoles { get; init; }
        // TODO: Kolla om man kan ha "private set" eller "init". Vet inte ifall detta göt någon skillnad alls på en collection i slutändan..
        /* public List<UserSession> activeSessions { get; private set; } */
        /* private List<UserSession> activeSessions = new List<UserSession>(); */
        private Dictionary<string, UserSession> activeSessions = new Dictionary<string, UserSession>();
        // TODO: Kanske kan ha activeSessions som "internal set" om man implementerar en annan service? Tror inte något behöver sättas av middleware utan bara av en annan service? 
        // TODO: Använd inte en List utan något där man kan ha ett index så det går snabbare att leta upp allt.
            // Lägg sessionId som index och UserSession som andra typen i denna collection?
        // TODO: System.Collections.Concurrent. 

        public UserSession ReadSession(string sessionid)
        {
            var userSession = activeSessions[sessionid];
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

        public void RemoveSession()
        {
            // TODO
            // Måste ha en input-paramete i metoden.
            // Måste föst leta upp vilket Id som används.
                // Antingen om man skickar med det i cookien eller om man letar upp utifrån det sessionId som användaren har i sin cookie.
        }
    }
}

// Kan vara ett problem:
    // Do not resolve a scoped service from a singleton and be careful not to do so indirectly, for example, through a transient service. It may cause the service to have incorrect state when processing subsequent requests. It's fine to..
    // https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
