using System.Drawing;
using System.Drawing.Imaging;

namespace ergo_web2_2023.Helpers
{
    public class ImageHelper
    {

        public string GetMimeType(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                using (var img = Image.FromStream(ms))
                {
                    return getImageType(img.RawFormat);


                }


            }
        }

        private string getImageType(ImageFormat img)
        {
            if (ImageFormat.Jpeg.Equals(img))
            {
                return "data:image/jpeg;base64,{0}";
            }
            else if (ImageFormat.Gif.Equals(img))
            {
                return "data:image/png;base64,{0}";
            }
            else if (ImageFormat.Png.Equals(img))
            {
                return "data:image/png;base64,{0}";
            }
            else if (ImageFormat.Bmp.Equals(img))
            {
                return "data:image/bmp;base64,{0}";
            }
            else if (ImageFormat.Tiff.Equals(img))
            {
                return "data:image/tiff;base64,{0}";
            }
            else
            {
                return "data:image/jpeg;base64,{0}";
            }


        }



    }
}
