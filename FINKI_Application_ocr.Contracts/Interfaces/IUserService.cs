using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Models;
using System.Collections.Generic;

namespace FINKI_Application_ocr.Contracts.Interfaces
{
    public interface IUserService
    {
        User GetUser(UserDetails userDetails);

        List<User> GetUsers();

        User AddUser(UserDetails user);

        bool DeleteUser(UserDetails user);

    }
}
