using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Wigraf.Views;
using Wigraf.Presenters;
using System.IO;

using Wigraf.Properties;
using System.Globalization;
using System.Threading;

namespace Wigraf
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Culture))
            {
                SetCultureInfo(Properties.Settings.Default.Culture);
            }

            var presenter = new MainPresenter();
            Application.Run(presenter.MainForm);

            // Opening .dot file from command line
            // and windows file extension association
            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    var fi = new FileInfo(args[0]);
                    if (fi.Extension.ToLower() == ".gv")
                    {
                        presenter.OpenFile(args[0]);
                    }
                }
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
