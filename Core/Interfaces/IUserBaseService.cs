using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>Interface with base functionality for roles and sessions.</summary>
    public interface IUserBaseService
    {
        IEnumerable<Roles> roles { get; }
        UserSession ReadSession(string sessionId);
        void AddSession(UserSession userSession);
        void RemoveSession(string sessionId);
    }
}
