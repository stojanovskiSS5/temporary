using Newtonsoft.Json;
using System;

namespace FINKI_Application_ocr.Contracts.Models
{
    public class ErrorLog
    {
        public string Parametars { get; set; }
        public DateTime? ExceptionDate { get; set; }
        public string ExceptionReason { get; set; }

        public ErrorLog(Exception exception, params object[] parametars)
        {
            ExceptionDate = DateTime.Now;
            ExceptionReason = exception.ToString();
            Parametars = JsonConvert.SerializeObject(parametars);
        }
    }
}
