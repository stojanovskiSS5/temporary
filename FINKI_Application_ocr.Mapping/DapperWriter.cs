using Dapper;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Mapping.Constants;
using System.Data;
using System.Data.SqlClient;

namespace FINKI_Application_ocr.Mapping
{



    public static class DapperWriter
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationOcrConnection"].ConnectionString;

        public static string SavePicture(Image image)
        {
            int i = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                i = connection.Execute(StoredProcedures.SavePicture, new { FileName = image.Name, ImagePath = image.Path }, commandType: CommandType.StoredProcedure);
            }
            if (i > 0) { return "Simon"; }
            else
                return "DADA";
        }

        public static bool InsertSessionLog(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(StoredProcedures.CreateSessionLog, param: new
                {
                    userOPDID = user.StaffNumber,
                    jwtToken = user.Token,
                    expiryDate = user.TokenExpirationDate,
                    browser = user.BrowserUsed,
                    ipAddress = user.IpAddress
                },
               commandType: CommandType.StoredProcedure) > 0;
            }
        }


    }
}
