using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Wigraf.Interfaces
{
    public interface IMainView
    {
        void Close();
        void Show();
        void ShowError(string text, string caption);
        void ShowInfo(string text, string caption);
        string Code { get; set; }
        void UpdateMenuExamples(List<FileInfo> examples);
        void Title();
        void Title(string title);
        void CheckLanguageItem(ToolStripMenuItem item);
        void CheckLanguageCulture(string culture);

        event EventHandler BrowseClicked;
        event EventHandler RefreshClicked;
        event EventHandler PreviewClicked;
        event EventHandler GraphvizClicked;
        event EventHandler WinGraphvizClicked;
        event EventHandler SaveAsClicked;
        event EventHandler AboutClicked;
        event EventHandler CloseClicked;
        event EventHandler OpenClicked;
        event EventHandler SaveClicked;
        event EventHandler NewClicked;

        event Wigraf.Views.MainView.LanguageHandler LanguageClicked;
        event Wigraf.Views.MainView.ExampleHandler ExampleClicked;
    }
}
