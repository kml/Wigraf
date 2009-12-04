using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// WinGraphviz .NET Wrapper
using Wigraf.WinGraphviz;

using Wigraf.Interfaces;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Wigraf.Views;
using Wigraf.Events;

using Wigraf.Properties;

namespace Wigraf.Presenters
{
    class MainPresenter
    {
        string fileName;

        private readonly IMainView m_view;

        public MainPresenter()
        {
            m_view = new MainView();
            UpdateExamples();
            SetMenuLangugage();

            #region Add handlers to events

            m_view.BrowseClicked += Browse;
            m_view.RefreshClicked += Refresh;
            m_view.PreviewClicked += ShowPreview;
            m_view.GraphvizClicked += Graphviz;
            m_view.WinGraphvizClicked += WinGraphvizClicked;
            m_view.SaveAsClicked += SaveAsClicked;
            m_view.AboutClicked += AboutBoxClicked;
            m_view.CloseClicked += CloseClicked;
            m_view.OpenClicked += OpenClicked;
            m_view.SaveClicked += SaveClicked;
            m_view.NewClicked += NewClicked;

            m_view.LanguageClicked += LanguageClicked;
            m_view.ExampleClicked += ExampleClicked;

            #endregion
        }

        public Form MainForm
        {
            get { return (Form)m_view; }
        }

        public void Show()
        {
            m_view.Show();
        }

        #region Event Handlers

        public void Browse(object sender, EventArgs e)
        {
            Process.Start(ExamplesDirectory);
        }

        public void Refresh(object sender, EventArgs e)
        {
            UpdateExamples();
        }

        public void ShowPreview(object sender, EventArgs e)
        {
            Dot dot;
            try
            {
                dot = new Dot();
            }
            catch (DotAccessException)
            {
                m_view.ShowError(Resources.i18nPreviewAccessErrorCaption, Resources.i18nPreviewAccessErrorTitle);
                return;
            }
            catch (DotInitException)
            {
                m_view.ShowError(Resources.i18nPreviewInitErrorCaption, Resources.i18nPreviewInitErrorTitle);
                return;
            }

            try
            {
                dot.Parse(m_view.Code);

                // Create Preview Presenter
                var preview = new PreviewPresenter();
                preview.Image = dot.Image;
                preview.ShowDialog();
            }
            catch (DotParseException)
            {
                m_view.ShowError(Resources.i18nPreviewParseErrorCaption, Resources.i18nPreviewParseErrorTitle);
            }
        }
        
        public void Graphviz(object sender, EventArgs e)
        {
            Process.Start("http://www.graphviz.org/");
        }

        public void WinGraphvizClicked(object sender, EventArgs e)
        {
            Process.Start("http://wingraphviz.sourceforge.net/wingraphviz/");
        }

        public void SaveAsClicked(object sender, EventArgs e)
        {
            SaveAs();
        }

        public void AboutBoxClicked(object sender, EventArgs e)
        {
            using (var view = new AboutBoxView())
            {
                view.ShowDialog();
            }
        }

        public void CloseClicked(object sender, EventArgs e)
        {
            m_view.Close();
        }

        public void OpenClicked(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog()
            {
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

        public void SaveClicked(object sender, EventArgs e)
        {
            Save();
        }

        public void NewClicked(object sender, EventArgs e)
        {
            fileName = null;
            m_view.Code = String.Empty;
            m_view.Title();
        }

        public void LanguageClicked(object sender, LanguageClickedEventArgs e)
        {
            var item = e.item;

            // Check only selected language
            m_view.CheckLanguageItem(item);

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

            Wigraf.Properties.Settings.Default.Culture = culture;
            Wigraf.Properties.Settings.Default.Save();

            m_view.ShowInfo("Language changed. Restart application and check the result.", "Info");
        }

        public void ExampleClicked(object sender, ExampleClickedEventArgs e)
        {
            OpenExample(e.item.Text);
        }

        #endregion

        #region Helper Methods

        private void SetMenuLangugage()
        {
            m_view.CheckLanguageCulture(Properties.Settings.Default.Culture);
        }

        private void UpdateExamples()
        {
            var ei = new ExamplesInfo(ExamplesDirectory);
            var examples = ei.GetExamples();
            m_view.UpdateMenuExamples(examples);
        }

        public void OpenFile(string path)
        {
            var f = new StreamReader(path);
            m_view.Title(Path.GetFileName(path));
            m_view.Code = f.ReadToEnd();
            m_view.Code = m_view.Code.Replace("\n", Environment.NewLine);
            f.Close();
        }

        public void OpenExample(string fileName)
        {
            if (Directory.Exists(ExamplesDirectory))
            {
                var path = Path.Combine(ExamplesDirectory, fileName);
                if (File.Exists(path))
                {
                    OpenFile(path);
                }
                else
                {
                    m_view.ShowError(Resources.i18nExamplesOpenErrorCaption, Resources.i18nExamplesOpenErrorTitle);
                }
            }
            else
            {
                m_view.ShowError(Resources.i18nExamplesOpenDirectoryErrorCaption, Resources.i18nExamplesOpenDirectoryErrorTitle);
            }
        }

        private string ExamplesDirectory
        {
            get
            {
                return Path.Combine(Application.StartupPath, "Examples");
            }
        }

        private void Save()
        {
            if (fileName != null)
            {
                var f = new StreamWriter(fileName);
                f.Write(m_view.Code);
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
                    f.Write(m_view.Code);
                    f.Close();
                    fileName = dialog.FileName;
                    m_view.Title(Path.GetFileName(dialog.FileName));
                }
            }
        }

        #endregion

    }
}
