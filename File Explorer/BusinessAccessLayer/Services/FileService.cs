using BusinessAccessLayer.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class FileService:IFileService
    {

        public Tuple<string, Dictionary<string, List<Tuple<string, string>>>> GetInternalFiles(string path)
        {
            string message = "";
            try
            {
                Dictionary<string, List<Tuple<string, string>>> filesInfo = new Dictionary<string, List<Tuple<string, string>>>();

                string[] files = Directory.GetFiles(path);
                List<Tuple<string, string>> tuplelst = new List<Tuple<string, string>>();

                foreach (string file in files)
                {


                    tuplelst.Add(new Tuple<string, string>(Path.GetExtension(file), file));

                }
                filesInfo[path] = new List<Tuple<string, string>>(tuplelst);
                return new Tuple<string, Dictionary<string, List<Tuple<string, string>>>>(message, filesInfo);

            }
            catch (Exception ex)
            {
                Dictionary<string, List<Tuple<string, string>>> driveInfo = new Dictionary<string, List<Tuple<string, string>>>();

                driveInfo[path] = new List<Tuple<string, string>>();
                return new Tuple<string, Dictionary<string, List<Tuple<string, string>>>>(ex.Message, driveInfo);
            }
        }

        public Tuple<string, string> GetFileContent(string path)
        {
            string message = "";
            try
            {
                string fileContent = System.IO.File.ReadAllText(path);

                return new Tuple<string, string>(message, fileContent);
            }
            catch (Exception ex)
            {
                return new Tuple<string, string>(ex.Message, "");
            }
        }

        public Tuple<string, Dictionary<string, List<Tuple<string, string>>>>  GetInternalFilesByName(string path, string searchPattern)
        {
            string message = "";
            try
            {
                Dictionary<string, List<Tuple<string, string>>> filesInfo = new Dictionary<string, List<Tuple<string, string>>>();

                string[] files = Directory.GetFiles(path); ;
                List<Tuple<string, string>> tuplelst = new List<Tuple<string, string>>();

                foreach (string file in files)
                {
                    string filename = Path.GetFileName(file);
                    if (filename.ToLower().Contains(searchPattern.ToLower()))
                    {

                        tuplelst.Add(new Tuple<string, string>(Path.GetExtension(file), file));

                    }


                }
                filesInfo[path] = new List<Tuple<string, string>>(tuplelst);
                return new Tuple<string, Dictionary<string, List<Tuple<string, string>>>>(message,filesInfo);
            }
            catch (Exception ex)
            {
                Dictionary<string, List<Tuple<string, string>>> filesInfo = new Dictionary<string, List<Tuple<string, string>>>();

                filesInfo[path] = new List<Tuple<string, string>>();
                return new Tuple<string, Dictionary<string, List<Tuple<string, string>>>>(ex.Message, filesInfo);
            }
        }

    }
}
