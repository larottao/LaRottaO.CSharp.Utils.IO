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
            //For the filter, use
            //"Text files (*.txt)|*.txt|All files (*.*)|*.*"

            string selectedPath = null;

            var t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog1.Title = argDescription;
                openFileDialog1.DefaultExt = argDefaultExtension.ToUpper();
                openFileDialog1.Filter = argDefaultExtension;
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = argDefaultFilename;
                openFileDialog1.InitialDirectory = argStartingDirectory;

                openFileDialog1.ShowDialog(new Form() { TopMost = true });

                selectedPath = openFileDialog1.FileName;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return selectedPath;
        }
    }
}