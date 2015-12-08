using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace OFClassLibrary.Common.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bitmap"></param>
        /// <param name="quality"></param>
        public static void SaveJPEG(string path, Bitmap bitmap, int quality)
        {
            if (quality < 0 || quality > 100)
            {
                throw new ArgumentOutOfRangeException("Quality", "Quality must be in [0..100].");
            }

            ImageCodecInfo encoder = ImageCodecInfo.GetImageDecoders().Where(c => c.FormatID == ImageFormat.Jpeg.Guid).Single();

            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            bitmap.Save(@path, encoder, parameters);
        }
    }
}
