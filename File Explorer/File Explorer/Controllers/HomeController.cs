//using FileExplorer.Models;
using BusinessAccessLayer.Intrefaces;
using File_Explorer.Controllers;
using File_Explorer.Models;
using Microsoft.AspNetCore.Mvc;

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
        private readonly MyResponse myResponse= new MyResponse();

        public HomeController(ILogger<HomeController> logger, ICrud crud, IDriveInfo driveInfo, IFileService fileService): base(crud, driveInfo, fileService)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            throw new Exception("my Exception for fun");
            _logger.LogInformation("Home Index Called");
            return View();
        } 
        public IActionResult Error()
        {
            var errorViewModel = TempData["ErrorViewModel"] as ErrorViewModel;

            return View(errorViewModel);
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
                    myResponse.Message = "Task Executed Successfully!";
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
                    myResponse.Message = "Task Executed Successfully!";
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
                    myResponse.Message = "Files getting Successfully!";
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
                    myResponse.Message = "Task Executed Successfully!";
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
                    myResponse.Message = "Task Executed Successfully!";
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
                    myResponse.Message = "Task Executed Successfully!";
                    myResponse.Ok = true;
                }
                else if (result == "NotDefine")
                {
                    myResponse.Message = "Undefined file Type";
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
                    myResponse.Message = "Directory deleted successfully!";
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

        // Method names should be pascal cased and not camelcased
        // Violation of SRP (Single responsibility principle) .Move this to file service class which will implement
        //IFileService interface. Then inject IfileService as a dependency into controller
        //variable name should be camel case and method/property/class name should be pascal

        //ToDo: Prepare application for multilingual support. Move all messages to resource files

    }
}