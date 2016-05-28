using System;
using JsBuild.Core;

namespace JsBuild.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine();
            Console.WriteLine(string.Format("JsBuild v{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            Console.WriteLine("http://www.lucianorasente.com");
            Console.WriteLine();

            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Use: jsbuild config1.txt config2.txt ... configN.txt");
            }
            else
            {

                foreach (string config in args)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Processing " + config);

                        Processor p = new Processor();
                        if (p.Start(config))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Format("Error! {0}", ex.ToString()));
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}
