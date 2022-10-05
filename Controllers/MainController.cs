using System.Drawing;
using System.Windows.Forms;
using PictureViewerDE.Models;
using PictureViewerDE.Utilities;
using PictureViewerDE.Views;

namespace PictureViewerDE.Controllers
{
    internal static class MainController
    {
        //---( open )---//
        public static void Open(MainForm form)
        { Debug.Trace("");
            form.MyFileBMP.OpenFileDialog();
        }

        //---( save )---//
        public static void Save(MainForm form)
        { Debug.Trace("");
            ///form.MyFileBMP.SaveFile();
        }

        //---( close )---//
        public static void Close(MainForm form)
        { Debug.Trace("");
            form.MyFileBMP.CloseFile();
        }
        
        //---( encode )---//
        public static void Encode(MainForm form)
        { Debug.Trace("");
            /////////////////
            ////check if imagie is loaded first

            //BitmapModel myBitmap = new BitmapModel(form.FileData);
            //myBitmap.Test(form);
            HexViewerTUI myView = new HexViewerTUI();
            myView.ViewHex(form, form.FileData);
            myView.ViewDec(form, form.FileData);


            ////////////////
        }

        //---( decode )---//
        public static void Decode(MainForm form)
        { Debug.Trace("");
            form.label1.Parent = form.pictureBox1;
            form.label1.BackColor = Color.FromArgb(127, 0, 0, 0); //Color.Transparent
            form.label1.Location = new Point(0 , 0);
            form.label1.Dock = DockStyle.Fill;
            form.label1.AutoSize = false;
            form.label1.Width = form.pictureBox1.Width;
            form.label1.Height = form.pictureBox1.Height;
            form.label1.Anchor = AnchorStyles.None;
            form.label1.Visible = true;
            
            //this.label1.Location = pictureBox1.PointToClient(this.PointToScreen(label1.Location));
            //////////////
            BitmapModel myBitmap2 = new BitmapModel(form.FileData); // HARDCODED 2nd OBJECT
            myBitmap2.CheckHeader();
            myBitmap2.TextToBits("hello");
            myBitmap2.TextToBits("Les sanglots longs Des violons De l'automne. Blessent mon coeur D'une langueur Monotone.");

            ///////////////
        }

        //---( exit )---//
        public static void Exit()
        { Debug.Trace("Bye bye!!");
            Application.Exit(); ///System.Environment.Exit(1);
        }

        //---( User Manual )---//
        public static void UserManual()
        { Debug.Trace("");
            EncodeForm ef = new EncodeForm();
            ef.ShowDialog();
            
            /*
            MessageBox.Show(
                "[Ctrl+E] - Encode.\n"
                +"[Ctrl+D] - Decode.\n"
                +"[Ctrl+Q] - Quit.\n"
                ,"User Manual"
            );*/
        }

        //---( About )---//
        public static void About()
        { Debug.Trace("");
            AboutBox ab = new AboutBox();
            ab.ShowDialog();

            /*
            MessageBox.Show(
                "Author: \tLe Huu, Etienne-Minh\n"
                +"SN: \t2211757\n"
                ,"About"
            );*/
        }
    }
}
