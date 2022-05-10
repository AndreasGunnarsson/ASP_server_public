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
        }

        public UserSession CheckLogin(string sessionId)
        {
            var userSession = _userBaseService.CheckSessionId(sessionId);
            return userSession;
        }

        public string GenerateSessionHash()
        {
            var randomGenerator = RandomNumberGenerator.Create();
            byte[] sessionHash = new Byte[32];
            randomGenerator.GetBytes(sessionHash);
            return Convert.ToBase64String(sessionHash);
        }

        public string LoginUser(AccountsTemp account)
        {
            var accountWithRole = _passwordManagementService.ValidatePassword(account);

            if (accountWithRole != null)
            {
                var generatedSessionHash = GenerateSessionHash();
                DateTime loginDate = DateTime.Now;
                UserSession session = new UserSession(generatedSessionHash, loginDate, accountWithRole.Id, accountWithRole.RolesId);
                _userBaseService.AddSession(session);

                return session.sessionId;
            }

            return null;
        }

        public void LogoutUser(string sessionId)
        {
            _userBaseService.RemoveSession(sessionId);
        }
    }
}
