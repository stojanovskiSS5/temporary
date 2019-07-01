using Ghostscript.NET;
using System;
using System.IO;

namespace FINKI_Application_ocr.Datamanipulation
{
    public static class ConvertPdfToImages
    {
        public static bool PdfToPng(string inputFile, string outputPath, string fileDeterminator)
        {
            try
            {
                GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.PngGray);
                dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                dev.CustomSwitches.Add("-r300");
                dev.InputFiles.Add(inputFile);
                dev.CustomSwitches.Add("-dDOINTERPOLATE"); // custom parameter
                dev.OutputPath = Path.Combine(outputPath, String.Format("{0}Image%03d.png", fileDeterminator));
                dev.Process();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
