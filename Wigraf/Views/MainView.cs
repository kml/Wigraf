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

using Wigraf.Presenters;
using Wigraf.Interfaces;
using Wigraf.WinGraphviz;

using Wigraf.Events;
using Wigraf;

namespace Wigraf.Views
{
    public partial class MainView : Form, IMainView
    {
        public delegate void LanguageHandler(object sender, LanguageClickedEventArgs e);
        public delegate void ExampleHandler(object sender, ExampleClickedEventArgs e);

        public event EventHandler BrowseClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler PreviewClicked;
        public event EventHandler GraphvizClicked;
        public event EventHandler WinGraphvizClicked;
        public event EventHandler SaveAsClicked;
        public event EventHandler AboutClicked;
        public event EventHandler CloseClicked;
        public event EventHandler OpenClicked;
        public event EventHandler SaveClicked;
        public event EventHandler NewClicked;

        public event LanguageHandler LanguageClicked;
        public event ExampleHandler ExampleClicked;

        public MainView()
        {
            InitializeComponent();
            //Icon = new Icon(this.GetType(), "Icon.ico");

            #region Events - delegates

            mnuPreview.Click += delegate(object sender, EventArgs e)
            {
                PreviewClicked(this, new EventArgs());
            };

            mnuGraphviz.Click += delegate(object sender, EventArgs e)
            {
                GraphvizClicked(this, new EventArgs());
            };

            mnuWinGraphviz.Click += delegate(object sender, EventArgs e)
            {
                WinGraphvizClicked(this, new EventArgs());
            };

            mnuSaveAs.Click += delegate(object sender, EventArgs e)
            {
                SaveAsClicked(this, new EventArgs());
            };

            mnuAbout.Click += delegate(object sender, EventArgs e)
            {
                AboutClicked(this, new EventArgs());
            };

            mnuClose.Click += delegate(object sender, EventArgs e)
            {
                CloseClicked(this, new EventArgs());
            };

            mnuOpen.Click += delegate(object sender, EventArgs e)
            {
                OpenClicked(this, new EventArgs());
            };

            mnuSave.Click += delegate(object sender, EventArgs e)
            {
                SaveClicked(this, new EventArgs());
            };

            mnuNew.Click += delegate(object sender, EventArgs e)
            {
                NewClicked(this, new EventArgs());
            };

            mnuLanguageDefault.Click += mnuLanguage_Click;
            mnuLanguageEnglish.Click += mnuLanguage_Click;
            mnuLanguagePolish.Click += mnuLanguage_Click;

            #endregion
        }

        // TODO: for item in languages do ... end ;]
        public void CheckLanguageItem(ToolStripMenuItem item)
        {
            mnuLanguageDefault.Checked = false;
            mnuLanguageEnglish.Checked = false;
            mnuLanguagePolish.Checked = false;

            item.Checked = true;
        }

        public void CheckLanguageCulture(string culture)
        {
            switch (culture)
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
        }

        public void UpdateMenuExamples(List<FileInfo> examples)
        {
            examplesToolStripMenuItem.DropDownItems.Clear();

            var browse = new ToolStripMenuItem(Resources.i18nBrowseDirectory);
            browse.Click += delegate(object sender, EventArgs e)
            {
                BrowseClicked(this, new EventArgs());
            };

            var refresh = new ToolStripMenuItem(Resources.i18nRefresh);
            refresh.Click += delegate(object sender, EventArgs e)
            {
                RefreshClicked(this, new EventArgs());
            };

            examplesToolStripMenuItem.DropDownItems.Add(browse);
            examplesToolStripMenuItem.DropDownItems.Add(refresh);

            var sep = new ToolStripSeparator();
            examplesToolStripMenuItem.DropDownItems.Add(sep);

            foreach (var item in examples)
            {
                var mnuItem = new ToolStripMenuItem(item.Name);
                mnuItem.Click += new EventHandler(mnuExample_Click);
                examplesToolStripMenuItem.DropDownItems.Add(mnuItem);
            }
        }

        #region Events

        void mnuExample_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            ExampleClicked(this, new ExampleClickedEventArgs(item));
        }

        void mnuLanguage_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            LanguageClicked(this, new LanguageClickedEventArgs(item));
        }

        #endregion

        #region Messages

        public void ShowError(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        public string Code
        {
            get
            {
                return txtCode.Text;
            }
            set
            {
                txtCode.Text = value;
            }
        }

        public void Title(string title)
        {
            Text = title + " - Wigraf";
        }

        public void Title()
        {
            Text = "Wigraf";
        }
    }
}
