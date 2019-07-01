using Dapper;
using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Mapping.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FINKI_Application_ocr.Mapping
{
    public static class PictureManager
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationOcrConnection"].ConnectionString;
        public static int SavePicture(Image image)
        {
            int i = 0;
            var retVal = -1;
            using (var connection = new SqlConnection(connectionString))
            {
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@FileName", image.Name, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@TesseractImageText", image.TessarectImageText, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@ImageText", image.ImageText, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@InvalidWords", String.Join(" ", image.InvalidWords.ToArray()), DbType.String, direction: ParameterDirection.Input);
                _params.Add("@InvalidWordsCount", image.CountInvalidWords, DbType.Int64, direction: ParameterDirection.Input);
                _params.Add("@ImagePath", image.Path, DbType.String, direction: ParameterDirection.Input);
                _params.Add("@StartProcessingTime", image.StartProcessingTime, DbType.DateTime, direction: ParameterDirection.Input);
                _params.Add("@EndProcessingTime", image.EndProcessingTime, DbType.DateTime, direction: ParameterDirection.Input);
                _params.Add("@ProcessingTime", image.ProcessingTime, DbType.String, direction: ParameterDirection.Input);
                if (image.PdfDocumentId != -1)
                    _params.Add("@PdfFileId", image.PdfDocumentId, DbType.Int64, direction: ParameterDirection.Input);

                _params.Add("@Id", DbType.Int32, direction: ParameterDirection.Output);

                connection.Open();
                i = connection.Execute(StoredProcedures.SavePicture, param: _params, commandType: CommandType.StoredProcedure);

                retVal = _params.Get<int>("Id");
            }
            return retVal;

        }

        public static Picture GetPictureById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Picture>(StoredProcedures.GetPictureById,
                                                 param: new { id },
                                                 commandType: CommandType.StoredProcedure)
                                                 .FirstOrDefault();
            }
        }

        public static List<Picture> GetPdfPicturesById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Picture>(StoredProcedures.GetPdfPicturesById,
                                                 param: new { id },
                                                 commandType: CommandType.StoredProcedure)
                                                 .ToList();
            }
        }

        public static Picture GetPictureWithMostSuccessfullConversationRate()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Picture>(StoredProcedures.GetImageWithMostSuccessfullConversationRate,
                                                 commandType: CommandType.StoredProcedure)
                                                 .FirstOrDefault();
            }
        }
        public static Picture GetPictureWithLowestSuccessfullConversationRate()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Picture>(StoredProcedures.GetImageWithLowestSuccessfullConversationRate,
                                                 commandType: CommandType.StoredProcedure)
                                                 .FirstOrDefault();
            }
        }
        public static Picture GetPictureWithLongestTimeNeededForProcessing()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Picture>(StoredProcedures.GetImageWithLongestTimeNeededForProcessing,
                                                 commandType: CommandType.StoredProcedure)
                                                 .FirstOrDefault();
            }
        }
        public static Picture GetPictureWithShortestTimeNeededForProcessing()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Picture>(StoredProcedures.GetImageWithShortestTimeNeededForProcessing,
                                                 commandType: CommandType.StoredProcedure)
                                                 .FirstOrDefault();
            }
        }
    }
}
