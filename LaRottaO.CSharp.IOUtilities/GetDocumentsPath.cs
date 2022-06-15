using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public static class GetDocumentsPath
    {
        public static String get()
        {
            return Environment.SpecialFolder.MyDocuments + @"\";
        }
    }
}