using System;

namespace FINKI_Application_ocr.Contracts.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime StartProcessingTime { get; set; }
        public string ProcessingTime { get; set; }
        public DateTime? EndProcessingTime { get; set; }

    }
}
