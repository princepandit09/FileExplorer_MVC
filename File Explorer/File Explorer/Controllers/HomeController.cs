//using FileExplorer.Models;
using Microsoft.AspNetCore.Mvc;
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