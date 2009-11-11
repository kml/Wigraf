using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

using Wigraf.WinGraphviz.Helpers;

namespace Wigraf
{
    public partial class Preview : Form
    {
        public Preview()
        {
            InitializeComponent();
        }

        public void Image(Image img)
        {
            picture.Image = img;
        }

        private void mnuPreviewSave_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog() {
                Filter = "BMP File (*.bmp)|*.bmp|EMF File (*.emf)|*.emf|EXIF File (*.exif)|*.exif|GIF File (*.gif)|*.gif|ICON File (*.ico)|*.ico|JPEG File (*.jpeg)|*.jpeg|PNG File (*.png)|*.png|TIFF File (*.tiff)|*.tiff|WMF File (*.wmf)|*.wmf",
                FilterIndex = 7,
                DefaultExt = "png",
                AddExtension = true,
                RestoreDirectory = true
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fi = new FileInfo(dialog.FileName);
                    var ext = fi.Extension.ToLower().Substring(1);
                    var converter = new StringToImageFormatConverter();
                    var format = converter.Convert(ext);
                    picture.Image.Save(dialog.FileName, format);
                }
            }

        }
    }
}
