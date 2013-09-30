using System;
using System.Collections.Generic;
using System.IO;

namespace JsBuild.Core
{
    public class Configuration
    {
        string directory;
        public string Directory
        {
            get { return directory; }
            set { directory = value; }
        }

        private string outFile;

        public string OutFile
        {
            get { return Path.Combine(directory, outFile); }
            set { outFile = value; }
        }


        List<string> files;
        public List<string> Files
        {
            get { return files; }
            protected set { files = value; }
        }

        public IEnumerable<string> FullFiles
        {
            get
            {
                foreach (string file in files)
                {
                    string full = Path.Combine(this.Directory, file);
                    if (File.Exists(full))
                        yield return full;
                }
            }
        }

        List<IWorker> workers;
        public List<IWorker> Workers
        {
            get { return workers; }
            protected set { workers = value; }
        }

        public Configuration()
        {
            Reset();
        }

        void Reset()
        {
            directory = Environment.CurrentDirectory;
            files = new List<string>();
            workers = new List<IWorker>();
        }

        string ParsePath(string path, string configDir)
        {
            string result = path.Replace("$(ConfigDir)", configDir);

            while (result.Contains("..\\"))
            {
                int i = result.IndexOf("..\\");
                int j = result.LastIndexOf('\\', result.LastIndexOf('\\', i)-1);

                result = result.Remove(j + 1, i - j + 3);
            }

            return result;
        }

        public bool Load(string path)
        {
            this.Reset();

            var fi = new FileInfo(path);
            if (!fi.Exists)
            {
                throw new FileNotFoundException("File not found", path);
            }

            string currentDir = fi.DirectoryName;

            using (var sr = fi.OpenText())
            {
                string line;
                string section = "";

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(line) ||
                        line.StartsWith("//"))
                        continue;

                    if (line.EndsWith(":"))
                    {
                        section = line.Remove(line.Length - 1).ToLower();
                    }
                    else
                    {
                        switch (section)
                        {
                            case "workers":
                                {
                                    string worker = line.ToLower();

                                    switch (worker)
                                    {
                                        case "join":
                                        case "joiner":
                                            {
                                                workers.Add(new Joiner());
                                            }
                                            break;
                                        case "min":
                                        case "minify":
                                        case "minifier":
                                            {
                                                workers.Add(new Minifier());
                                            }
                                            break;
                                    }
                                }
                                break;

                            case "in":
                                {
                                    try
                                    {
                                        string dir = ParsePath(line, currentDir);
                                        if (System.IO.Directory.Exists(dir))
                                            this.Directory = dir;
                                    }
                                    catch { }
                                }
                                break;

                            case "out":
                                {
                                    this.OutFile = line;
                                }
                                break;

                            case "files":
                                {
                                    files.Add(line);
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }

                sr.Close();
            }

            if (files.Count > 0)
            {
                if(workers.Count == 0)
                    workers.Add(new Joiner());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
