using System;
using System.Threading;
using System.Collections.Generic;

namespace IntThreat
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "";
            if (args.Length == 3 && args[1] == "-f")
            {
                file = args[2];
            }
            if (args.Length != 0 && (args[0] == "-h" || args[0] == "help"))
            {
                Console.WriteLine("Use '-h' or 'help' for this menu");
                Console.WriteLine("Use '-s' to run a single test");
                Console.WriteLine("Use '-i' to run a test for each number of threads until given number");
                Console.WriteLine("Use '-f' followed by file name to save result to given file. Must follow '-i' or '-s'");
            }
            else if (args.Length == 0 || args[0] == "-s")
            {
                int count = AskNrThreads();
                Console.Write("Time for " + count + " threads: ");
                TimeSpan duration = Intrun(count);
                if (file == "")
                    Console.WriteLine(duration);
                else
                {
                    System.IO.File.WriteAllText(file, count + ". " + duration + "\n");
                    Console.WriteLine("Results exported to " + file);
                }
            }
            else if (args.Length != 0 && args[0] == "-i")
            {
                int count = AskNrThreads();
                if (file == "")
                    for (int i = 1; i <= count; ++i)
                    {
                        Console.Write(i + " threads: ");
                        Console.WriteLine(Intrun(i));
                    }
                else
                {
                    System.IO.File.WriteAllText(file, "");
                    for (int i = 1; i <= count; ++i)
                        System.IO.File.AppendAllText(file, i + ". " + Intrun(i) + "\n");
                    Console.WriteLine("Results exported to " + file);
                }
            }
        }

        public static int AskNrThreads()
        {
            Console.Write("Choose number of threads: ");
            string input = Console.ReadLine();
            int count;
            try { count = int.Parse(input); }
            catch { throw new InvalidCastException("Incorrect input, \"" + input + "\" is not a number."); }
            return count;
        }
        public static TimeSpan Intrun(int count)
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
        public static void onerun()
        {
            int counter = 0;
            while (++counter != 0) ;
        }
    }
}
