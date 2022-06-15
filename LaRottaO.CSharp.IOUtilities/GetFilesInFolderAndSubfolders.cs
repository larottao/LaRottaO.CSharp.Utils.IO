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
        public Task<List<String>> getFilesInFolderAndSubfolders(String folderPath, String searchedExtension)
        {
            return Task.Run(() =>
            {
                DirectoryInfo dirInfoCarpetaPadre = new DirectoryInfo(folderPath);
                DirectoryInfo[] dirInfoSubCarpetas = dirInfoCarpetaPadre.GetDirectories();

                List<String> foundFilesList = new List<String>();

                foreach (DirectoryInfo dirInfoSubCarpeta in dirInfoSubCarpetas)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(dirInfoSubCarpeta.FullName);

                    FileInfo[] fileInfoArray = directoryInfo.GetFiles(searchedExtension);

                    foreach (FileInfo file in fileInfoArray)

                    {
                        foundFilesList.Add(file.FullName);
                    }
                }

                return foundFilesList;
            }
        }
    }
}