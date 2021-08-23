using System;
using Core.Entities;
using Core.Interfaces;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Application
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IUserRolesRepository _repository;
        private readonly IPasswordManagementService _passwordmanagement;
        private readonly IUserBaseService _userbaseservice;

        public LoginUserService(IUserRolesRepository repository, IPasswordManagementService passwordmanagement, IUserBaseService userbaseservice)
        {
            _repository = repository;
            _passwordmanagement = passwordmanagement;
            _userbaseservice = userbaseservice; 
            // TODO: Behöver jag repository här?
        }

        public string GenerateSessionHash()
        {
            var randomGenerator = RandomNumberGenerator.Create();
            byte[] sessionHash = new Byte[32];
            randomGenerator.GetBytes(sessionHash);
            foreach(var v in sessionHash) { Console.WriteLine("Generated Session Hash: " + v); }                          // Debug.
            Console.WriteLine("Generated session hash (string): " + Convert.ToBase64String(sessionHash));                  // Debug.
            return Convert.ToBase64String(sessionHash);
            // TODO: Lägg till i interface.
            // TODO: Går det bra med endast bytes i cookes?
        }

        public string LoginUser(AccountsTemp account)
        {
            Console.WriteLine("LoginUser: " + account.Name + " " + account.Password);           // Debug.

            var accountWithRole = _passwordmanagement.ValidatePassword(account);
            Console.WriteLine("LoginUser userId: " + accountWithRole.Id + " role: " + accountWithRole.RolesId);         // Debug.

            if (accountWithRole != null)
            {
                Console.WriteLine("LoginUser accountsWithRole not null");                       // Debug.
                var generatedSessionHash = GenerateSessionHash();
                DateTime loginDate = DateTime.Now;
                UserSession session = new UserSession(generatedSessionHash, loginDate, accountWithRole.Id, accountWithRole.RolesId);
                /* Console.WriteLine("LoginUser session: " + session.sessionId + " " + session.loginDate.ToString() + "  " + session.userId + " " + session.userRole);     // Debug. */
                Console.WriteLine("LoginUser session: " + session.sessionId + " " + session.loginDate.ToString() + "  " + session.userId + " " + session.userRole);     // Debug.
                _userbaseservice.AddToSession(session);

                /* var test = _userbaseservice.ReadAllSessions();          // Debug. */
                /* foreach (var v in test)                                 // Debug. */
                /*     Console.WriteLine("In session: " + v.userId); */

                return session.sessionId;
            }

            return null;
            // TODO: Behöver returnera något vettigt här ifall accountsWithRole == null.

            
            // För att logga in.
                // Måste använda PasswordManagementService.IsValidPassword()
                    // Om sant så lägg till användaren i session > UserBaseService.AddToSession
                        // Lägger till session-id och tid som användaren loggade in.
                        // if-checka först ifall användaren redan finns i session.
                        // Behöver också hantera cookies på något sätt; lägga till en session cookie för inloggad användare.
                        // Måste generera något session-id.
                            // Läs om HMAC.
        }

        public void LogoutUser(Accounts account)
        {
            // För att logga ut.
                // Läser användaren.
                // Tar bort från session.
                // Skicka förfrågan om att ta bort cookie för användaren.
        }
    }
}
