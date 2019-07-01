using FINKI_Application_ocr.Contracts.Interfaces;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FINKI_Application_ocr.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        protected readonly IUserService _userService;
        protected readonly IPictureService _pictureService;
        protected readonly IFileService _fileService;
        protected readonly IStatisticService _statisticService;

        public BaseController(IUserService userService, IPictureService pictureService, IFileService fileService, IStatisticService statisticService)
        {
            this._userService = userService;
            this._pictureService = pictureService;
            this._fileService = fileService;
            this._statisticService = statisticService;
        }
    }
}
