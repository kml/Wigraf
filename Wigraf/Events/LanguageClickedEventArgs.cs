using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wigraf.Events
{
    public class LanguageClickedEventArgs : EventArgs
    {
        public ToolStripMenuItem item;

        public LanguageClickedEventArgs(ToolStripMenuItem item)
        {
            this.item = item;
        }
    }
}
