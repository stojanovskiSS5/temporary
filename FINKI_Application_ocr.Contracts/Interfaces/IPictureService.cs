using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Models;
using System.Collections.Generic;

namespace FINKI_Application_ocr.Contracts.Interfaces
{
    public interface IPictureService
    {
        File ProcessPicture(File file);
       // int SavePicture(string path, int pdfId);
        Picture GetPictureById(int id);
        List<Picture> GetPdfPicturesById(int id);
        Picture GetPictureWithMostSuccessfullConversationRate();
        Picture GetPictureWithLowestSuccessfullConversationRate();
        Picture GetPictureWithLongestTimeNeededForProcessing();
        Picture GetPictureWithShortestTimeNeededForProcessing();


    }
}
