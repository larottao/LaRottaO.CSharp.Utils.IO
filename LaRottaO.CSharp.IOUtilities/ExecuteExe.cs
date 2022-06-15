using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class ExecuteExe
    {
        public Task<string> execute(String argExePath, String argExeArguments, Boolean argCreateNoWindow = true)
        {
            return Task.Run(() =>
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                    startInfo.FileName = argExePath;
                    startInfo.Arguments = argExeArguments;

                    process.StartInfo = startInfo;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    process.StartInfo.CreateNoWindow = argCreateNoWindow;

                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    process.Close();

                    return output;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            });
        }
    }
}