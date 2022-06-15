using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class ReadTextFile
    {
        public enum possibleResult

        { SUCCESS, FILE_NOT_FOUND, EXCEPTION }

        public Task<Tuple<Boolean, possibleResult, String>> read(String textFilePath)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(textFilePath))
                {
                    return new Tuple<Boolean, possibleResult, String>(false, possibleResult.FILE_NOT_FOUND, "");
                }

                try
                {
                    StringBuilder sb = new StringBuilder();

                    IEnumerable<String> lines = File.ReadLines(textFilePath);

                    foreach (var line in lines)
                    {
                        sb.Append(line.ToString() + Environment.NewLine);
                    }

                    return new Tuple<Boolean, possibleResult, String>(false, possibleResult.SUCCESS, sb.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when reading file: " + ex.ToString());
                    return new Tuple<Boolean, possibleResult, String>(false, possibleResult.EXCEPTION, "");
                }
            });
        }
    }
}