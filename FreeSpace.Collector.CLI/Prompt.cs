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
        private YamlReader yamlReader;
        private FileInspection inspection;
        private List<ScanFile> scanFiles;

        public Prompt()
        {
            inspection = new FileInspection();
            scanFiles = new List<ScanFile>();
            yamlReader = new YamlReader();
        }

        public void Run()
        {
            var done = false;
            while (!done)
            {
                Console.Write("FS> ");
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
                case "show":
                    foreach (var d in yamlReader.SearchDirectories)
                    {
                        Console.WriteLine(" - {0}", d);
                    }
                    break;
                case "large":
                    if (scanFiles.Count == 0) { RunScans();}
                    var largeFiles = from sc in scanFiles where sc.FileScanResult.IsBig orderby sc.FileInformation.Length select sc;
                    printResults(largeFiles);
                    break;
                case "locked":
                    if (scanFiles.Count == 0) { RunScans(); }
                    var lockedFiles = from sc in scanFiles where sc.FileScanResult.IsFileSystemLocked select sc;
                    printResults(lockedFiles);
                    break;
                case "dupe":
                    if (scanFiles.Count == 0) { RunScans(); }
                    var dupeFiles = from sc in scanFiles where sc.FileScanResult.IsPossibleDupe orderby sc.FileScanResult.MD5 select sc;
                    printResults(dupeFiles);
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

        private void printResults(IEnumerable<ScanFile> scanFiles)
        {
            foreach (var s in scanFiles)
            {
                Console.WriteLine("{0}\t{1}", s.FileInformation.Length, s.FileInformation.FullName);
            }
            Console.WriteLine("Count: {0}\n\n", scanFiles.Count());
        }

        private void RunScans()
        {
            foreach (var dir in yamlReader.SearchDirectories)
            {
                RecurseDir(dir);
            }
        }

        private void RecurseDir(string dir)
        {
            var dirInfo = new DirectoryInfo(dir);
            if (!Utility.IsDirectoryLocked(dirInfo.FullName))
            {
                foreach (var f in dirInfo.GetFiles())
                {
                    var scanFile = new ScanFile();
                    scanFile.FileInformation = f;
                    scanFile.FileScanResult = inspection.ScanAndRecord(f);
                    scanFiles.Add(scanFile);
                }

                foreach (var d in dirInfo.GetDirectories())
                {
                    RecurseDir(d.FullName);
                }
            }
        }
    }
}
