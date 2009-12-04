using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wigraf.Events
{
    public class ExampleClickedEventArgs : EventArgs
    {
        public ToolStripMenuItem item;

        public ExampleClickedEventArgs(ToolStripMenuItem item)
        {
            this.item = item;
        }
    }
}
