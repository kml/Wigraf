using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using System.Globalization;

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
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Culture))
            {
                SetCultureInfo(Properties.Settings.Default.Culture);
            }

            InitializeComponent();

            switch (Properties.Settings.Default.Culture)
            {
                case "pl-PL":
                    mnuLanguagePolish.Checked = true;
                    break;
                case "en-US":
                    mnuLanguageEnglish.Checked = true;
                    break;
                default:
                    mnuLanguageDefault.Checked = true;
                    break;
            }

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

            mnuLanguageDefault.Click += new EventHandler(Language_Click);
            mnuLanguageEnglish.Click += new EventHandler(Language_Click);
            mnuLanguagePolish.Click  += new EventHandler(Language_Click);
        }

        // TODO: for item in languages do ... end ;]
        void UncheckLanguageMenuItems()
        {
            mnuLanguageDefault.Checked = false;
            mnuLanguageEnglish.Checked = false;
            mnuLanguagePolish.Checked  = false;
        }

        void Language_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            // Check only selected language
            UncheckLanguageMenuItems();
            item.Checked = true;

            string culture;

            switch (item.Text)
            {
                case "Polish (Polski)":
                    culture = "pl-PL";
                    break;
                case "English":
                    culture = "en-US";
                    break;
                default:
                    culture = "";
                    break;
            }

            Properties.Settings.Default.Culture = culture;
            Properties.Settings.Default.Save();

            MessageBox.Show("Language changed. Restart application and check the result.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateMenuExamples()
        {            
            examplesToolStripMenuItem.DropDownItems.Clear();

            var browse = new ToolStripMenuItem(Resources.i18nBrowseDirectory);
            browse.Click += new EventHandler(browse_Click);

            var refresh = new ToolStripMenuItem(Resources.i18nRefresh);
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
                    MessageBox.Show(Resources.i18nExamplesOpenErrorCaption, Resources.i18nExamplesOpenErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Resources.i18nExamplesOpenDirectoryErrorCaption, Resources.i18nExamplesOpenDirectoryErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //txtCode.Text = txtCode.Text.Replace("\r\n", Environment.NewLine);
            txtCode.Text = txtCode.Text.Replace("\n", Environment.NewLine);

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
            catch (DotAccessException)
            {
                MessageBox.Show(Resources.i18nPreviewAccessErrorCaption, Resources.i18nPreviewAccessErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (DotInitException)
            {
                MessageBox.Show(Resources.i18nPreviewInitErrorCaption, Resources.i18nPreviewInitErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(Resources.i18nPreviewParseErrorCaption, Resources.i18nPreviewParseErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private static void SetCultureInfo(string culture)
        {
            // culture could be "en-US", "de-DE", "pl-PL" etc...
            CultureInfo myCultureInfo = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = myCultureInfo;
            Thread.CurrentThread.CurrentUICulture = myCultureInfo;
        }
    }
}
