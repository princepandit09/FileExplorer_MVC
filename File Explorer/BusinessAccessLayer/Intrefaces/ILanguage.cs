using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Intrefaces
{
    public interface Ilanguage
    {
        public LocalizedString GetKey(string key);
    }
}
