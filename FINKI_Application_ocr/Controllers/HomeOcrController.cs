using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FINKI_Application_ocr.Controllers
{


    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Home")]
    public class HomeOcrController : BaseController
    {

        public HomeOcrController(IUserService userService, IPictureService pictureService, IFileService fileService, IStatisticService statisticService)
            : base(userService, pictureService, fileService, statisticService)
        {

        }


        [HttpPost]
        [Route("ExtractText")]
        public HttpResponseMessage SaveImageAndPrepareForExtraction()
        {
            try
            {
                var context = HttpContext.Current;
                File file = _fileService.SaveFile(context, _pictureService);
                if (file is PdfDocument)
                    return Request.CreateResponse(HttpStatusCode.OK, file);

                return Request.CreateResponse(HttpStatusCode.OK, file);
            }
            catch (Exception err)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, err.ToString());
            }
        }

        [HttpPost]
        [Route("CompareFiles")]
        public HttpResponseMessage SaveFilesAndPrepareForCompare()
        {
            try
            {
                var context = HttpContext.Current;
                var files = _fileService.SaveFiles(context, _pictureService);
                if (files != null)
                    return Request.CreateResponse(HttpStatusCode.OK, files);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err.ToString());
            }
        }

        [HttpGet]
        [Route("GetImage/{id}")]
        public HttpResponseMessage GetImage(int id)
        {
            try
            {
                List<KeyValuePair<Picture, string>> listWithKeyValuePairs = new List<KeyValuePair<Picture, string>>();
                var picture = _pictureService.GetPictureById(id);
                byte[] b = System.IO.File.ReadAllBytes(picture.ImagePath);
                string binaryString = "data:image/png;base64," + Convert.ToBase64String(b);
                listWithKeyValuePairs.Add(new KeyValuePair<Picture, string>(picture, binaryString));

                return Request.CreateResponse(HttpStatusCode.OK, listWithKeyValuePairs);

            }
            catch (Exception err)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, err.ToString());
            }
        }

        [HttpGet]
        [Route("GetPdfImages/{id}")]
        public HttpResponseMessage GetPdfImages(int id)
        {
            try
            {
                List<KeyValuePair<Picture, string>> listWithKeyValuePairs = new List<KeyValuePair<Picture, string>>();
                var pictures = _pictureService.
                    GetPdfPicturesById(id);
                if (pictures != null && pictures.Count > 0)
                {
                    foreach (Picture picture in pictures)
                    {
                        byte[] b = System.IO.File.ReadAllBytes(_pictureService.GetPictureById(picture.Id).ImagePath);
                        string binaryString = "data:image/png;base64," + Convert.ToBase64String(b);

                        listWithKeyValuePairs.Add(new KeyValuePair<Picture, string>(picture, binaryString));

                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, listWithKeyValuePairs);
            }
            catch (Exception err)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, err.ToString());
            }
        }


        [HttpGet]
        [Route("getIp")]
        public HttpResponseMessage GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, addresses[0]);
                }
            }

            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            string ip = addr[1].ToString();

            return Request.CreateResponse(HttpStatusCode.OK, context.Request.ServerVariables["REMOTE_ADDR"]);

        }


    }
}
