using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using WINGRAPHVIZLib;

namespace Wigraf.WinGraphviz
{
    public class Dot
    {
        private readonly DOTClass dot;
        private string _dotCode;
        //private bool _valid;

        public Dot()
        {
            try
            {
                dot = new DOTClass();
            }
            catch (COMException)
            {                
                throw new DotInitException();
            }
            
        }

        public void Parse(string dotCode)
        {
            try
            {
                // TODO: Windows 7 AccessViolationException
                dot.Validate(dotCode); // throws COMException
                _dotCode = dotCode;    
            }
            catch(COMException)
            {
                throw new DotParseException();   
            }
            
        }
                        
        public Image Image
        {
            get
            {
                // generate png image
                var img = dot.ToPNG(_dotCode);

                var imgStream = Base64ToStream(img.ToBase64String());
                return Image.FromStream(imgStream);
            }
        }


        //                if (img == null)
      //          {
     //               MessageBox.Show("Unable to generate to PNG");
    //            }
    //            else
    //            {

        // Based on:
        // Convert Base64 String to Image
        // http://www.dailycoding.com/Posts/convert_image_to_base64_string_and_base64_string_to_image.aspx
        private static MemoryStream Base64ToStream(string base64String)
        {
            // Convert Base64 String to byte[]
            var imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Stream
            ms.Write(imageBytes, 0, imageBytes.Length);
            // Convert Stream to Image
            //Image image = Image.FromStream(ms, true);
            //return image;

            return ms;
        }
    }
}
