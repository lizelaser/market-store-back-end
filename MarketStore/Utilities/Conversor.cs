using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Utilities
{
    public class Conversor
    {
        public static (bool, string) SaveImage(string wwwpath, string base64String)
        {
            try
            {
                if (base64String != null)
                {
                    using MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64String));
                    using Bitmap bm2 = new Bitmap(ms);
                    Guid uuid = System.Guid.NewGuid();
                    string file = uuid.ToString() + ".jpg";

                    string path = Path.Join(wwwpath.AsSpan(), "wwwroot".AsSpan(), "images".AsSpan(), file.AsSpan());

                    bm2.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                    return (true, file);
                }
                return (false, null);
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
            
        }
    }
}
