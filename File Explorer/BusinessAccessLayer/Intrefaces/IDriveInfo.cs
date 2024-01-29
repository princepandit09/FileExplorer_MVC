using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Intrefaces
{
    public interface IDriveInfo
    {
        public Dictionary<string, List<Tuple<int, string>>> GetDriveAndDirectories();
        public string[] GetDirectories(string driveLetter);
        public Tuple<string, Dictionary<string, List<Tuple<int, string>>>> GetTnternalDirectoriesByName(string path, string searchPattern);
        public Tuple<string, Dictionary<string, List<Tuple<int, string>>>> GetInternalDriveAndDirectoriesByPath(string path);
        
    }
}
