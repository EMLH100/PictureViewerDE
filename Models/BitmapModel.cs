using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PictureViewerDE.Utilities;

namespace PictureViewerDE.Models
{
    internal class BitmapModel
    {
        public string FileContent { get; set; }

        public BitmapModel(string fileContent)
        {
            FileContent = fileContent;
        }

        public void Test(MainForm form)
        { Debug.Trace("");
            byte[] fileBytes = Encoding.Convert(Encoding.Default, Encoding.GetEncoding("Windows-1252"), Encoding.Default.GetBytes(FileContent));
            //Debug.Trace($"FileContent=\n{FileContent}\n=FileContent");

            string filename = form.FilePath.Substring(form.FilePath.LastIndexOf('\\') + 1);
            Console.WriteLine($"============[ File: {filename}, Size: {fileBytes.GetLength(0)} bytes ]============");
            Console.WriteLine( "_Address_  _0_ _1_ _2_ _3_ _4_ _5_ _6_ _7_ _8_ _9_ _A_ _B_ _C_ _D_ _E_ _F_ ______Dump______");
            Console.Write("[00000000] ");
            int empty_space = 16 - ((fileBytes.GetLength(0) + 1) % 16) + 1;
            string string_ascii = "";
            for (int i = 0; i < fileBytes.GetLength(0) + empty_space; i++)
            {
                if (i < fileBytes.GetLength(0)) {
                    string dec = String.Format("{0:000}", fileBytes[i]);
                    string hex = " "+fileBytes[i].ToString("x2").ToUpper();

                    Console.Write($"{hex}" + " ");

                    if (fileBytes[i] < 32 && fileBytes[i] != 127)
                    {
                        string_ascii += ".";
                    } else {
                        string_ascii += (char)fileBytes[i];
                    }
                } else {
                    Console.Write($"   " + " ");
                    string_ascii += " ";
                }
                if ((i + 1) % 16 == 0 && i != 0)
                {
                    Console.Write($"{string_ascii}");
                    string_ascii = "";
                    Console.Write("\n");
                    if (i > 10000) {
                        Console.WriteLine($"=== INCOMPLET: Stopped at byte {i} ===");
                        break;
                    }
                    if (i != fileBytes.GetLength(0) + empty_space - 1) {
                        Console.Write($"[{(i + 1).ToString("x8").ToUpper()}] ");
                    }
                }
                
            }
            Console.WriteLine($"============[ File: {filename}, Size: {fileBytes.GetLength(0)} bytes ]============");
        }

