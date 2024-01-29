using BusinessAccessLayer.Intrefaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class DriveInfoService:IDriveInfo
    {
        public Dictionary<string, List<Tuple<int, string>>> GetDriveAndDirectories()
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

        public string[] GetDirectories(string driveLetter)
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

        public Tuple<string, Dictionary<string, List<Tuple<int, string>>>> GetTnternalDirectoriesByName(string path, string searchPattern)
        {
            string message="";
            try
            {
                Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

                string[] directories = Directory.GetDirectories(path);
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
                return new Tuple<string, Dictionary<string, List<Tuple<int, string>>>>(message, driveInfo);
               
            }
            catch (Exception ex)
            {
                Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

                driveInfo[path] = new List<Tuple<int, string>>();
                return new Tuple<string, Dictionary<string, List<Tuple<int, string>>>>(ex.Message, driveInfo);

            }
        }

        public Tuple<string, Dictionary<string, List<Tuple<int, string>>>> GetInternalDriveAndDirectoriesByPath(string path)
        {
            string message = "";
            try
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
                return new Tuple<string, Dictionary<string, List<Tuple<int, string>>>>(message, driveInfo);

            }
            catch (Exception ex)
            {
                Dictionary<string, List<Tuple<int, string>>> driveInfo = new Dictionary<string, List<Tuple<int, string>>>();

                driveInfo[path] = new List<Tuple<int, string>>();
                return new Tuple<string, Dictionary<string, List<Tuple<int, string>>>>(ex.Message, driveInfo);
            }
        }


    }
}
