using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FINKI_Application_ocr.Controllers
{
    [RoutePrefix("Statistic")]
    [JwtAuthentication]
    public class StatisticController : BaseController
    {
        public StatisticController(IUserService userService, IPictureService pictureService, IFileService fileService, IStatisticService statisticService)
            : base(userService, pictureService, fileService, statisticService)
        {

        }


        [HttpGet]
        [Route("getAllErrors")]
        public HttpResponseMessage GetErrors()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _statisticService.GetErrors());
            }
            catch (Exception err)
            {
                _statisticService.CreateErrorLog(new ErrorLog(err, "Get Errors (Statistic Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [HttpGet]
        [Route("getApplicationDetails")]
        public HttpResponseMessage GetApplicationDetails()
        {
            try
            {
                ApplicationDetails appDetails = _statisticService.GetApplicationDetails();
                if (appDetails != null)
                {
                    appDetails.SetTotalImagesNumber();
                    return Request.CreateResponse(HttpStatusCode.OK, appDetails);

                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception err)
            {
                _statisticService.CreateErrorLog(new ErrorLog(err, "Get Application Details (Statistic Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [HttpGet]
        [Route("getImageWithMostSuccessfullConversationRate")]
        public HttpResponseMessage GetImageWithMostSuccessfullConversationRate()
        {
            try
            {
                List<KeyValuePair<Picture, string>> listWithKeyValuePairs = new List<KeyValuePair<Picture, string>>();
                var picture = _pictureService.GetPictureWithMostSuccessfullConversationRate();

                if (picture != null)
                {
                    byte[] b = System.IO.File.ReadAllBytes(picture.ImagePath);
                    string binaryString = "data:image/png;base64," + Convert.ToBase64String(b);
                    listWithKeyValuePairs.Add(new KeyValuePair<Picture, string>(picture, binaryString));

                    return Request.CreateResponse(HttpStatusCode.OK, listWithKeyValuePairs);
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception err)
            {
                _statisticService.CreateErrorLog(new ErrorLog(err, "Get Image With Most Successfull ConversationRate (Statistic Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [HttpGet]
        [Route("getImageWithLowestSuccessfullConversationRate")]
        public HttpResponseMessage GetImageWithLowestSuccessfullConversationRate()
        {
            try
            {
                List<KeyValuePair<Picture, string>> listWithKeyValuePairs = new List<KeyValuePair<Picture, string>>();
                var picture = _pictureService.GetPictureWithLowestSuccessfullConversationRate();

                if (picture != null)
                {
                    byte[] b = System.IO.File.ReadAllBytes(picture.ImagePath);
                    string binaryString = "data:image/png;base64," + Convert.ToBase64String(b);
                    listWithKeyValuePairs.Add(new KeyValuePair<Picture, string>(picture, binaryString));

                    return Request.CreateResponse(HttpStatusCode.OK, listWithKeyValuePairs);
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception err)
            {
                _statisticService.CreateErrorLog(new ErrorLog(err, "Get Image With Lowest Successfull ConversationRate (Statistic Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [HttpGet]
        [Route("getPictureWithShortestTimeNeededForProcessing")]
        public HttpResponseMessage GetPictureWithShortestTimeNeededForProcessing()
        {
            try
            {
                List<KeyValuePair<Picture, string>> listWithKeyValuePairs = new List<KeyValuePair<Picture, string>>();
                var picture = _pictureService.GetPictureWithShortestTimeNeededForProcessing();

                if (picture != null)
                {
                    byte[] b = System.IO.File.ReadAllBytes(picture.ImagePath);
                    string binaryString = "data:image/png;base64," + Convert.ToBase64String(b);
                    listWithKeyValuePairs.Add(new KeyValuePair<Picture, string>(picture, binaryString));

                    return Request.CreateResponse(HttpStatusCode.OK, listWithKeyValuePairs);
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception err)
            {
                _statisticService.CreateErrorLog(new ErrorLog(err, "Get Picture With Shortest Time Needed For Processing (Statistic Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [HttpGet]
        [Route("getImageWithLongestTimeNeededForProcessing")]
        public HttpResponseMessage GetImageWithLongestTimeNeededForProcessing()
        {
            try
            {
                List<KeyValuePair<Picture, string>> listWithKeyValuePairs = new List<KeyValuePair<Picture, string>>();
                var picture = _pictureService.GetPictureWithLongestTimeNeededForProcessing();

                if (picture != null)
                {
                    byte[] b = System.IO.File.ReadAllBytes(picture.ImagePath);
                    string binaryString = "data:image/png;base64," + Convert.ToBase64String(b);
                    listWithKeyValuePairs.Add(new KeyValuePair<Picture, string>(picture, binaryString));

                    return Request.CreateResponse(HttpStatusCode.OK, listWithKeyValuePairs);
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception err)
            {
                _statisticService.CreateErrorLog(new ErrorLog(err, "Get Image With Longest Time Needed For Processing (Statistic Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }
    }

}
