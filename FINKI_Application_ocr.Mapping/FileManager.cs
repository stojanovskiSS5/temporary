using Dapper;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Mapping.Constants;
using System.Data;
using System.Data.SqlClient;

namespace FINKI_Application_ocr.Mapping
{
    public static class FileManager
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationOcrConnection"].ConnectionString;
        public static int SaveFile(PdfDocument pdf)
        {
            int i = 0;
            var retVal = -1;
            using (var connection = new SqlConnection(connectionString))
            {
                DynamicParameters _params = new DynamicParameters();

                _params.Add("@PdfName", pdf.Name, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@PdfPath", pdf.Path, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@StartProcessingTime", pdf.StartProcessingTime, DbType.DateTime, direction: ParameterDirection.Input);
                _params.Add("@EndProcessingTime", pdf.EndProcessingTime, DbType.DateTime, direction: ParameterDirection.Input);
                _params.Add("@ProcessingTime", pdf.ProcessingTime, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@PagesNumber", pdf.PagesNumber, DbType.Int64, direction: ParameterDirection.Input);
                _params.Add("@PdfIdForComparing", pdf.PdfIdForComparing, DbType.Int64, direction: ParameterDirection.Input);
                _params.Add("@ShouldCompare", pdf.ShouldCompare, DbType.Boolean, direction: ParameterDirection.Input);
                _params.Add("@Id", DbType.Int32, direction: ParameterDirection.Output);


                connection.Open();
                i = connection.Execute(StoredProcedures.SaveFile, param: _params, commandType: CommandType.StoredProcedure);

                retVal = _params.Get<int>("Id");
            }
            return retVal;

        }

        public static void UpdateFile(PdfDocument pdf)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                DynamicParameters _params = new DynamicParameters();

                _params.Add("@Id", pdf.Id, DbType.Int32, direction: ParameterDirection.Input);
                _params.Add("@EndProcessingTime", pdf.EndProcessingTime, DbType.DateTime, direction: ParameterDirection.Input);
                _params.Add("@PdfIdForComparing", pdf.PdfIdForComparing, DbType.Int64, direction: ParameterDirection.Input);
                _params.Add("@ProcessingTime", pdf.ProcessingTime, DbType.String, direction: ParameterDirection.Input);

                connection.Open();
                connection.Execute(StoredProcedures.UpdateFile, param: _params, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
