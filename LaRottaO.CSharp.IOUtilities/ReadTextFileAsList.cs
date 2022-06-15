using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class ReadTextFileAsList
    {
        public enum possibleResult

        { SUCCESS, FILE_NOT_FOUND, EXCEPTION }

        public Task<Tuple<Boolean, possibleResult, List<String>>> read(String textFilePath)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(textFilePath))
                {
                    return new Tuple<Boolean, possibleResult, List<String>>(false, possibleResult.FILE_NOT_FOUND, new List<String>());
                }

                try
                {
                    IEnumerable<String> lines = File.ReadLines(textFilePath);

                    return new Tuple<Boolean, possibleResult, List<String>>(true, possibleResult.SUCCESS, new List<String>(lines));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when reading file: " + ex.ToString());
                    return new Tuple<Boolean, possibleResult, List<String>>(false, possibleResult.EXCEPTION, new List<String>());
                }
            });
        }
    }
}