namespace FINKI_Application_ocr.Contracts.DatabaseModels
{
    public class ApplicationDetails
    {
        public int TotalPdfProcessed { get; set; }
        public int TotalImagesFromPdf { get; set; }
        public int TotalImagesNumber { get; set; }
        public int TotalFilesProcessed { get; set; }
        public int MaxLetters { get; set; }
        public int MinLetters { get; set; }
        public string MinProcessedTime { get; set; }
        public string MaxProcessedTime { get; set; }



        public void SetTotalImagesNumber()
        {
            TotalImagesNumber = TotalFilesProcessed - TotalImagesFromPdf;
        }

    }
}
