using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics; // Process
using System.IO; // StreamReader


namespace Threading
{
    public class Calvin_Thread
    {
        static int i = 0;
        static object o = new object();

        public static void Main()
        {
            Console.WriteLine("Calvin's Main Thread's managed thread id: " + Thread.CurrentThread.ManagedThreadId.ToString());

            // Create a thread by ThreadPool.QueueUserWorkItem.
            ThreadPool.QueueUserWorkItem(new WaitCallback(RunCalvin));


            Console.Read();
        }
        public static void RunCalvin(Object stateInfo)
        {
            lock (o)
            {
                Console.WriteLine("\r\nLaunching Calvin Calendar Window... thread's managed thread id: " + Thread.CurrentThread.ManagedThreadId.ToString());
                ThreadPool.QueueUserWorkItem(new WaitCallback(CheckEvernote));
            }
        }
        static void CheckEvernote(Object stateInfo)
        {
            lock (o)
            {
                Console.WriteLine("\r\nLaunching the evernote checking python script... thread's managed thread id: " + Thread.CurrentThread.ManagedThreadId.ToString());
                Process p = new Process(); // create a new process for the python program to run in
                string pythonFile = @"C:\\Users\\Orit\\Documents\\GitHub\\CalvinCalendar\\python\\tmp.py";
                //p.StartInfo = new ProcessStartInfo("cmd", "python27 " + pythonFile)
                //p.StartInfo = new ProcessStartInfo(@"C:\Python27\python.exe", pythonFile)
                /*{
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };*/
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = @"C:\\Python27\\python27.exe";
                p.StartInfo.Arguments = pythonFile;
                p.Start();
                /*ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = pythonFile;
                start.UseShellExecute = true;
                start.RedirectStandardOutput = true;
                Console.Write("hello - I started");
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                }*/
                Console.WriteLine("made it through starting the process");
                //string output = p.StandardOutput.ReadToEnd();
                //p.WaitForExit();
                //Console.WriteLine(output);
                //Console.ReadLine();
            }
        }
    }
}
