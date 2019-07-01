using System.Collections.Generic;

namespace FINKI_Application_ocr.Contracts.Models
{
    public class Image : File
    {
        public string ImageText { get; set; }
        public string TessarectImageText { get; set; }

        public List<string> InvalidWords = new List<string>();
        public int CountInvalidWords { get; set; }

        public int PdfDocumentId { get; set; }
    }
}
