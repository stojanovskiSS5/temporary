using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Mapping;
using System.Collections.Generic;

namespace FINKI_Application_ocr.Manager
{
    public class StatisticService : IStatisticService
    {
        public void CreateErrorLog(ErrorLog error)
        {
            LogsManager.InsertErrorLog(error);
        }

        public List<ErrorLog> GetErrors()
        {
            return LogsManager.GetErrors();
        }

        public ApplicationDetails GetApplicationDetails()
        {
            return LogsManager.GetApplicationDetails();
        }
    }
}
