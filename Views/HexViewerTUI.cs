using System;
using System.Text;
using PictureViewerDE.Utilities;

namespace PictureViewerDE.Views
{
    public class HexViewerTUI
    {
        public void ViewHex(MainForm form, string fileContent)
        { Debug.Trace("");
            ViewData(form, fileContent, "hex");
        }

        public void ViewDec(MainForm form, string fileContent, string output = "dec")
        { Debug.Trace("");
            ViewData(form, fileContent, "dec");
        }
        public void ViewData(MainForm form, string fileContent, string output)
        { Debug.Trace("");
            byte[] fileBytes = Encoding.Convert(Encoding.Default, Encoding.GetEncoding("Windows-1252"), Encoding.Default.GetBytes(fileContent));
            //Debug.Trace($"FileContent=\n{FileContent}\n=FileContent");

            string filename = form.FilePath.Substring(form.FilePath.LastIndexOf('\\') + 1);
            Console.WriteLine($"============[ File: {filename}, Size: {fileBytes.GetLength(0)} bytes ]============");
            Console.WriteLine("_Address_  _0_ _1_ _2_ _3_ _4_ _5_ _6_ _7_ _8_ _9_ _A_ _B_ _C_ _D_ _E_ _F_ ______Dump______");
            Console.Write("[00000000] ");
            int empty_space = 16 - ((fileBytes.GetLength(0) + 1) % 16) + 1;
            string string_ascii = "";
            for (int i = 0; i < fileBytes.GetLength(0) + empty_space; i++)
            {
                if (i < fileBytes.GetLength(0))
                {
                    string dec = String.Format("{0:000}", fileBytes[i]);
                    string hex = " " + fileBytes[i].ToString("x2").ToUpper();
                    string outp = string.Empty;
                    if (output == "hex") {
                        outp = hex;
                    } else if (output == "dec") {
                        outp = dec;
                    } else {
                        Debug.Trace("ERROR: Empty data string.");
                    }
                    Console.Write($"{outp}" + " ");

                    if (fileBytes[i] < 32 && fileBytes[i] != 127)
                    {
                        string_ascii += ".";
                    }
                    else
                    {
                        string_ascii += (char)fileBytes[i];
                    }
                }
                else
                {
                    Console.Write($"   " + " ");
                    string_ascii += " ";
                }
                if ((i + 1) % 16 == 0 && i != 0)
                {
                    Console.Write($"{string_ascii}");
                    string_ascii = "";
                    Console.Write("\n");
                    if (i > 10000)
                    {
                        Console.WriteLine($"=== INCOMPLET: Stopped at byte {i} ===");
                        break;
                    }
                    if (i != fileBytes.GetLength(0) + empty_space - 1)
                    {
                        Console.Write($"[{(i + 1).ToString("x8").ToUpper()}] ");
                    }
                }

            }
            Console.WriteLine($"============[ File: {filename}, Size: {fileBytes.GetLength(0)} bytes ]============");
        }
    }
}
