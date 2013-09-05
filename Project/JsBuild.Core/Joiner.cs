using System;
using System.IO;

namespace JsBuild.Core
{
    public class Joiner: IWorker
    {
        #region IWorker Members

        Configuration cfg;

        public void Configure(Configuration cfg)
        {
            this.cfg = cfg;
        }

        public void Run()
        {
            using (StreamWriter sw = new StreamWriter(cfg.OutFile))
            {

                //  ======================
                //  Write Header
                //  ======================

                sw.WriteLine("/*");
                sw.WriteLine();
                sw.WriteLine("Files:");
                foreach (string file in cfg.FullFiles)
                {
                    var fi = new FileInfo(file);
                    sw.WriteLine(fi.Name);
                }
                sw.WriteLine();
                sw.WriteLine("*/");


                //  ======================
                //  Write Content
                //  ======================

                try
                {
                    foreach (string file in cfg.FullFiles)
                    {
                        sw.WriteLine();
                        using (StreamReader sr = new StreamReader(file))
                        {
                            while (!sr.EndOfStream)
                            {
                                sw.WriteLine(sr.ReadLine());
                            }
                            sr.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    sw.Close();
                    throw ex;
                }

            }   //  End of write

        }

        #endregion
    }
}
