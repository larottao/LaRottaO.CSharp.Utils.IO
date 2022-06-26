using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.IOUtilities
{
    public static class ShowOpenFileDialog
    {
        public static String showOpenFileDialog(String argDefaultFilename, String argDefaultExtension = "Text files (*.txt)|*.txt|All files (*.*)|*.*", String argStartingDirectory = null, String argDescription = "")
        {
            //The filter string must contain a description of the filter,
            //followed by the vertical bar (|) and the filter pattern.
            //The strings for different filtering options must also be separated by the vertical bar.
            //Example: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            //"Supported extensions | *.0??;*.1??;*.2??;*.3??;*.4??;*.5??;*.6??;*.7??;*.8??;*.9??";

            string filePathSelectedByUser = null;

            var t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Title = argDescription;
                openFileDialog.DefaultExt = argDefaultExtension.ToUpper();
                openFileDialog.Filter = argDefaultExtension;
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = argDefaultFilename;
                openFileDialog.InitialDirectory = argStartingDirectory;

                openFileDialog.ShowDialog(new Form() { TopMost = true });

                if (openFileDialog.FileName != null && !openFileDialog.Equals(argDefaultFilename))
                {
                    filePathSelectedByUser = openFileDialog.FileName;
                }
                else
                {
                    filePathSelectedByUser = "";
                }
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return filePathSelectedByUser;
        }
    }
}