using Dapper;
using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Mapping.Constants;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace FINKI_Application_ocr.Mapping
{
    public static class UsersDataManager
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationOcrConnection"].ConnectionString;
        public static User GetUserData(UserDetails userDetails)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<User>(StoredProcedures.GetUser,
                    param: new { @Firstname = userDetails.FirstName, @Lastname = userDetails.LastName, @Email = userDetails.EmailAddress },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }


        public static List<User> GetUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<User>(StoredProcedures.GetUsers,
                    param: new { },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }
        }



        public static User AddUser(UserDetails userDetails)
        {
            int i = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<User>(StoredProcedures.InsertUser,
                    param: new { @Firstname = userDetails.FirstName, @Lastname = userDetails.LastName, @Email = userDetails.EmailAddress },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }

        public static bool DeleteUser(UserDetails user)
        {
            int i = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                i = connection.Execute(StoredProcedures.DeleteUser, param: new { user.Id, @Email = user.EmailAddress }, commandType: CommandType.StoredProcedure);
            }
            if (i > 0) { return true; }
            else
                return false;
        }

    }
}
