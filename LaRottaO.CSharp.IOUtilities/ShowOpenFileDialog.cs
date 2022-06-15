using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.IOUtilities
{
    public class ShowOpenFileDialog
    {
        public static String showOpenFileDialog(String defaultFilename, String defaultExtension, String startingDirectory = null)
        {
            string selectedPath = null;

            var t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog1.Title = "Open file " + defaultExtension.ToUpper();
                openFileDialog1.DefaultExt = defaultExtension.ToUpper();
                openFileDialog1.Filter = "File " + defaultExtension.ToUpper() + " (*" + defaultExtension.ToUpper() + ")|*" + defaultExtension.ToUpper();

                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = defaultFilename;
                openFileDialog1.InitialDirectory = startingDirectory;

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