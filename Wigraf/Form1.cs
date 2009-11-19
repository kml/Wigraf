using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;

// WinGraphviz .NET Wrapper
using Wigraf.WinGraphviz;

namespace Wigraf
{
    public partial class Form1 : Form
    {
        string fileName;

        public Form1(string[] args)
        {
            InitializeComponent();
            Icon = new Icon(this.GetType(), "Icon.ico");

            // Opening .dot file from command line
            // and windows file extension association
            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    var fi = new FileInfo(args[0]);
                    if (fi.Extension.ToLower() == ".gv")
                    {
                        OpenFile(args[0]);
                    }
                }
            }

            UpdateMenuExamples();
        }

        private void UpdateMenuExamples()
        {            
            examplesToolStripMenuItem.DropDownItems.Clear();

            var browse = new ToolStripMenuItem("Otwórz katalog");
            browse.Click += new EventHandler(browse_Click);

            var refresh = new ToolStripMenuItem("Odśwież");
            refresh.Click += new EventHandler(refresh_Click);

            examplesToolStripMenuItem.DropDownItems.Add(browse);
            examplesToolStripMenuItem.DropDownItems.Add(refresh);

            var sep = new ToolStripSeparator();
            examplesToolStripMenuItem.DropDownItems.Add(sep);

            var ei = new ExamplesInfo(ExamplesDirectory);
            var examples = ei.GetExamples();

            foreach (var item in examples)
            {
                var mnuItem = new ToolStripMenuItem(item.Name);
                mnuItem.Click += new EventHandler(mnuItem_Click);
                examplesToolStripMenuItem.DropDownItems.Add(mnuItem);
            }
        }

        void mnuItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var fileName = item.Text;

            if (Directory.Exists(ExamplesDirectory))
            {
                var path = Path.Combine(ExamplesDirectory, fileName);
                if (File.Exists(path))
                {
                    OpenFile(path);
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas otwierania przykładu. Nie usunąłeś go?", "Błąd otwierania przykładu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas otwierania katalogu przykłądów. Nie usunąłeś go?", "Katalog przykładów", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void refresh_Click(object sender, EventArgs e)
        {
            UpdateMenuExamples();
            //menuStrip1.Items[0].perf
            //examplesToolStripMenuItem.ShowDropDown();
        }

        void browse_Click(object sender, EventArgs e)
        {
            Process.Start(ExamplesDirectory);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog() { 
                Filter = "Graphviz File|*.gv",
                DefaultExt = "gv",
                AddExtension = true,
                RestoreDirectory = true
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(dialog.FileName);
                }
            }
        }

        private void OpenFile(string path)
        {
            var f = new StreamReader(path);
            Title(Path.GetFileName(path));
            txtCode.Text = f.ReadToEnd();
            f.Close();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                var f = new StreamWriter(fileName);
                f.Write(txtCode.Text);
                f.Close();
            }
            else
            {
                SaveAs();
            }         
        }

        private void SaveAs()
        {
            using (var dialog = new SaveFileDialog()
            {
                Filter = "Graphviz File|*.gv",
                DefaultExt = "gv",
                AddExtension = true,
                RestoreDirectory = true
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var f = new StreamWriter(dialog.FileName);
                    f.Write(txtCode.Text);
                    f.Close();
                    fileName = dialog.FileName;
                    Title(Path.GetFileName(dialog.FileName));
                }
            }
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            fileName = null;
            txtCode.Text = String.Empty;
            Title();
        }

        private void Title(string title)
        {
            Text = title + " - Wigraf";
        }

        private void Title()
        {
            Text = "Wigraf";
        }

        private void mnuPreview_Click(object sender, EventArgs e)
        {
            Dot dot;
            try
            {
                dot = new Dot();
            }
            catch (DotInitException)
            {
                MessageBox.Show("Nie można utworzyć podglądu. Sprawdź, czy WinGraphviz jest na pewno zainstalowany.", "Błąd inicjalizacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                dot.Parse(txtCode.Text);
                using (var p = new Preview())
                {
                    p.Image(dot.Image);
                    p.ShowDialog();
                }
            }
            catch (DotParseException)
            {
                MessageBox.Show("Popraw kod i spróbuj ponownie.", "Błąd parsowania", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void mnuGraphviz_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.graphviz.org/");
        }

        private void mnuWinGraphviz_Click(object sender, EventArgs e)
        {
            Process.Start("http://wingraphviz.sourceforge.net/wingraphviz/");
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private string ExamplesDirectory {
            get
            {
                return Path.Combine(Application.StartupPath, "Examples");
            }
        }            
    }
}
