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
using Wigraf.Interfaces;
using Wigraf.Presenters;

namespace Wigraf.Views
{
    public partial class PreviewView : Form, IPreviewView
    {
        public event EventHandler SaveClicked;
        public event EventHandler SizeNormalClicked;
        public event EventHandler SizeZoomClicked;

        public PreviewView()
        {
            InitializeComponent();

            // OnSave - Save event anonymous method handler
            mnuPreviewSave2.Click += delegate(object sender, EventArgs e)
            {
                SaveClicked(this, new EventArgs());
            };

            mnuSizeNormal.Click += delegate(object sender, EventArgs e)
            {
                SizeNormalClicked(this, new EventArgs());
            };

            mnuSizeZoom.Click += delegate(object sender, EventArgs e)
            {
                SizeZoomClicked(this, new EventArgs());
            };
        }

        public PictureBox Picture
        {
            get
            {
                return picture;
            }
        }

        public Image Image
        {
            get
            {
                return picture.Image;
            }
            set
            {
                picture.Image = value;
            }

        }
    }
}
