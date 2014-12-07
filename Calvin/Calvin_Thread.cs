using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics; // Process
using System.IO; // StreamReader
using System.Data;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Drag_and_Drop
{
    public static partial class Calvin_Thread
    {
        /*[DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();*/
        //static int i = 0;
        static object o = new object();

        /*public static void Main()
        {
            Console.WriteLine("Calvin's Main Thread's managed thread id: " + Thread.CurrentThread.ManagedThreadId.ToString());

            // Create a thread by ThreadPool.QueueUserWorkItem.
            ThreadPool.QueueUserWorkItem(new WaitCallback(RunCalvin));


            Console.Read();
        }*/
        /*public static void RunCalvin(Object stateInfo)
        {
            lock (o)
            {
                Console.WriteLine("\r\nLaunching Calvin Calendar Window... thread's managed thread id: " + Thread.CurrentThread.ManagedThreadId.ToString());
                ThreadPool.QueueUserWorkItem(new WaitCallback(CheckEvernote));
            }
        }*/
        //public StreamReader StandardOutput { get; }
        public static void bw_DoWork(Object sender, DoWorkEventArgs e)
        {
       
            //AllocConsole();
            Console.WriteLine("inside background worker!");
            BackgroundWorker worker = sender as BackgroundWorker;
            
            for (int i = 1; (i <= 100); i++)
            {
                //Console.WriteLine("Hello from the Thread Pool! Thread id: " + Thread.CurrentThread.ManagedThreadId);
                if ((worker.CancellationPending == true))
                {
                    Console.WriteLine("Cancelling!");
                    e.Cancel = true;
                    break;
                }
                else
                {
                    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    //Thread.Sleep(500);
                    worker.ReportProgress((i * 10));
                }
            }
        }
        private static void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Hello" + e.ProgressPercentage);   
                //Thread.Sleep(100);
        
        }
        public static void ThreadTest(Object stateInfo)
        {
            Console.WriteLine("Inside a thread class! Thread id:" + Thread.CurrentThread.ManagedThreadId);
        }
        public static void CheckEvernote(Object stateInfo)
        {
            //lock (o)
            //{
                Console.WriteLine("\r\nLaunching the evernote checking python script... thread's managed thread id: " + Thread.CurrentThread.ManagedThreadId.ToString());
                //StreamReader reader = new StreamReader(Console.ReadLine());
                Process p = new Process(); // create a new process for the python program to run in
                string pythonFile = @"C:\\Users\\Orit\\Documents\\GitHub\\CalvinCalendar\\python\\tmp.py";
                p.StartInfo = new ProcessStartInfo(@"C:\Python27\python27.exe", pythonFile)
                {
                    UseShellExecute = false,
                    //RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    //CreateNoWindow = true,
                };
                p.EnableRaisingEvents = true;
                //p.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);
                //p.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
                Thread.Sleep(20);
                SurfaceWindow1.display.Update_Label(o, "hello");
                p.Start();
                Console.WriteLine("started process");
                SurfaceWindow1.display.Update_Label(o, Console.ReadLine());
                Thread.Sleep(40);
                Console.Write("Thread went to sleep");
                /*using (reader) {
                    //string l = reader.ReadLine();
                    //Console.Write(l);
                    Drag_and_Drop.SurfaceWindow1.main.Update_Label(o, reader.ReadLine());
                }*/
                //Console.Write("Made it!");
                //Drag_and_Drop.SurfaceWindow1.main.Update_Label(o, "Made It"); 
                //Console.WriteLine("made it through starting the process");
                //string output = p.StandardOutput.ReadToEnd();
                //Console.WriteLine(output);
                /*if (output.Equals("this is an image"))
                {
                    Console.WriteLine("Recognized a thing");
                }*/
                //p.WaitForExit();
                //Console.WriteLine(output);
                //Console.ReadLine();
            //}
        }
        /*public static void OutputDataReceived(object sender, DataReceivedEventArgs args)
        {
            //if (args.Data != null)
            //{
                Drag_and_Drop.SurfaceWindow1.main.Update_Label(o, args.Data);
                // This is where we will bind it with Calvin

            //}
        }*/
        /*
        public void appendText(string text)
        {
            if (ResultTextBox.InvokeRequired)
            {
            }
        }*/

        public delegate void UpdateTextCallback(string message);

        /*private void TestThread()
        {
            for (int i = 0; i <= 1000000000; i++)
            {
                Thread.Sleep(1000);
                PythonOutput.
                .Dispatcher.Invoke(
                    new UpdateTextCallback(this.UpdateText),
                    new object[] { i.ToString }
                );
            }
        }*/

        /*private void UpdateText(string message)
        {
            richTextBox1.AppendText(message + '\n');
        }*/

    }
}