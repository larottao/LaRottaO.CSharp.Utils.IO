using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class GetFilesInFolder
    {
        public Task<List<String>> get(String argFolderPath, String argSearchedExtension)
        {
            return Task.Run(() =>
            {
                List<String> foundFilesList = new List<String>();

                DirectoryInfo directoryInfo = new DirectoryInfo(argFolderPath);

                FileInfo[] fileInfoArray = directoryInfo.GetFiles(argSearchedExtension);

                foreach (FileInfo fileInfo in fileInfoArray)

                {
                    foundFilesList.Add(fileInfo.FullName);
                }

                return foundFilesList;
            });
        }
    }
}