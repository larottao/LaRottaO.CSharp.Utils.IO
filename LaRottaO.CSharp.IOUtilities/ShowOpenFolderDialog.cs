using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.IOUtilities
{
    public class ShowOpenFolderDialog
    {
        public static String showOpenFolderDialog()
        {
            string selectedPath = null;
            var t = new Thread((ThreadStart)(() =>
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

                folderBrowserDialog.ShowDialog((new Form() { TopMost = true }));

                selectedPath = folderBrowserDialog.SelectedPath;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return selectedPath;
        }
    }
}