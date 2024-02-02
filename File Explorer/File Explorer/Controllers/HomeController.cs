//using FileExplorer.Models;
using BusinessAccessLayer.Intrefaces;
using File_Explorer.Controllers;
using File_Explorer.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace FileExplorer.Controllers
{

    //ToDo: Implement bundling and minification
    // If using images use image sprites
    // Log to text files
    //ToDo: Implement filters to manage exception globally
    //Never inherit or use framework class/object directly. Always create an abstract base class inherting from controller class and then inherit from that base class
    //This is called Basecontroller pattern
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IViewLocalizer _localizer;
        private readonly MyResponse myResponse= new MyResponse();

        private Ilanguage Language;
        private readonly ActionContext _actionContext = new ActionContext();

        public HomeController(ILogger<HomeController> logger,Ilanguage language, IViewLocalizer localizer, ICrud crud, IDriveInfo driveInfo, IFileService fileService): base(crud, driveInfo, fileService)
        {
            _logger = logger;
            Language = language;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            //throw new Exception("my Exception for fun");
            _logger.LogInformation(Language.GetKey("Home Index Called").Value);
            //var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            var currentCulture = CultureInfo.CurrentCulture.Name;

            return View();
        } 
        public IActionResult Error()
        {
            ViewData["message"] = Language.GetKey("An unexpected error occurred. Please try again later after some time :)").Value;
            return View();
        }

        public IActionResult GetInitialDriveAndDirectories()
        {
            var driveInfo = _driveInfoServices.GetDriveAndDirectories();
            return Json(driveInfo);
        }
        public IActionResult GetTnternalDirectoriesByName(string path,string searchPattern)
        {
            try
            {
                var result = _driveInfoServices.GetTnternalDirectoriesByName(path, searchPattern);
                if (String.IsNullOrEmpty(result.Item1))
                {
                    myResponse.Message = Language.GetKey("Task Executed Successfully!").Value;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                else
                {
                    myResponse.Message = result.Item1;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                return Json(myResponse);
            }
            catch (Exception ex)
            {
                myResponse.Message = ex.Message;
                myResponse.Ok = false;
                myResponse.Data = null;
                return Json(myResponse);
            }
        }
        public IActionResult GetInternalDriveAndDirectories(string path)
        {
            try
            {
                var result = _driveInfoServices.GetInternalDriveAndDirectoriesByPath(path);
                if (String.IsNullOrEmpty(result.Item1))
                {
                    myResponse.Message = Language.GetKey("Task Executed Successfully!").Value;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                else
                {
                    myResponse.Message = result.Item1;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                
            }
            catch (Exception ex)
            {
                myResponse.Message = ex.Message;
                myResponse.Ok = false;
                myResponse.Data = null;
                
            }
            return Json(myResponse);
        }
        public IActionResult GetInternalFiles(string path)
        {
            try
            {
                var result = _fileService.GetInternalFiles(path);
                if (String.IsNullOrEmpty(result.Item1))
                {
                    myResponse.Message =Language.GetKey("Files getting Successfully!").Value;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                else
                {
                    myResponse.Message = result.Item1;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                return Json(myResponse);
            }
            catch (Exception ex)
            {
                myResponse.Message = ex.Message;
                myResponse.Ok = false;
                myResponse.Data = null;
                return Json(myResponse);
            }
        }
        public IActionResult GetInternalFilesByName(string path, string searchPattern)
        {
            try
            {
                var result = _fileService.GetInternalFiles(path);
                if (String.IsNullOrEmpty(result.Item1))
                {
                    myResponse.Message =Language.GetKey("Task Executed Successfully!").Value;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                else
                {
                    myResponse.Message = result.Item1;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                return Json(myResponse);
            }
            catch (Exception ex)
            {
                myResponse.Message = ex.Message;
                myResponse.Ok = false;
                myResponse.Data = null;
                return Json(myResponse);
            }

        }
        public IActionResult GetFileContent(string path)
        {
            try
            {
                var result = _fileService.GetFileContent(path);
                if (String.IsNullOrEmpty(result.Item1))
                {
                    myResponse.Message =Language.GetKey("Task Executed Successfully!").Value;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                else
                {
                    myResponse.Message = result.Item1;
                    myResponse.Ok = true;
                    myResponse.Data = result.Item2;
                }
                return Json(myResponse);
            }
            catch (Exception ex)
            {
                myResponse.Message = ex.Message;
                myResponse.Ok = false;
                myResponse.Data = null;
                return Json(myResponse);
            }
        }
        public IActionResult SaveData(string newPath, string oldPath, bool isDelete)
        {
            try
            {
                string result = _crudService.SaveData(newPath, oldPath, isDelete);
                if (String.IsNullOrEmpty(result))
                {
                    myResponse.Message =Language.GetKey("Task Executed Successfully!").Value;
                    myResponse.Ok = true;
                }
                else if (result == "NotDefine")
                {
                    myResponse.Message =Language.GetKey("Undefined file Type").Value;
                    myResponse.Ok = true;
                }
                else
                {
                    myResponse.Message = result;
                }
                return Json(myResponse);

            }
            catch (Exception ex)
            {
                myResponse.Ok = false;
                myResponse.Message = ex.Message;
                return Json(myResponse);
            }
        }
        public IActionResult DeleteData(string oldPath)
        {
            try
            {
                string result = _crudService.DeleteData(oldPath);
                if (String.IsNullOrEmpty(result))
                {
                    myResponse.Message =Language.GetKey("Directory deleted successfully!").Value;
                    myResponse.Ok = true;
                }
                else
                {
                    myResponse.Message = "Something went Wrong";
                    myResponse.Ok = true;
                }
                return Json(myResponse);
            }
            catch (Exception ex)
            {
                myResponse.Ok = false;
                myResponse.Message = ex.Message;
                return Json(myResponse);
            }
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            //return Redirect(Request.Headers["Referer"].ToString());
            //Response.Cookies.Append("culture", culture);

            return RedirectToAction("Index");
        }

        //ToDo: Prepare application for multilingual support. Move all messages to resource files

        

    }
}