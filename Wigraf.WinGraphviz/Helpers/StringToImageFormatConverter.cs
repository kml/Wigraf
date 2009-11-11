using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace Wigraf.WinGraphviz.Helpers
{
    public class StringToImageFormatConverter
    {
        // String to ImageFormat
        public ImageFormat Convert(string value)
        {
            string format = value as string;
            ImageFormat img;

            switch (format)
            {
                case "bmp":
                    img = ImageFormat.Bmp;
                    break;
                case "emf":
                    img = ImageFormat.Emf;
                    break;
                case "exif":
                    img = ImageFormat.Exif;
                    break;
                case "gif":
                    img = ImageFormat.Gif;
                    break;
                case "ico":
                    img = ImageFormat.Icon;
                    break;
                case "jpeg":
                    img = ImageFormat.Jpeg;
                    break;
                case "png":
                    img = ImageFormat.Png;
                    break;
                case "tiff":
                    img = ImageFormat.Tiff;
                    break;
                case "wmf":
                    img = ImageFormat.Wmf;
                    break;
                default:
                    img = ImageFormat.Png;
                    break;
            }

            return img;
        }

        // ImageFormat to String
        public string ConvertBack(ImageFormat value)
        {
            var img = (ImageFormat)value;

            if (img == ImageFormat.Bmp)
            {
                return "bmp";
            }
            else if (img == ImageFormat.Emf)
            {
                return "emf";
            }
            else if (img == ImageFormat.Exif)
            {
                return "exif";
            }
            else if (img == ImageFormat.Gif)
            {
                return "gif";
            }
            else if (img == ImageFormat.Icon)
            {
                return "ico";
            }
            else if (img == ImageFormat.Jpeg)
            {
                return "jpeg";
            }
            else if (img == ImageFormat.Png)
            {
                return "png";
            }
            else if (img == ImageFormat.Tiff)
            {
                return "tiff";
            }
            else if (img == ImageFormat.Wmf)
            {
                return "wmf";
            }

            return "png";
        }

    }
}
