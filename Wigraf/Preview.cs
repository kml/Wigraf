using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
