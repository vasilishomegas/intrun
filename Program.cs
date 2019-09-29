using System;
using System.Threading;
using System.Collections.Generic;

namespace IntThreat
{
    class Program
    {
        enum TestMode { None, Single, Increment };
        enum ResultMode { None, Console, File };
        static void Main(string[] args)
        {
            string file = "";
            int threads = 0;
            TestMode testmode = TestMode.None;
            ResultMode resultmode = ResultMode.None;

            // reading user input
            if (args.Length == 0) // default option
            {
                threads = 4;
                testmode = TestMode.Single;
                resultmode = ResultMode.Console;
            }
            for (int i = 0; i < args.Length; ++i) // go through all parameters
            {
                // TODO: add option for console output (explicitly), add option for repeating same test, and appending or overwriting file
                if (args[i] == "-h" || args[i] == "help") // help menu
                {
                    if (i != 0) // help should be first parameter, possibly followed by the parameter you need help with
                    {
                        Console.WriteLine("Invalid command sequence");
                        return;
                    }
                    else if (args.Length == 1) // only display default help menu
                    {
                        Console.WriteLine("Use '-h' or 'help' for help menu");
                        Console.WriteLine("Use '-s' or 'single' to run a single test for give amount of threads");
                        Console.WriteLine("Use '-i' or 'increment' to run a test for each number of threads until given amount of threads");
                        Console.WriteLine("Use '-f' or 'file' to save output in given file.");
                        Console.WriteLine("For more info about a specific command, use '" + args[i] + " <command>'");
                        return;
                    }
                    else if (args[i + 1] == "-s" || args[i + 1] == "single")
                    {
                        Console.WriteLine("This will run a single test for a given number of threads.");
                        Console.WriteLine("Specify the number of threads after the command, for example:");
                        Console.WriteLine("'-s 8'");
                        return;
                    }
                    else if (args[i + 1] == "-i" || args[i + 1] == "increment")
                    {
                        Console.WriteLine("This will run a series of tests, from 1 thread to the give amount of threads.");
                        Console.WriteLine("Specify the number of threads after the command, for example:");
                        Console.WriteLine("'-i 8'");
                        Console.WriteLine("This will run the test first with 1 thread, then 2, and so on \nuntil the specified number is reached (8 in this case)");
                        return;
                    }
                    else if (args[i + 1] == "-f" || args[i + 1] == "file")
                    {
                        Console.WriteLine("This will save the test results in a file instead of printing it to the console.");
                        Console.WriteLine("Specify the file name of the file in which the result needs to be saved.");
                        Console.WriteLine("If the file already exists, it will be overwritten");
                        return;
                    }
                    else // invalid command
                    {
                        Console.WriteLine("'" + args[i + 1] + "' is not a recognised command. \nPlease use '-h' or 'help' without further parameters to see the currently supported commands.");
                        return;
                    }
                }
                else if (args[i] == "-s" || args[i] == "single") // follow this by number of cores and run test once
                {
                    if (args.Length == i + 1)
                    {
                        Console.WriteLine("Missing parameter for '" + args[i] + "'");
                        return;
                    }
                    try
                    {
                        threads = int.Parse(args[i + 1]);
                        testmode = TestMode.Single;
                        ++i;
                    }
                    catch
                    {
                        Console.WriteLine("'" + args[i + 1] + "' is not a valid number of cores.");
                        return;
                    }
                }
                else if (args[i] == "-i" || args[i] == "increment") // follow this by number of cores and test from 1 core to n cores
                {
                    if (args.Length == i + 1)
                    {
                        Console.WriteLine("Missing parameter for '" + args[i] + "'");
                        return;
                    }
                    try
                    {
                        threads = int.Parse(args[i + 1]);
                        testmode = TestMode.Increment;
                        ++i;
                    }
                    catch
                    {
                        Console.WriteLine("'" + args[i + 1] + "' is not a valid number of cores.");
                        return;
                    }
                }
                else if (args[i] == "-f" || args[i] == "file") // follow this by the file path
                {
                    if (args.Length == i + 1)
                    {
                        Console.WriteLine("Missing parameter for '" + args[i] + "'");
                        return;
                    }
                    file = args[i + 1];
                    resultmode = ResultMode.File;
                    ++i;
                }
                else if (args[i] == "-p" || args[i] == "echo")
                {
                    resultmode = ResultMode.Console;
                }
                else // invalid command
                {
                    // maybe just ignore?
                }
            }

            if (testmode == TestMode.None)
            {
                Console.WriteLine("No test mode specified.");
                return;
            }
            if (resultmode == ResultMode.None)
            {
                resultmode = ResultMode.Console;
            }

            // carrying out tests
            if (testmode == TestMode.Single)
            {
                if (resultmode == ResultMode.Console)
                {
                    Console.Write("Time for " + threads + " threads: ");
                    Console.WriteLine(Intrun(threads));
                }
                else if (resultmode == ResultMode.File)
                {
                    Console.WriteLine("Testing for " + threads + " threads");
                    System.IO.File.WriteAllText(file, threads + ". " + Intrun(threads) + "\n");
                    Console.WriteLine("Results exported to " + file);
                }
            }
            else if (testmode == TestMode.Increment)
            {
                if (resultmode == ResultMode.Console)
                {
                    Console.WriteLine("Results per threads:");
                    for (int i = 1; i <= threads; ++i)
                    {
                        Console.Write(i + " threads: ");
                        Console.WriteLine(Intrun(i));
                    }
                }
                else if (resultmode == ResultMode.File)
                {
                    Console.WriteLine("Testing from 1 to " + threads + " threads");
                    System.IO.File.WriteAllText(file, "");
                    for (int i = 1; i <= threads; ++i)
                        System.IO.File.AppendAllText(file, i + ". " + Intrun(i) + "\n");
                    Console.WriteLine("Results exported to " + file);
                }
            }
        }
        public static TimeSpan Intrun(int count) // This is where the magic happens
        {
            List<Thread> threads = new List<Thread>();
            DateTime start, end;
            start = DateTime.Now;
            for (int n = 1; n <= count; n++)
            {
                Thread t = new Thread(onerun);
                threads.Add(t);
                t.Start();
            }
            foreach (Thread t in threads)
                t.Join();
            end = DateTime.Now;
            return (end - start);
        }
        public static void onerun() // This is where the hard work is done
        {
            int counter = 0;
            while (++counter != 0) ;
        }
    }
}
