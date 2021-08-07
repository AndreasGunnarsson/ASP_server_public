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
        List<AccountsLogin> activeSessions { get; set; }
        // TODO: Ta bort "set" om det fungerar med "internal"? Se implementationen.
        void TESTMETHOD();      // TODO: Ta bort.
    }
}

// TODO: Döp om till IAccountsBaseService
