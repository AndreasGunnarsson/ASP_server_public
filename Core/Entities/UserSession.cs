using System;

namespace Core.Entities
{
    /// <summary>Immutable entity representing a session of a logged in user.</summary>
    public class UserSession 
    {
        public string sessionId { get; }
        public DateTime loginDate { get; }
        public int userId { get; }
        public int userRole { get; }

        public UserSession(string sessionid, DateTime logindate, int userid, int userrole)
        {
            sessionId = sessionid;
            loginDate = logindate;
            userId = userid;
            userRole = userrole;
        }
    }
}
