using Core.Entities;

namespace Core.Interfaces
{
    public interface IAuthentication
    {
        // Hämtar användare (från databas) och roll. Lägger till i activeSessions. Skickar en cookie.
        // Se till så att man bara kan logga in som admin från locahost?
        // TODO: Måste ta någon input-typ med info om vem som loggar in? Eller går det att göra direkt med razor pages; bara kolla input?
        /* AccountsLogin loginUser(); */
        // Tar bort från activeSession (se IUserBase), uppderar login-informationen på hemsidan samt förstör kakor på klient-sidan.
        /* void logoutUser(AccountsLogin account); */
    }
}

// TODO: Ta bort detta interface? Tror inte det används alls?

// Methoder för att logga in och logga ut.
// TODO: Vi vill egentligen inte att dessa metoder (loginUser och logutUser) ska hantera hemsidan då det är MVC/UI-delen av programmet.
    // Kanske att man bara returnerar true eller false istället?
// Service?
// Används av razor pages?
