using System.IO;
using System.Web;

namespace FINKI_Application_ocr.Datamanipulation.Constants
{
    public static class Paths
    {
        public static readonly string ProjectDirectory = Path.Combine(HttpRuntime.AppDomainAppPath, @"..\WebApi");
        public static readonly string PdfFilesFolder = "PdfFiles";
        public static readonly string PdfImagesFolder = "PdfImages";
        public static readonly string ImagesFolder = "Images";
        public static readonly string TesseractDataFolder = "tessdata";

        public static readonly string PdfFilesPath = Path.Combine(ProjectDirectory, PdfFilesFolder);
        public static readonly string PdfImagesPath = Path.Combine(ProjectDirectory, PdfImagesFolder);
        public static readonly string ImagesPath = Path.Combine(ProjectDirectory, ImagesFolder);
        public static readonly string TesseractDataFolderPath = Path.Combine(ProjectDirectory, TesseractDataFolder);


    }
}
