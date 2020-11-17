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
        /// <exception cref="ConversorException" />
        public static string SaveImage(string wwwpath, string base64String)
        {
            try
            {
                using MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64String));
                using Bitmap bm2 = new Bitmap(ms);

                Guid uuid = System.Guid.NewGuid();
                string filePath = uuid.ToString() + ".jpg";

                string dirPath = Path.Join(wwwpath.AsSpan(), "wwwroot".AsSpan(), "images".AsSpan(), filePath.AsSpan());

                bm2.Save(dirPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                return filePath;
            } catch (Exception e)
            {
                throw new ConversorException(e.Message);
            }
        }
    }

    [Serializable()]
    public class ConversorException : System.Exception
    {
        public ConversorException() : base() { }
        public ConversorException(string message) : base(message) { }
        public ConversorException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected ConversorException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
