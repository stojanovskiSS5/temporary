using System;

namespace FINKI_Application_ocr.Contracts.DatabaseModels
{
    public class Picture
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string TessarectText { get; set; }
        public DateTime StartProcessingTime { get; set; }
        public DateTime EndProcessingTime { get; set; }
        public string TotalProcessingTime { get; set; }
        public string InvalidWords { get; set; }
        public int InvalidWordsCount { get; set; }
        public int? PdfFileId { get; set; }

    }
}
