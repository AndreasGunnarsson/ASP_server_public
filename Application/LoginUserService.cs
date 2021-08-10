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

        public void LoginUser(AccountsTemp account)
        {
            // För att logga in.
                // Måste använda PasswordManagementService.CheckPassword
                    // Om sant så lägg till användaren i session > UserBaseService.AddToSession
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
