using FINKI_Application_ocr.Contracts.Models;
using System.Collections.Generic;
using System.Web;

namespace FINKI_Application_ocr.Contracts.Interfaces
{
    public interface IFileService
    {
        File SaveFile(HttpContext context, IPictureService _pictureService);

        List<File> SaveFiles(HttpContext context, IPictureService _pictureService);
        
    }
}
