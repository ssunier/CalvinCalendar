using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics; // Process
using System.IO; // StreamReader
using System.Data;
using System.ComponentModel;

namespace Threading
{
    public class Calvin_Thread
    {
        //static int i = 0;
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
                p.StartInfo = new ProcessStartInfo(@"C:\Python27\python27.exe", pythonFile)
                {
                    UseShellExecute = false,
                    //RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    //CreateNoWindow = true,
                };
                p.EnableRaisingEvents = true;
                p.OutputDataReceived += new DataReceivedEventHandler(OnDataReceived);
                p.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
                Drag_and_Drop.SurfaceWindow1.main.Update_Label(o, "hello"); 
                p.Start();

                Console.Write(Drag_and_Drop.SurfaceWindow1.DataContextProperty); 
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
            }
        }
        public static void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                string temp = (e.Data) + Environment.NewLine;
                // This is where we will bind it with Calvin

            }
        }
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