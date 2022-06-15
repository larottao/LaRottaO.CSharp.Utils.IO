using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public static class GetAppPath
    {
        public static String get()
        {
            return AppContext.BaseDirectory + @"\";
        }
    }
}