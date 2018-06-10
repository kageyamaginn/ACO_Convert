using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACO_Convert
{
    public class AcoWriter
    {
        //ACO files take 16 bit words.
public void writeValue(StreamWriter writeStream, int value) {
            var buffer = new byte[2];

           // buffer.writeUInt16BE(value, 0);
           
            writeStream.Write(buffer);
        }

        //Convenient way to write RGB integer values. Expected with this multiplier.
        private void  writeRGBValue(StreamWriter writeStream,int value)
        {
            writeValue(writeStream, value * 256);
        }

        //Convenient for adding zero
        private void  writeZero(StreamWriter writeStream)
        {
            writeValue(writeStream, 0);
        }

        private int readValue(byte[] readBuffer)
        {
            throw new Exception();
            //Convert.to readBuffer[0]
        }

        private int readRGBValue(byte[] readBuffer)
        {
            return readValue(readBuffer) / 256;
        }

        private String readCharValue(byte[] readBuffer)
        {
            return fromCharCode(readValue(readBuffer));
        }

        private String fromCharCode(params int[] values)
        {
            String result = "";
            foreach (int v in values)
            {
                result += ((char)v).ToString();
            }
            return result;
        }

        private String sanitizeFilename(String filename)
        {
            filename = filename + "aco-" + DateTime.Now.ToLongDateString() + ".aco";
            if (filename.LastIndexOf(".aco") != filename.Length - 4) filename = filename + ".aco";
            return filename;
        }

        /*exports.make = */
        public void MakeFile(String filename, List<ColorInfo> colors/*, callback*/)
        {
            filename = sanitizeFilename(filename);
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

            //colors = colors instanceof Array ? colors: [];

            var aco = new StreamWriter(fs);

            //Version 2
            writeValue(aco, 2);

            //Number of colors
            writeValue(aco, colors.Count);

            try
            {
                foreach (ColorInfo colorInfo in colors)
                {
                    var hex = colorInfo.color;
                    var name = colorInfo.name + hex;

                    //Parse RGB
                    var color = ColorTranslator.FromHtml(hex);

                    int r = Convert.ToInt16(color.R);
                    int g = Convert.ToInt16(color.G);
                    int b = Convert.ToInt16(color.B);

                    int[] rgb = new int[] { r, g, b };
                    //rgb = rgb.filter(function(value) {
                    //    return !isNaN(value);
                    //});

                    //Make sure we have valid values
                    if (rgb.Length < 3)
                    {
                        throw new Exception("color Exception");
                    }

                    //Write 0, for RGB color space
                    writeZero(aco);

                    //R
                    writeRGBValue(aco, rgb[0]);
                    //G
                    writeRGBValue(aco, rgb[1]);
                    //B
                    writeRGBValue(aco, rgb[2]);
                    //Pad (we need w, x, y, and z values. RGB only has w, x, y - so z is zero.
                    writeZero(aco);

                    //Name
                    writeZero(aco);
                    writeValue(aco, name.Length + 1);
                    for (var i = 0; i < name.Length; i++)
                    {
                        writeValue(aco, name[i]);
                    }
                    writeZero(aco);
                }

               
            }
            catch (Exception e)
            {
                var error = "Parse Error";
              
            }

            aco.Dispose();



        }
    }

   
}
