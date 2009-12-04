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
        DialogResult ShowDialog();
        event EventHandler SaveClicked;
    }
}
