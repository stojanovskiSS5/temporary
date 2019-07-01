using System.Collections.Generic;

namespace FINKI_Application_ocr.Contracts.Models
{
    public class PdfDocument : File
    {
        public List<Image> Images = new List<Image>();
        public int PagesNumber { get; set; }
        public bool ShouldCompare { get; set; }
        public int PdfIdForComparing { get; set; }

    }
}
