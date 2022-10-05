using System;
using System.Drawing;
using System.Windows.Forms;
using PictureViewerDE.Models;
using PictureViewerDE.Utilities;

namespace PictureViewerDE
{
    public partial class MainForm : Form
    {
        public bool IsFileOpen { get; set; }
        public bool IsFileModified { get; set; }
        public string FilePath { get; set; }
        public string FileData { get; set; }

        private FileBMP _MyFileBMP;

        public FileBMP MyFileBMP{ get { return _MyFileBMP; } set { this._MyFileBMP = value; } }
        public MainForm()
        {
            InitializeComponent();
            Debug.Enable = true;
            Debug.Trace("Constructor just after InitializeComponent()...");

            _MyFileBMP = new FileBMP(this);
        }

        //~~~{ MainForm.Load }~~~//
        private void MainForm_Load(object sender, EventArgs e)
        { Debug.Trace("The main form load event!!");
            this.toolStripStatusLabel1.Text = "Welcome! Use the menu to open an image file.";
        }

        //~~~{ MainForm.KeyDown }~~~//
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            { Debug.Trace("Ctrl+D");
                Controllers.MainController.Decode(this);
            }
            if (e.Control && e.KeyCode == Keys.E)
            { Debug.Trace("Ctrl+E");
                Controllers.MainController.Encode(this);
            }
            if (e.Control && e.KeyCode == Keys.Q)
            { Debug.Trace("Ctrl+Q");
                Controllers.MainController.Exit();
            }
        }

        //~~~{ Menu > File > Open }~~~//
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        { Debug.Trace("");
            Controllers.MainController.Open(this);
        }

        //~~~{ Menu > File > Save }~~~//
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        { Debug.Trace("");
            Controllers.MainController.Save(this);
        }

        //~~~{ Menu > File > Close }~~~//
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        { Debug.Trace("");
            Controllers.MainController.Close(this);
        }

        //~~~{ Menu > File > Exit }~~~//
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        { Debug.Trace("");
            Controllers.MainController.Exit();        
        }

        //~~~{ Menu > Help > User Manual }~~~//
        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        { Debug.Trace("");
            Controllers.MainController.UserManual();
        }

        //~~~{ Menu > Help > About }~~~//
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        { Debug.Trace("");
            Controllers.MainController.About();
        }
    }
}
