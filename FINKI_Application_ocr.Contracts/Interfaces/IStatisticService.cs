using FINKI_Application_ocr.Contracts.DatabaseModels;
using System.Collections.Generic;

namespace FINKI_Application_ocr.Contracts.Interfaces
{
    public interface IStatisticService
    {
        void CreateErrorLog(ErrorLog error);
        List<ErrorLog> GetErrors();

        ApplicationDetails GetApplicationDetails();
        

    }
}
