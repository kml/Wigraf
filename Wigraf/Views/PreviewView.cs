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

        public PreviewView()
        {
            InitializeComponent();

            // OnSave - Save event anonymous method handler
            mnuPreviewSave2.Click += delegate(object sender, EventArgs e)
            {
                SaveClicked(this, new EventArgs());
            };
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
