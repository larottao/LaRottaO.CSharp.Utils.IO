using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.IOUtilities
{
    public static class ShowSaveAsDialog
    {
        public static String showSaveAsDialog(String argDefaultFilename, String argDefaultExtension, String argDescription = "")
        {
            //The filter string must contain a description of the filter,
            //followed by the vertical bar (|) and the filter pattern.
            //The strings for different filtering options must also be separated by the vertical bar.
            //Example: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            //"Supported extensions | *.0??;*.1??;*.2??;*.3??;*.4??;*.5??;*.6??;*.7??;*.8??;*.9??";

            string filePathSelectedByUser = null;
            var t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog.Title = argDescription;
                saveFileDialog.DefaultExt = argDefaultExtension.ToUpper();
                saveFileDialog.Filter = argDefaultExtension;

                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = argDefaultFilename;

                saveFileDialog.ShowDialog(new Form() { TopMost = true });

                if (saveFileDialog.FileName != null && !saveFileDialog.FileName.Equals(argDefaultFilename))
                {
                    filePathSelectedByUser = saveFileDialog.FileName;
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