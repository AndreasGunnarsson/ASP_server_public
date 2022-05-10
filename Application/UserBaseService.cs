using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class UserBaseService : IUserBaseService
    {
        private readonly IUserRolesRepository _repository;
        public IEnumerable<Roles> roles { get; init; }          // TODO: Använd denna till något?
        private Dictionary<string, UserSession> activeSessions;

        public UserBaseService(IUserRolesRepository repository)
        {
            _repository = repository;
            activeSessions = new Dictionary<string, UserSession>();
            roles = _repository.ReadAllRoles();
        }

        public UserSession CheckSessionId(string sessionId)
        {
            if (sessionId != null)
            {
                UserSession userSession = null;
                var isFound = activeSessions.TryGetValue(sessionId, out userSession);
                if (isFound)
                    return userSession;
                else
                    return null;
            }
            else
                return null;
        }

        public void AddSession(UserSession userSession)
        {
            activeSessions.Add(userSession.sessionId, userSession);
        }

        public void RemoveSession(string sessionId)
        {
            activeSessions.Remove(sessionId);
        }
    }
}
