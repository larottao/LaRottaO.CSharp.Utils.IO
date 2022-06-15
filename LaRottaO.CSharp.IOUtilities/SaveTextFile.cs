using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.IOUtilities
{
    public class SaveTextFile
    {
        public Task<Boolean> saveTextFile(String textToSave, String FilePath, Boolean showDialog = false)
        {
            try
            {
                File.WriteAllText(FilePath, textToSave);

                if (showDialog)
                {
                    MessageBox.Show("File successfuly saved at " + FilePath + ". ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                if (showDialog)
                {
                    MessageBox.Show("ERROR: Unable to save file " + FilePath + ". " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return Task.FromResult(false);
            }
        }
    }
}