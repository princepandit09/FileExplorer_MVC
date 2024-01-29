using BusinessAccessLayer.Enums;
using BusinessAccessLayer.Intrefaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class CrudService:ICrud
    {
        public static readonly UtilityService utilityService = new UtilityService();

        public string SaveData(string newPath, string oldPath, bool isDelete)
        {
            string message="";
            try
            {
                Data_Type datatype =utilityService.CheckContentType(oldPath);


                if (datatype == Data_Type.Directory)
                {
                    // Copy the entire directory to the new path
                    string subdirName = Path.GetFileName(oldPath);
                    string destSubDir = Path.Combine(newPath, subdirName);
                     utilityService.CopyDirectory(oldPath, destSubDir);

                }
                else if (datatype == Data_Type.File)
                {
                    string fileContent = System.IO.File.ReadAllText(oldPath);
                    System.IO.File.WriteAllText(newPath, fileContent);
                }
                else
                {
                  return message = Data_Type.NotDefine.ToString();
                }
                if (isDelete)
                {
                   DeleteData(oldPath);
                }

                return message;
            }
            catch (Exception ex)
            {
                return message = ex.Message;
            }
        }

        public string DeleteData(string oldPath)
        {
            string message;
            try
            {
                Data_Type datatype =utilityService.CheckContentType(oldPath);

                if (datatype == Data_Type.Directory)
                {
                    Directory.Delete(oldPath, true);//second parameter, recursive, is set to true. When recursive is true, Directory.Delete deletes all files and subdirectories recursively within the specified directory.

                }
                else if (datatype ==Data_Type.File)
                {
                    System.IO.File.Delete(oldPath);
                }
                return message = "";

            }
            catch (Exception ex)
            {
                return message = ex.Message;
            }
        }

    }
}
