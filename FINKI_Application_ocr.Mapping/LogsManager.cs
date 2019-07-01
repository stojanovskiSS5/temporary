using Dapper;
using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Mapping.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FINKI_Application_ocr.Mapping
{
    public static class LogsManager
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationOcrConnection"].ConnectionString;

        public static void InsertErrorLog(ErrorLog errorLog)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute(StoredProcedures.CreateErrorLog, param: new
                    {
                        errorLog.ExceptionReason,
                        errorLog.ExceptionDate,
                        errorLog.Method,
                        errorLog.Parametars,
                    },
                   commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception) { }
        }

        public static bool InserSessionLog(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(StoredProcedures.CreateSessionLog, param: new
                {
                    userID = user.UserID,
                    jwtToken = user.Token,
                    dateInserted = DateTime.Now,
                    expiryDate = user.TokenExpirationDate,
                    browser = user.BrowserUsed,
                    ipAddress = user.IpAddress
                },
               commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public static List<ErrorLog> GetErrors()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<ErrorLog>(StoredProcedures.GetErrors, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static ApplicationDetails GetApplicationDetails()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<ApplicationDetails>(StoredProcedures.GetApplicationDetails, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}
