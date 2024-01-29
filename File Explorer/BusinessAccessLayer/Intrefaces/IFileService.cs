using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Intrefaces
{
    public interface IFileService
    {
        public Tuple<string, Dictionary<string, List<Tuple<string, string>>>> GetInternalFiles(string path);
        public Tuple<string, Dictionary<string, List<Tuple<string, string>>>> GetInternalFilesByName(string path, string searchPattern);
        public Tuple<string, string> GetFileContent(string path);
    }
}
