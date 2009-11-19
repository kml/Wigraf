using System.Collections.Generic;
using System.IO;

namespace Wigraf.WinGraphviz
{
    public class ExamplesInfo
    {
        private string _path;

        public ExamplesInfo(string path)
        {
            _path = path;
        }

        public List<FileInfo> GetExamples()
        {
            List<FileInfo> _examples = new List<FileInfo>();

            var di = new DirectoryInfo(_path);
            var files = di.GetFiles();

            foreach (var item in files)
            {
                if (item.Extension == ".gv")
                {
                    _examples.Add(item);
                }
            }

            return _examples;
        }

    }
}
