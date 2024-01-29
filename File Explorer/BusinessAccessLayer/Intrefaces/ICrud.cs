using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Intrefaces
{
    public interface ICrud
    {
        public string DeleteData(string oldPath);

        public string SaveData(string newPath, string oldPath, bool isDelete);
    }
}
