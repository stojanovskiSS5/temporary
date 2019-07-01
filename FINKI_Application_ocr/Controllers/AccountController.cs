using FINKI_Application_ocr.Contracts.DatabaseModels;
using FINKI_Application_ocr.Contracts.Interfaces;
using FINKI_Application_ocr.Contracts.Models;
using FINKI_Application_ocr.Filters;
using FINKI_Application_ocr.PictureService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FINKI_Application_ocr.Controllers
{
    [RoutePrefix("User")]
    public class AccountController : BaseController
    {

        public AccountController(IUserService userService, IPictureService pictureService, IFileService fileService, IStatisticService statisticService)
            : base(userService, pictureService, fileService, statisticService)
        {

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("validateUser")]
        public HttpResponseMessage ValidateUser(UserDetails userDetails)
        {
            User currentUser = _userService.GetUser(userDetails);
            if (currentUser != null)
            {

                DateTime expireDate = DateTime.Now.AddDays(1);
                currentUser.Token = JwtManager.GenerateToken(currentUser, expireDate);

                currentUser.IpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(currentUser.IpAddress))
                    currentUser.IpAddress = HttpContext.Current.Request.UserHostAddress;

                if (string.IsNullOrEmpty(currentUser.IpAddress))
                {
                    currentUser.IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                //BrowserName
                currentUser.BrowserUsed = HttpContext.Current.Request.Browser.Type ?? string.Empty;

                currentUser.TokenExpirationDate = expireDate;
                JwtManager.CreateSessionLog(currentUser);

                return Request.CreateResponse(HttpStatusCode.OK, currentUser);
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }


        [JwtAuthentication]
        [HttpGet]
        [Route("getAllUsers")]
        public HttpResponseMessage GetUsers()
        {
            try
            {
                List<User> users = _userService.GetUsers();
                if (users.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, users);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception ex)
            {
                _statisticService.CreateErrorLog(new ErrorLog(ex, "Get Users (Account Controller)", ""));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("deleteUser")]
        public HttpResponseMessage DeleteUser(UserDetails userDetails)
        {
            try
            {
                if (_userService.DeleteUser(userDetails))
                    return Request.CreateResponse(HttpStatusCode.OK, "User has been successfully deleted");

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry couldn't delete the user");
            }
            catch (Exception ex)
            {
                _statisticService.CreateErrorLog(new ErrorLog(ex, "Delete User (Account Controller)", Newtonsoft.Json.JsonConvert.SerializeObject(userDetails)));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }


        [JwtAuthentication]
        [HttpPost]
        [Route("insertUser")]
        public HttpResponseMessage InsertUser(UserDetails userDetails)
        {
            try
            {
                User currentUser = _userService.AddUser(userDetails);
                if (currentUser != null)
                    return Request.CreateResponse(HttpStatusCode.OK, currentUser);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception ex)
            {
                _statisticService.CreateErrorLog(new ErrorLog(ex, "Insert User (Account Controller)", Newtonsoft.Json.JsonConvert.SerializeObject(userDetails)));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }


    }
}
