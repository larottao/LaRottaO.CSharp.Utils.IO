using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class AppendToTextFile
    {
        public Task append(String argTextToAppend, String argFilePath, Boolean argInsertTimeStamp, String argTitle = null)
        {
            try
            {
                String fullFilePath = Path.GetFullPath(argFilePath);

                if (!Directory.Exists(Path.GetDirectoryName(argFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(argFilePath));
                }

                if (!File.Exists(fullFilePath))
                {
                    using (StreamWriter sw = File.CreateText(fullFilePath))
                    {
                        if (argTitle != null)
                        {
                            sw.WriteLine(argTitle);
                        }
                    }
                }

                using (StreamWriter sw = File.AppendText(fullFilePath))
                {
                    if (argInsertTimeStamp)
                    {
                        String timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sw.WriteLine(timeStamp + ": " + argTextToAppend);
                    }
                    else
                    {
                        sw.WriteLine(argTextToAppend);
                    }
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to append text to file: " + ex.ToString());
                return Task.FromResult(false);
            }
        }
    }
}