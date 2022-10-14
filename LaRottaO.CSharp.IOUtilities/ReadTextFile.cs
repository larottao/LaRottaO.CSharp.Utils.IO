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
        public enum PossibleResult

        { SUCCESS, FILE_NOT_FOUND, EXCEPTION }

        public Tuple<Boolean, PossibleResult, String> read(String textFilePath)
        {
            if (!File.Exists(textFilePath))
            {
                return new Tuple<Boolean, PossibleResult, String>(false, PossibleResult.FILE_NOT_FOUND, "");
            }

            try
            {
                StringBuilder sb = new StringBuilder();

                IEnumerable<String> lines = File.ReadLines(textFilePath);

                foreach (var line in lines)
                {
                    sb.Append(line.ToString() + Environment.NewLine);
                }

                return new Tuple<Boolean, PossibleResult, String>(true, PossibleResult.SUCCESS, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when reading file: " + ex.ToString());
                return new Tuple<Boolean, PossibleResult, String>(false, PossibleResult.EXCEPTION, "");
            }
        }
    }
}