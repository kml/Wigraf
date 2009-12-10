using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Wigraf.WinGraphviz.Helpers;
using Wigraf.Interfaces;
using Wigraf.Views;

using System.Drawing;

namespace Wigraf.Presenters
{
    public class PreviewPresenter
    {
        private readonly IPreviewView m_view;

        public PreviewPresenter()
        {
            m_view = new PreviewView();
            m_view.SaveClicked += SaveClicked;
            m_view.SizeNormalClicked += m_view_SizeNormalClicked;
            m_view.SizeZoomClicked += m_view_SizeZoomClicked;
        }

        void m_view_SizeZoomClicked(object sender, EventArgs e)
        {
            m_view.Picture.SizeMode = PictureBoxSizeMode.Zoom;
        }

        void m_view_SizeNormalClicked(object sender, EventArgs e)
        {
            m_view.Picture.SizeMode = PictureBoxSizeMode.Normal;
        }

        public void ShowDialog()
        {
            m_view.ShowDialog();
        }

        public Image Image
        {
            get { return m_view.Image; }
            set { m_view.Image = value; }
        }

        public void SaveClicked(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog()
            {
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
                    m_view.Image.Save(dialog.FileName, format);
                }
            }
        }
    }
}
