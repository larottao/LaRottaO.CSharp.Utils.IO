using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.IOUtilities
{
    public class ShowSaveAsDialog
    {
        public static String showSaveAsDialog(String defaultFilename, String defaultExtension)
        {
            string selectedPath = null;
            var t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog1.Title = "Save file " + defaultExtension.ToUpper();
                saveFileDialog1.DefaultExt = defaultExtension.ToUpper();
                saveFileDialog1.Filter = "File " + defaultExtension.ToUpper() + " (*" + defaultExtension.ToUpper() + ")|*" + defaultExtension.ToUpper();

                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = defaultFilename;

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