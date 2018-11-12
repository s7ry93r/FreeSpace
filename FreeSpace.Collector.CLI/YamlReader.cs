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
        private List<string> searchDirectories;
        //private List<string> skipExtensions;
        //private List<string> skipSubDirectories;

        public YamlReader()
        {
            searchDirectories = new List<string>();

            var yamlPath = CM.AppSettings["yamlFile"];
            var streamReader = new StreamReader(yamlPath);
            var yamlStream = new YamlStream();
            yamlStream.Load(streamReader);

            var mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
            getListItems(searchDirectories, "search", mappingNode);

        }

        public List<string> SearchDirectories
        {
            get { return searchDirectories; }
        }

        private void getListItems(List<string> list, string key, YamlMappingNode mappingNode)
        {
            var items = (YamlSequenceNode) mappingNode.Children[new YamlScalarNode(key)];
            foreach (YamlScalarNode item in items)
            {
                list.Add(item.Value);
            }
        }

    }
}
