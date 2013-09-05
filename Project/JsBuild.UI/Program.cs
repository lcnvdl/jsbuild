using System;
using JsBuild.Core;

namespace JsBuild.UI
{
    class Program
    {
        static void Main(string[] args)
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

            Console.ReadLine();
        }
    }
}
