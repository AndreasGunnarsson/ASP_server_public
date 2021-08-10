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
            Console.WriteLine("DEBUG: Inside UserBaseService");     // Debug.
            foreach (var f in allRoles) {                           // Debug.
                Console.WriteLine("allRoles: " + f.Priveledge);     // Debug.
            }
        }

        /* public void TESTMETHOD()                                    // Debug. */
        /* { */
        /*     Console.WriteLine("DEBUG: Inside TESTMETHOD"); */
        /* } */

        public IEnumerable<Roles> allRoles { get; private set; }
        // TODO: Kolla om man kan ha "private set" eller "init". Vet inte ifall detta göt någon skillnad alls på en collection i slutändan..
        public List<AccountsLogin> activeSessions { get; private set; }
        // TODO: Kanske kan ha activeSessions som "internal set" om man implementerar en annan service? Tror inte något behöver sättas av middleware utan bara av en annan service? 
        public void AddToSession() { }              // TODO.
        public void RemoveFromSession() { }         // TODO.
    }
}

// Kan vara ett problem:
    // Do not resolve a scoped service from a singleton and be careful not to do so indirectly, for example, through a transient service. It may cause the service to have incorrect state when processing subsequent requests. It's fine to..
    // https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
