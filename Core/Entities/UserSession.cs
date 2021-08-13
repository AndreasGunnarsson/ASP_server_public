using System;

namespace Core.Entities
{
    /// <summary>Value object that represents a session of a logged in user.</summary>
    public class UserSession 
    {
        public byte[] sessionId { get; set; }
        public DateTime loginDate { get; set; } 

        public UserSession(byte[] sessionid, DateTime logindate)
        {
            sessionId = sessionid;
            logindate = loginDate;
        }

        // TODO: Se över getters och setters; använd "init"?
    }
}
