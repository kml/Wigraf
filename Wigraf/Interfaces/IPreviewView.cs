using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Wigraf.Interfaces
{
    public interface IPreviewView
    {
        Image Image { get; set; }
        PictureBox Picture { get; }
        DialogResult ShowDialog();

        event EventHandler SaveClicked;
        event EventHandler SizeNormalClicked;
        event EventHandler SizeZoomClicked;
    }
}