        public bool CheckHeader( )
        {
            //  Signature   2 bytes     offset:0000h(0)     'BM'
            string bytes_0_2 = BytesToText(0, 2);
            Debug.Trace($"Signature={bytes_0_2}, expected=" + "BM");

            //  FileSize    4 bytes     0002h  2    File size in bytes
            Int32 bytes_2_4 = BytesToInt(2, 4);
            Debug.Trace($"FileSize={bytes_2_4} (Bytes)");


            //  reserved    4 bytes     0006h  6    unused(= 0)
            Int32 bytes_6_4 = BytesToInt(6, 4);
            Debug.Trace($"reserved={bytes_6_4} (Bytes)");

            //  DataOffset 	4 bytes 	000Ah  10   Offset from beginning of file to the beginning of the bitmap data
            Int32 bytes_10_4 = BytesToInt(10, 4);
            Debug.Trace($"DataOffset={bytes_10_4}");

            //  Size        4 bytes     000Eh  14   Size of InfoHeader = 40
            Int32 bytes_14_4 = BytesToInt(14, 4);
            Debug.Trace($"Size={bytes_14_4}");

            //  Width       4 bytes     0012h   18  Horizontal width of bitmap in pixels
            Int32 bytes_18_4 = BytesToInt(18, 4);
            Debug.Trace($"Width={bytes_18_4}");

            //  Height      4 bytes     0016h  22   Vertical height of bitmap in pixels
            Int32 bytes_22_4 = BytesToInt(22, 4);
            Debug.Trace($"Height={bytes_22_4}");

            //  Planes 	2 bytes 	001Ah 	Number of Planes (=1)
            Int32 bytes_26_2 = BytesToInt(26, 2);
            Debug.Trace($"Planes={bytes_26_2}");

            //  BitsPerPixel 2 bytes    001Ch  28   Bits per Pixel used to store palette entry information. This also identifies in an indirect way the number of possible colors. Possible values are:
            //      1 = monochrome palette.NumColors = 1
            //      4 = 4bit palletized.NumColors = 16
            //      8 = 8bit palletized. NumColors = 256
            //      16 = 16bit RGB. NumColors = 65536
            //      24 = 24bit RGB. NumColors = 16M
            Int32 bytes_28_2 = BytesToInt(28, 2);
            Debug.Trace($"BitsPerPixel={bytes_28_2}");

            //  Compression 4 bytes     001Eh  30  Type of Compression
            //      0 = BI_RGB no compression
            //      1 = BI_RLE8 8bit RLE encoding
            //      2 = BI_RLE4 4bit RLE encoding
            Int32 bytes_30_4 = BytesToInt(30,4);
            Debug.Trace($"Compression={bytes_30_4}");

            //  ImageSize   4 bytes     0022h(compressed) Size of Image, It is valid to set this = 0 if Compression = 0
            Int32 bytes_34_4 = BytesToInt(34, 4);
            Debug.Trace($"ImageSize={bytes_34_4}");

            //  XpixelsPerM     4 bytes     0026h horizontal resolution: Pixels / meter
            Int32 bytes_38_4 = BytesToInt(38, 4);
            Debug.Trace($"XpixelsPerM={bytes_38_4}");

            //  YpixelsPerM     4 bytes     002Ah vertical resolution: Pixels / meter
            Int32 bytes_42_4 = BytesToInt(42, 4);
            Debug.Trace($"YpixelsPerM={bytes_42_4}");

            //  ColorsUsed     4 bytes     002Eh Number of actually used colors. For a 8 - bit / pixel bitmap this will be 100h or 256.
            Int32 bytes_46_4 = BytesToInt(46, 4);
            Debug.Trace($"ColorsUsed={bytes_46_4}");

            //  Important Colors    4 bytes     0032h Number of important colors, 0 = all
            Int32 bytes_50_4 = BytesToInt(46, 4);
            Debug.Trace($"ColorsUsed={bytes_50_4}");

            //  w:bytes_18_4, h:bytes_22_4
            Int32 w = bytes_18_4;
            Int32 h = bytes_22_4;
            Int32 paddin = w % 4;
            Int32 total_pixel = w * h;
            Int32 total_byte = ((3*w) + paddin) * h;
            Debug.Trace($"PADDING={paddin}");
            Debug.Trace($"TOTAL_PIXEL={total_pixel}");
            Debug.Trace($"TOTAL_BYTES={total_byte}");
            Debug.Trace($"MAX_CHAR_IN_MSG={ (float)total_pixel * 3 / 8 }");


            if (bytes_0_2 != "BM") {
                Debug.Trace("Header invalide.");
                return false;
            } else {
                Debug.Trace("Header valide.");
                return true;
            }
        }
        public string BytesToText(int offset, int length)
        {
            string text_string = string.Empty;
            int i = 0;
            while (i < (offset + length))
            {
                string hex = Convert.ToByte(FileContent[i]).ToString("x2");
                text_string += Convert.ToChar(int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
                i++;
            }
            return text_string;
        }

        public string BytesToHexString(int offset, int length)
        {
            string hex_string = string.Empty;
            int i = 0;
            while (i < (offset + length)) {
                hex_string += Convert.ToByte(FileContent[i]).ToString("x2");
                i++;
            }
            return hex_string;
        }
        public string BytesToHexString2(int offset, int length)
        {
            ///https://learn.microsoft.com/en-us/dotnet/api/system.convert.tobyte?view=net-6.0
            string hex_string = string.Empty;
            int i = (offset + length) - 1;
            while (i >= offset)
            {
                int myB = Convert.ToByte(FileContent[i]);
                uint myUint = Convert.ToUInt32(myB);
                myB = Convert.ToByte(myUint);
                string nyBstr = myB.ToString("x2");
                hex_string += nyBstr;
                //Debug.Trace("toBYTE==="+Convert.ToByte(FileContent[i]).ToString("x2"));
                i--;
            }
            Debug.Trace(hex_string);
            return hex_string;
        }
        public Int32 BytesToInt(int offset, int length)
        {
            string hex_string = BytesToHexString2(offset, length);
            Int32 intValue = Int32.Parse(hex_string, System.Globalization.NumberStyles.HexNumber);
            return intValue;
        }
        public int BytesToInt2(int offset, int length)
        {
            //int intValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            // The characters to encode:
            //    Latin Small Letter Z (U+007A)
            //    Latin Small Letter A (U+0061)
            //    Combining Breve (U+0306)
            //    Latin Small Letter AE With Acute (U+01FD)
            //    Greek Small Letter Beta (U+03B2)
            //    a high-surrogate value (U+D8FF)
            //    a low-surrogate value (U+DCFF)
            char[] myChars = FileContent.ToCharArray(); ;
            // char[] myChars = new char[] { 'z', 'a', '\u0306', '\u01FD', '\u03B2', '\uD8FF', '\uDCFF' };

            byte[] b1 = Encoding.Default.GetBytes(FileContent);
                //Debug.Trace(System.Text.Encoding.Default.GetString(b1));

            // Get different encodings.
            Encoding u7 = Encoding.UTF7;
            Encoding u8 = Encoding.UTF8;
            Encoding u16LE = Encoding.Unicode;
            Encoding u16BE = Encoding.BigEndianUnicode;
            Encoding u32 = Encoding.UTF32;

            byte[] fileBytes = Encoding.Convert(Encoding.Default, Encoding.GetEncoding("Windows-1252"), Encoding.Default.GetBytes(FileContent));
            
            // Encode the entire array, and print out the counts and the resulting bytes.
            //PrintCountsAndBytes(myChars, u7);
            //PrintCountsAndBytes(myChars, u8);
            //PrintCountsAndBytes(myChars, u16LE);
            //PrintCountsAndBytes(myChars, u16BE);
            //PrintCountsAndBytes(myChars, u32);



            //string hex_string = string.Empty;
            //string hex_string = "0";
            /*
            int i = (offset + length) - 1;
            while (i >= offset)
            {

                
                if (fileBytes[i] == '¶')
                {
                    hex_string += "b6";
                } else { 
                    hex_string += Convert.ToByte(FileContent[i]).ToString("x2");
                }
                
                i--;
            }
            */
            //Debug.Print($"{byte.Parse(hex_string, System.Globalization.NumberStyles.HexNumber)}");
            ////return int.Parse(hex_string, System.Globalization.NumberStyles.HexNumber);
            return 0;
        }
        public static void PrintCountsAndBytes(char[] chars, Encoding enc)
        {

            // Display the name of the encoding used.
            Console.Write("{0,-30} :", enc.ToString());

            // Display the exact byte count.
            int iBC = enc.GetByteCount(chars);
            Console.Write(" {0,-3}", iBC);

            // Display the maximum byte count.
            int iMBC = enc.GetMaxByteCount(chars.Length);
            Console.Write(" {0,-3} :", iMBC);

            // Encode the array of chars.
            byte[] bytes = enc.GetBytes(chars);

            // Display all the encoded bytes.
            PrintHexBytes(bytes);
        }

        public static void PrintHexBytes(byte[] bytes)
        {

            if ((bytes == null) || (bytes.Length == 0))
            {
                Console.WriteLine("<none>");
            }
            else
            {
                for (int i = 0; i < bytes.Length; i++)
                    Console.Write("{0:X2} ", bytes[i]);
                Console.WriteLine();
            }
        }

        public string TextToBits(string message)
        {
            //int[][] msg_list = new int[0][];

            List<List<int>> msg_list = new List<List<int>>();

            foreach (char c in message)
            {
                List<int> msg_letter = new List<int>();
                //int[] msg_letter = new int[8];
                string number = ((byte)c).ToString();
                int fromBase = 10;
                int toBase = 2;
                string s_bit = Convert.ToString(Convert.ToInt32(number, fromBase), toBase);
                s_bit = s_bit.PadLeft(8, '0');
                foreach (char l in s_bit)
                {
                    if (l == '1') {
                        msg_letter.Add(1);

                    } else if (l == '0') {
                        msg_letter.Add(0);
                    }
                }
                if (msg_letter.Count < 8) { }
                msg_list.Add(msg_letter);
                Debug.Trace(c.ToString() +" = "+ String.Join("",msg_letter));
            }

            string temp1 = "";
            for (int i = 0; i < msg_list.Count; i++)
            {
                string temp2 = "";
                for (int j = 0; j < msg_list[i].Count; j++)
                {
                    temp2 += msg_list[i][j];
                }
                //Debug.Trace(temp2);
                temp1 += temp2;
            }
            Debug.Trace(temp1 +", bit="+ temp1.Length + ", char=" + (temp1.Length / 8));

            return msg_list.ToString(); //temp1
        }
    }
}

/*            
To convert a string to a byte[] for this purpose, use Encoding.Default.GetBytes()[1].

To convert a byte[] back to a string for display or other string-based processing, use Encoding.Default.GetString().
            
//using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
//{
//    stream.Position = 24;
//    stream.WriteByte(0x04);
//}

if (checkHeader(fileString))
{
    Console.WriteLine("Header is OK!");

    byte x = 255;

    string hex = String.Format("0x{0:X4}", x);
    getBytes(fileString, 28, 2);

    Console.WriteLine(hex); // prints "0x003C"
}
int adr = 0;
Console.WriteLine("Byte#=" + adr + ", Hex=" + getCharToHex(fileString[adr]) + ", ascii=" + fileString[adr]);
            //Console.WriteLine(fileString);

*/



//  Signature 2 bytes 0000h 'BM'
//  FileSize 4 bytes 0002h File size in bytes
//  DataOffset 	4 bytes 	000Ah 	Offset from beginning of file to the beginning of the bitmap data
//  Size    4 bytes     000Eh Size of InfoHeader = 40
//  Width   4 bytes     0012h Horizontal width of bitmap in pixels
//  Height  4 bytes     0016h Vertical height of bitmap in pixels
//  Bits Per Pixel  2 bytes     001Ch Bits per Pixel used to store palette entry information. This also identifies in an indirect way the number of possible colors. Possible values are:
//  1 = monochrome palette.NumColors = 1
//  4 = 4bit palletized.NumColors = 16
//  8 = 8bit palletized. NumColors = 256
//  16 = 16bit RGB. NumColors = 65536
//  24 = 24bit RGB. NumColors = 16M
//  Compression     4 bytes     001Eh Type of Compression
//  0 = BI_RGB no compression
//  1 = BI_RLE8 8bit RLE encoding
//  2 = BI_RLE4 4bit RLE encoding