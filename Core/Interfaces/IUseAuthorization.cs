namespace Core.Interfaces
{
    public interface IUseAuthorization
    {
        // Om Attribut för användare == TRUE
            // Jämför med session (List).
            // Om användaren finns i session med rätt role så tillåt att gå vidare.
            // Annars redirecta med 403-HTTP-status.
    }
}

// Middleware.
// Kollar ifall en användare är authorizerad för att nå den aktuella sidan.
// Default är nej; redirecta till unathorized-sida.
// Använd attribute (som i sig är intresserad av en role) för att tillåta en route..
// Kollar cookie och jämför med session.
