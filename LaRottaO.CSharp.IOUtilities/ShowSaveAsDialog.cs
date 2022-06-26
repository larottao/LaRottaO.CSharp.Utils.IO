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
            string selectedPath = null;
            var t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog1.Title = argDescription;
                saveFileDialog1.DefaultExt = argDefaultExtension.ToUpper();
                saveFileDialog1.Filter = argDefaultExtension;

                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = argDefaultFilename;

                saveFileDialog1.ShowDialog(new Form() { TopMost = true });

                selectedPath = saveFileDialog1.FileName;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return selectedPath;
        }
    }
}