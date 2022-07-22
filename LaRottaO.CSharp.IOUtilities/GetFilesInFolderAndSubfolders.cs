using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class GetFilesInFolderAndSubfolders
    {
        public Task<List<String>> getFilesInFolderAndSubfolders(String argRootFolderPath, String argSearchedExtension)
        {
            return Task.Run(() =>
            {
                DirectoryInfo directoryInfoRoot = new DirectoryInfo(argRootFolderPath);
                DirectoryInfo[] directoryInfoSubfolders = directoryInfoRoot.GetDirectories();
                List<String> foundFilesList = new List<String>();

                FileInfo[] fileInfoArray = directoryInfoRoot.GetFiles(argSearchedExtension);

                foreach (FileInfo fileInfo in fileInfoArray)

                {
                    foundFilesList.Add(fileInfo.FullName);
                }

                foreach (DirectoryInfo directoryInfoSubfolder in directoryInfoSubfolders)
                {
                    foreach (FileInfo file in fileInfoArray)

                    {
                        foundFilesList.Add(file.FullName);
                    }
                }

                return foundFilesList;
            });
        }
    }
}