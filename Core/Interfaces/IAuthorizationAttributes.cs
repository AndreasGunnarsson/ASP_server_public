namespace Core.Interfaces
{
    public interface IAdminAutorizationAttribute
    {
        // Tom attribut.
    }

    public interface IUserAutorizationAttribute
    {
        // Tom attribut.
    }
}

// Ha olika attribut beroende på vem som tillåts att köra olika metoder. 
// Kan hända att det är bäst att separera admin och user-metoderna helt och hållet (även om de skulle göra snarlika saker)?


// OLD NOTES nedan --------------------------------
// Ärver från System.Attribute
// Innehåller bara properties.
// Sparar värden från databasens roles.
// [AttributeUsage]
// Man ska kunna skriva in vilken role som är intressant; t.ex: [Authorize("Admin")].
// Snyggast vore ifall man inte skrev in något utan den jämförde automatiskt värdet från session (array) med det från cookien.
// Använd sedan denna attribut de tillhörande cs-filerna till en page.
//
// Alternativt:
// Gör en metod som man kallar på i varje metod som hör till en page.
// Om cookien inte stämmer överrens med gt
//
// Alternativ:
// Det enda attributen gör är att spara ett värde om priveledge som krävs för att köra en metod.
