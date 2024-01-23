//using FileExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetInitialDriveAndDirectories()
        {
            //Dictionary<string, Tuple<int, string[]>> driveInfo = GetDriveAndDirectories();
            //Dictionary<string, List <string>> driveInfo = GetDriveAndDirectories();
            Dictionary<string, List<Tuple<int, string>>> driveInfo = GetDriveAndDirectories();
            return Json(driveInfo);
        }

        private Dictionary<string, List<Tuple<int, string>>> GetDriveAndDirectories()
        {
            // Dictionary<string, List<string>> driveInfo = new Dictionary<string, List<string>>();
            Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();


            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.IsReady)
                {
                    string driveLetter = drive.Name;
                    string[] directories = GetDirectories(driveLetter);

                    List<Tuple<int, string>> tuplelst = new List<Tuple<int, string>>();

                    foreach (string directory in directories)
                    {
                        string[] subdirectories = GetDirectories(directory);
                            
                        tuplelst.Add(new Tuple<int, string>(subdirectories.Length, directory));

                    }

                    driveInfo[driveLetter] = new List<Tuple<int, string>>(tuplelst);


                    //driveInfo[driveLetter] = new List<string>(directories);
                }
            }

            return driveInfo;
        }

        private string[] GetDirectories(string driveLetter)
        {
            try
            {
                return Directory.GetDirectories(driveLetter);
            }

            catch
            {
                return new string[0];
            }
        }

        [HttpGet]
        public IActionResult GetInternalFilesByName(string path,string searchPattern)
        {
            try
            {
                Dictionary<string, List<Tuple<string, string>>> FilesInfo = new Dictionary<string, List<Tuple<string, string>>>();

                string[] files = Directory.GetFiles(path); ;
                List<Tuple<string, string>> tuplelst = new List<Tuple<string, string>>();

                foreach (string file in files)
                {
                    string filename= Path.GetFileName(file);
                    if(filename.ToLower().Contains(searchPattern.ToLower()))
                    {

                    tuplelst.Add(new Tuple<string, string>(Path.GetExtension(file), file));

                    }
           

                }
                FilesInfo[path] = new List<Tuple<string, string>>(tuplelst);
                return Json(FilesInfo);
            }
            catch (Exception)
            {
                Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

                driveInfo[path] = new List<Tuple<int, string>>();
                return Json(driveInfo);
            }
        }
        public IActionResult GetInternalFiles(string path)
        {
            try
            {

             Dictionary<string,List<Tuple<string, string>>> FilesInfo =new Dictionary<string, List<Tuple<string, string>>>();

            string[] files = Directory.GetFiles(path);
            List<Tuple<string, string>> tuplelst = new List<Tuple<string, string>>();
                
            foreach (string file in files)
            {


                tuplelst.Add(new Tuple<string, string>(Path.GetExtension(file), file));

            }
            FilesInfo[path] = new List<Tuple<string, string>>(tuplelst);
            return Json(FilesInfo);

            }
            catch (Exception)
            {
                Dictionary<string, List<Tuple<string, string>>> driveInfo = new Dictionary<string, List<Tuple<string, string>>>();

                driveInfo[path] = new List<Tuple<string, string>>();
                return Json(driveInfo);
            }
        }
                
        [HttpGet]
        public IActionResult GetTnternalDirectoriesByName(string path,string searchPattern)
        {
            try
            {
                Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

                string[] directories =Directory.GetDirectories(path);
                List<Tuple<int, string>> tuplelst = new List<Tuple<int, string>>();


                foreach (string directory in directories)
                {
                    string[] subdirectories = GetDirectories(directory);
                    string directoryName = Path.GetFileName(directory);

                    if (directoryName.ToLower().Contains(searchPattern.ToLower()))
                    {
                        tuplelst.Add(new Tuple<int, string>(subdirectories.Length, directory));
                    }

                }

                driveInfo[path] = new List<Tuple<int, string>>(tuplelst);

                return Json(driveInfo);
            }
            catch (Exception)
            {
                Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

                driveInfo[path] = new List<Tuple<int, string>>();
                return Json(driveInfo);
            }
        }
        public IActionResult GetInternalDriveAndDirectories(string path)
        {
            Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

            string[] directories = GetDirectories(path);
            List<Tuple<int, string>> tuplelst = new List<Tuple<int, string>>();


            foreach (string directory in directories)
            {
                string[] subdirectories = GetDirectories(directory);

                tuplelst.Add(new Tuple<int, string>(subdirectories.Length, directory));

            }

            driveInfo[path] = new List<Tuple<int, string>>(tuplelst);
            return Json(driveInfo);
        }
    }
}