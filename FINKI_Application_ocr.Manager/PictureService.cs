using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Datamanipulation;
using FINKI_Application_ocr.Datamanipulation.Constants;
using FINKI_Application_ocr.Mapping;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using File = FINKI_Application_ocr.Contracts.Models.File;
using Image = FINKI_Application_ocr.Contracts.Models.Image;

namespace FINKI_Application_ocr.Manager
{
    public class PictureService : IPictureService
    {
        public File ProcessPicture(File file)
        {

            if (file.GetType() == typeof(Image) && file is Image)
            {
                Image image = (Image)file;
                image.Id = SavePicture(image.Path, -1);
            }

            if (file.GetType() == typeof(PdfDocument) && file is PdfDocument)
            {
                PdfDocument pdfDocument = (PdfDocument)file;

                Thread thread = new Thread(() => convertPdfToImages(pdfDocument));
                thread.Start();

                Thread processImagesThread = new Thread(() => ProcessImages(pdfDocument, thread));
                processImagesThread.Start();
            }
            return file;
        }

        public static void ProcessImages(PdfDocument pdfDocument, Thread thread)
        {
            thread.Join();
            string[] files = Directory.GetFiles(Paths.PdfImagesPath, $@"{pdfDocument.StartProcessingTime.ToString("yyyyMMdd_hhmmss")}_{pdfDocument.Name}*.png", SearchOption.AllDirectories);

            foreach (var path in files)
            {
                SavePicture(path, pdfDocument.Id);
            }
            pdfDocument.EndProcessingTime = DateTime.Now;
            pdfDocument.ProcessingTime = pdfDocument.EndProcessingTime != null ? (pdfDocument.EndProcessingTime.Value.TimeOfDay - pdfDocument.StartProcessingTime.TimeOfDay).ToString() : string.Empty;

            FileManager.UpdateFile(pdfDocument);
        }

        public static void convertPdfToImages(PdfDocument pdfDocument)
        {
            ConvertPdfToImages.PdfToPng(pdfDocument.Path, Paths.PdfImagesPath, $@"{pdfDocument.StartProcessingTime.ToString("yyyyMMdd_hhmmss")}_{pdfDocument.Name}");
        }

        public static int SavePicture(string path, int pdfId)
        {
            Image image = new Image();
            Bitmap bitmap = new Bitmap(path);
            image.StartProcessingTime = DateTime.Now;
            ImagePreprocessing imagePreprocessing = new ImagePreprocessing(bitmap);
            ExtractText ex = new ExtractText(imagePreprocessing);
            ex.ExtractTextFromImage();
            var splitPath = path.Split('\\');
            image.Name = splitPath[splitPath.Length - 1];
            image.TessarectImageText = ex.TextFromImageTesseract;

            ex.TextForImagePreparationFunctions();
            ex.PreparedTextForProcessingPreparationFunctions();
            ex.GetBetterText(ex.PreparedTextForProcessing);
            ex.TextFromImageSpellSuggestionCorrection();
            ex.WordsCheck();

            image.ImageText = ex.TextFromImage;
            image.Path = path;
            image.InvalidWords = ex.WrongWords;
            image.CountInvalidWords = ex.WrongWords.Count;
            image.EndProcessingTime = DateTime.Now;
            image.PdfDocumentId = pdfId;
            image.ProcessingTime = image.EndProcessingTime != null ? (image.EndProcessingTime.Value.TimeOfDay - image.StartProcessingTime.TimeOfDay).ToString() : string.Empty;

            //SaveToDb
            return PictureManager.SavePicture(image);
        }


        public Picture GetPictureById(int id)
        {
            return PictureManager.GetPictureById(id);
        }

        public List<Picture> GetPdfPicturesById(int id)
        {
            return PictureManager.GetPdfPicturesById(id);
        }

        public Picture GetPictureWithMostSuccessfullConversationRate()
        {
            return PictureManager.GetPictureWithMostSuccessfullConversationRate();
        }
        public Picture GetPictureWithLowestSuccessfullConversationRate()
        {
            return PictureManager.GetPictureWithLowestSuccessfullConversationRate();

        }
        public Picture GetPictureWithLongestTimeNeededForProcessing()
        {
            return PictureManager.GetPictureWithLongestTimeNeededForProcessing();

        }
        public Picture GetPictureWithShortestTimeNeededForProcessing()
        {
            return PictureManager.GetPictureWithShortestTimeNeededForProcessing();

        }
    }
}
