using System;
using Core.Entities;
using Core.Interfaces;

namespace Application
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRolesRepository _repository;

        public CreateUserService(IUserRolesRepository repository)
        {
            _repository = repository;
            Console.WriteLine("DEBUG: Inside CreateUser");                              // Debug.
        }

        public bool IsAccountNameAvailable(string username)
        {
            var allAccountNames = _repository.ReadAllAccountNames();

            foreach(var v in allAccountNames)
            {
                if (v.Name == username)
                {
                    Console.WriteLine("Name: " + v.Name + " is already in use!");       // Debug.
                    return false;
                }
                Console.WriteLine("Name: " + v.Name + " is available.");                // Debug.
            }
            /* allAccountNames.FirstOrDefault */
            
            return true;
        }

        public void CreateUser(Accounts account)
        {
            Accounts test = new Accounts(0, account.Name, account.PasswordHash, "salt");            // Debug.
            // TODO: Måste hantera lösenord; hasha och salta dem.
            _repository.CreateAccount(test);
        }
    }
}

// TODO: Sanitizea input i CreateUser.
