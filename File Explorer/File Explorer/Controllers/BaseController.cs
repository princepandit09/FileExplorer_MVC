using BusinessAccessLayer.Intrefaces;
using Microsoft.AspNetCore.Mvc;

namespace File_Explorer.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly ICrud _crudService;
        public readonly IDriveInfo _driveInfoServices;
        public readonly IFileService _fileService;

        public BaseController(ICrud crud, IDriveInfo driveInfo, IFileService fileService)
        {
            _crudService = crud;
            _driveInfoServices = driveInfo;
            _fileService = fileService;
        }

    }

}
