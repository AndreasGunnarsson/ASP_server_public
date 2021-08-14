using System;

namespace Core.Entities
{
    /// <summary>Value object that represents a session of a logged in user.</summary>
    public class UserSession 
    {
        public byte[] sessionId { get; }               // Måste jämföra denna property med cookien varje request.
        public DateTime loginDate { get; }             // Kollas också varje request av någon metod för att invalidera sessionen om datumet gått ut. Säg efter kanske 1h.
                                                                // Kanske borde ha checken i något event (när någon gör något på hemsidan) eller en fast timer som koller t.ex. varje minut. Tänker ifall användaren bara är inne på hemsidan en väldigt kort stund så att inte sessionen hinner avslutas..
                                                                    // Är dock inte är säkrare att checka så ofta dock då användaren ändå kommer att försvinna från sessionen om datumet inte stämmer.
                                                                        // Kanske ha en upprensnings-check som kör en gång om dagen eller något för att ta bort alla sessioner som fortfarande är kvar.
        public int userId { get; }
        public int userRole { get; }
        /* public string userName { get; set; } */

        public UserSession(byte[] sessionid, DateTime logindate, int userid, int userrole)
        {
            sessionId = sessionid;
            loginDate = logindate;
            userId = userid;
            userRole = userrole;
        }

        // TODO: Se över getters och setters; använd "init"?
        // TODO: Behövs Id (på användaren)?
        // TODO: Vart hanterar jag "Roles"?
    }
}
