using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM = System.Configuration.ConfigurationManager;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace FreeSpace.Collector.CLI
{
    public class YamlReader
    {
        private List<string> listDirs;

        public YamlReader()
        {
            listDirs = new List<string>();
        }

        public void Read()
        {
            var yamlPath = CM.AppSettings["yamlFile"];
            var sr = new StreamReader(yamlPath);
            var ys = new YamlStream();
            ys.Load(sr);

            var seqNode = (YamlSequenceNode) ys.Documents[0].RootNode;
            foreach (var entry in seqNode.Children)
            {
                listDirs.Add(entry.ToString());
            }
        }
    }
}
