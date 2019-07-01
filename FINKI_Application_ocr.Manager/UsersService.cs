using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Mapping;
using System.Collections.Generic;

namespace FINKI_Application_ocr.Manager
{
    public class UsersService : IUserService
    {
        public User AddUser(UserDetails user)
        {
            return UsersDataManager.AddUser(user);
        }

        public bool DeleteUser(UserDetails user)
        {
            return UsersDataManager.DeleteUser(user);
        }

        public User GetUser(UserDetails userDetails)
        {
            return UsersDataManager.GetUserData(userDetails);
        }

        public List<User> GetUsers()
        {
            return UsersDataManager.GetUsers();
        }


    }
}
