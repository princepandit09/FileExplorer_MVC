using BusinessAccessLayer.Enums;
using BusinessAccessLayer.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class UtilityService
    {
        public Data_Type CheckContentType(string path)
        {
            
            try
            {
                FileAttributes attributes = System.IO.File.GetAttributes(path);

                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Console.WriteLine("The path points to a directory.");
                    return Data_Type.Directory;
                }
                else
                {
                    return Data_Type.File;
                }
            }
            catch (Exception)
            {
                return Data_Type.NotDefine;
            }
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            string[] files = Directory.GetFiles(sourceDir);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destDir, fileName);
                System.IO.File.Copy(file, destFile, false); // Set the third parameter to true to overwrite existing files
            }

            string[] subdirectories = Directory.GetDirectories(sourceDir);
            foreach (string subdirectory in subdirectories)
            {
                string subdirName = Path.GetFileName(subdirectory);
                string destSubDir = Path.Combine(destDir, subdirName);
                CopyDirectory(subdirectory, destSubDir);
            }
        }


    }
}
