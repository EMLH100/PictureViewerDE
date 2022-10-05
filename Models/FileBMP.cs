using System.IO;
using System.Text;
using System.Windows.Forms;
using PictureViewerDE.Utilities;

namespace PictureViewerDE.Models
{
    public class FileBMP
    {
        //~~~{ Properties }~~~//
        private MainForm _mainForm = null;

        //~~~{ Constructors }~~~//
        public FileBMP(MainForm form)
        {
            _mainForm = form;
        }

        //~~~{ Methods }~~~//
        public void OpenFileDialog()
        {
            Debug.Trace("");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = System.AppContext.BaseDirectory;   // c:\\
                openFileDialog.Filter = "Bitmap images(*.bmp)|*.bmp"; // txt files(*.txt) | *.txt | All files(*.*) | *.*
                openFileDialog.FilterIndex = 2; // ummm ?!?! 
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _mainForm.FilePath = openFileDialog.FileName;
                    _mainForm.IsFileOpen = true;
                    _mainForm.toolStripStatusLabel1.Text = _mainForm.FilePath;
                    _mainForm.pictureBox1.Image = System.Drawing.Image.FromFile(_mainForm.FilePath);
                    using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), Encoding.Default, true))
                    {
                        _mainForm.FileData = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
        }

        public void CloseFile()
        {
            Debug.Trace("");
            if (_mainForm.IsFileOpen) {
                _mainForm.pictureBox1.Image = null;
                _mainForm.IsFileOpen = false;
                _mainForm.toolStripStatusLabel1.Text = "Stand by.";
            } else if (!_mainForm.IsFileOpen) {
                _mainForm.toolStripStatusLabel1.Text = "No file currently opened.";
            } else {
                Debug.Trace("ERROR: IsFileOpen==null");
            }
        }

        public void SaveFile()
        {
            Debug.Trace("");
            /*
            // ADD CHECK IF FILE IS OPEN AND/OR MODIFIED
            using (FileStream fs = File.Create(_mainForm.FilePath + ".MOD")) // temp fix for locked file
            {
                // writing data in string
                byte[] info = new UTF8Encoding(true).GetBytes(_mainForm.FileData);
                fs.Write(info, 0, info.Length);

                // writing data in bytes already
                byte[] data = new byte[] { 0x0 };
                fs.Write(data, 0, data.Length);
            }
            */
        }
    }
}

