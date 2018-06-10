using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlBrewer.Pcl.ColorSwatchReader;

namespace ACO_Convert
{
    class Program
    {
        static void Main(string[] args)
        {

            int a = 1;
            int b = 33;
            var result = a | b;
            byte[] buffer = new byte[2];
            buffer[0] = (byte)(b >> 8);
            buffer[1] = (byte)(b >> 0);


            AcoConverter conv = new AcoConverter();
            var ijijwo= conv.ReadPhotoShopSwatchFile(new System.IO.FileStream(@"C:\Users\ginn\Documents\GitHub\node-aco\examples\AcoFilePath.aco", System.IO.FileMode.Open, System.IO.FileAccess.Read));
        }
    }
}
