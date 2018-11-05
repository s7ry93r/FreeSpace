using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using FreeSpace.Lib;
using System.IO;

namespace FreeSpace.Collector.CLI
{

    public class ScanFile
    {
        public FileInfo FileInformation { get; set; }
        public ScanResult FileScanResult { get; set; }
    }


    public class Prompt
    {
        private YamlReader yr;
        private FileInspection inspection;

        private 

        public Prompt()
        {
            yr = new YamlReader();
            yr.Read();
        }

        public void Run()
        {
            var done = false;
            while (!done)
            {
                Console.WriteLine("FS> ");
                var input = Console.ReadLine();
                done = ProcessInput(input);
            }
        }

        private bool ProcessInput(string input)
        {
            var done = false;
            switch (input.ToLower())
            {
                case "scan":
                    RunScans();
                    break;
                case "large":
                    break;
                case "locked":
                    break;
                case "dupe":
                    break;
                case  "exit":
                    done = true;
                    break;
                default:
                    Console.WriteLine("avaiable commands: scan, large, locked, dupe, exit");
                    break;

            }

            return done;
        }
    }
}
