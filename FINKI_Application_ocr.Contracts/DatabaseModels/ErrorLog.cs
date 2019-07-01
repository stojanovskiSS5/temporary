using System;

namespace FINKI_Application_ocr.Contracts.DatabaseModels
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Parametars { get; set; }
        public DateTime? ExceptionDate { get; set; }
        public string ExceptionReason { get; set; }
        public string Method { get; set; }

        public ErrorLog(Exception exception, string method, string parametars)
        {

            ExceptionDate = DateTime.Now;
            ExceptionReason = exception.Message;
            Method = method;
            Parametars = parametars;
        }
        public ErrorLog()
        {

        }
    }
}
