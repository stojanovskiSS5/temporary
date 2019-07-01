using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Datamanipulation.Constants;
using FINKI_Application_ocr.Mapping;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using File = FINKI_Application_ocr.Contracts.Models.File;
using Image = FINKI_Application_ocr.Contracts.Models.Image;
using PdfDocument = FINKI_Application_ocr.Contracts.Models.PdfDocument;

namespace FINKI_Application_ocr.Manager
{
    public class FileService : IFileService
    {
        public File SaveFile(HttpContext context, IPictureService _pictureService)
        {
            File file = new File();
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection  
                var httpPostedFile = HttpContext.Current.Request.Files[0];
                if (httpPostedFile != null)
                {
                    var name = ModifyTheNameOfThePdf(httpPostedFile);

                    if (httpPostedFile.ContentType.Equals("application/pdf"))
                    {
                        file = _pictureService.ProcessPicture(GeneratePdfDocument(httpPostedFile));
                    }

                    if (httpPostedFile.ContentType.Equals("image/jpeg") || httpPostedFile.ContentType.Equals("image/png"))
                    {
                        Image img = new Image();
                        img.StartProcessingTime = DateTime.Now;
                        img.Name = name;
                        img.Path = Path.Combine(Paths.ImagesPath, $@"{img.StartProcessingTime.ToString("yyyyMMdd_hhmmss")}_{img.Name}");
                        httpPostedFile.SaveAs(img.Path);
                        file = _pictureService.ProcessPicture(img);
                    }

                }
            }

            return file;
        }

        public List<File> SaveFiles(HttpContext context, IPictureService _pictureService)
        {
            List<File> listWithFiles = new List<File>();
            File firstFile = new File();
            File fileThatWillBeCompared = new File();

            var firstPostedFile = HttpContext.Current.Request.Files[0];
            var secondPostedFile = HttpContext.Current.Request.Files[1];
            if (firstPostedFile != null && secondPostedFile != null)
            {
                var firstPdf = GeneratePdfDocument(firstPostedFile);
                Thread.Sleep(1000);
                var secondPdf = GeneratePdfDocument(secondPostedFile);
                firstPdf.PdfIdForComparing = secondPdf.Id;
                secondPdf.PdfIdForComparing = firstPdf.Id;
                if (firstPdf != null && secondPdf != null)
                {
                    if (firstPdf.PagesNumber == secondPdf.PagesNumber)
                    {
                        firstFile = _pictureService.ProcessPicture(firstPdf);
                        fileThatWillBeCompared = _pictureService.ProcessPicture(secondPdf);
                    }
                    listWithFiles.Add(firstFile);
                    listWithFiles.Add(fileThatWillBeCompared);
                }
            }

            return listWithFiles;
        }

        public string ModifyTheNameOfThePdf(HttpPostedFile file)
        {
            int length = file.ContentLength;
            var fileData = new byte[length];
            file.InputStream.Read(fileData, 0, length);
            var name = file.FileName;
            if (name.Contains("\\"))
            {
                var splitted = file.FileName.Split('\\');
                name = splitted[splitted.Length - 1];
            }

            return name;
        }

        public PdfDocument GeneratePdfDocument(HttpPostedFile file)
        {
            if (file.ContentType.Equals("application/pdf"))
            {
                PdfDocument pdf = new PdfDocument();
                pdf.StartProcessingTime = DateTime.Now;
                pdf.Name = ModifyTheNameOfThePdf(file);
                pdf.Path = Path.Combine(Paths.PdfFilesPath, $@"{pdf.StartProcessingTime.ToString("yyyyMMdd_hhmmss")}_{pdf.Name}");
                file.SaveAs(pdf.Path);
                pdf.PagesNumber = GetPdfPageNumber(pdf);
                pdf.PdfIdForComparing = -1;
                pdf.ShouldCompare = false;
                pdf.Id = SavePdfToDb(pdf);
                return pdf;
            }

            return null;
        }
        public int GetPdfPageNumber(PdfDocument pdfDocument)
        {
            PdfReader pdfReader = new PdfReader(pdfDocument.Path);
            return pdfReader.NumberOfPages;
        }

        public int SavePdfToDb(PdfDocument pdf)
        {
            return FileManager.SaveFile(pdf);
        }

    }
}
