using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

// WinGraphviz .NET Wrapper
using WinGraphviz;
using WinGraphviz.Exceptions;

namespace Wigraf
{
    public partial class Form1 : Form
    {
        string fileName;

        public Form1(string[] args)
        {
            InitializeComponent();

            // Opening .dot file from command line
            // and windows file extension association
            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    var fi = new FileInfo(args[0]);
                    if (fi.Extension.ToLower() == ".dot")
                    {
                        OpenFile(args[0]);
                    }
                }
            }
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog() { 
                Filter = "Dot File|*.dot",
                DefaultExt = "dot",
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

        private void OpenFile(string fileName)
        {
            var f = new StreamReader(fileName);
            Title(Path.GetFileName(fileName));
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
                using (var dialog = new SaveFileDialog()
                {
                    Filter = "Dot File|*.dot",
                    DefaultExt = "dot",
                    AddExtension = true,
                    RestoreDirectory = true
                })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var f = new StreamWriter(dialog.FileName);
                        f.Write(txtCode.Text);
                        f.Close();
                        Title(Path.GetFileName(dialog.FileName));
                    }
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
            var dot = new Dot();
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
                MessageBox.Show("Błąd parsowania", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        // Najprostsze rozwiązanie.
        // TODO: Sprawdzanie katalogu examples i listowanie plików dot
        private void helloWorlddotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var executableDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            var examplesDirectory = Path.Combine(executableDirectory, "Examples");
            if (Directory.Exists(examplesDirectory))
            {
                var hello = Path.Combine(examplesDirectory, "HelloWorld.dot");
                OpenFile(hello);
            }
        }
            
    }
}
