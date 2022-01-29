using System;
using Core.Entities;
using Core.Interfaces;
using System.Security.Cryptography;

namespace Application
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IUserRolesRepository _repository;
        private readonly IPasswordManagementService _passwordManagementService;
        private readonly IUserBaseService _userBaseService;

        public LoginUserService(IUserRolesRepository repository, IPasswordManagementService passwordmanagement, IUserBaseService userbaseservice)
        {
            _repository = repository;
            _passwordManagementService = passwordmanagement;
            _userBaseService = userbaseservice;
            // TODO: Behöver jag repository här?
        }

        public string GenerateSessionHash()
        {
            var randomGenerator = RandomNumberGenerator.Create();
            byte[] sessionHash = new Byte[32];
            randomGenerator.GetBytes(sessionHash);
            /* foreach(var v in sessionHash) { Console.WriteLine("LoginUser Generated Session Hash: " + v); }                    // Debug. */
            /* Console.WriteLine("LoginUser Generated session hash (string): " + Convert.ToBase64String(sessionHash));           // Debug. */
            return Convert.ToBase64String(sessionHash);
        }

        public string LoginUser(AccountsTemp account)
        {
            /* Console.WriteLine("LoginUser: " + account.Name + " " + account.Password);                               // Debug. */

            var accountWithRole = _passwordManagementService.ValidatePassword(account);
            /* Console.WriteLine("LoginUser userId: " + accountWithRole.Id + " role: " + accountWithRole.RolesId);     // Debug. */

            if (accountWithRole != null)
            {
                /* Console.WriteLine("LoginUser accountsWithRole not null");                                           // Debug. */
                var generatedSessionHash = GenerateSessionHash();
                DateTime loginDate = DateTime.Now;
                UserSession session = new UserSession(generatedSessionHash, loginDate, accountWithRole.Id, accountWithRole.RolesId);
                /* Console.WriteLine("LoginUser session: " + session.sessionId + " " + session.loginDate.ToString() + "  " + session.userId + " " + session.userRole);     // Debug. */
                _userBaseService.AddSession(session);

                /* var test = _userbaseservice.ReadAllSessions();          // Debug. */
                /* foreach (var v in test)                                 // Debug. */
                /*     Console.WriteLine("In session: " + v.userId); */

                return session.sessionId;
            }

            return null;
        }

        public void LogoutUser(Accounts account)
        {
          // Logic for logging out here..
        }
    }
}
