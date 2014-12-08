using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
// THREADING IMPORTS
using System.Diagnostics; // Process
using System.IO; // StreamReader
using System.Data;


namespace Drag_and_Drop
{

    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling=true)]
        public static extern bool FreeConsole();
        #region Collections
        private static ObservableCollection<PhotoData> libraryItems;
        private static ObservableCollection<PhotoData> scatterItems;

        public static ObservableCollection<PhotoData> LibraryItems
        {
            get
            {
                if (libraryItems == null)
                {
                    libraryItems = new ObservableCollection<PhotoData>();
                }

                return libraryItems;
            }
        }

        public ObservableCollection<PhotoData> ScatterItems
        {
            get
            {
                if (scatterItems == null)
                {
                    scatterItems = new ObservableCollection<PhotoData>();
                }

                return scatterItems;
            }
        }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        // ===================================================================================================
        // METHOD: ADD DOWNLOADED IMAGES FROM EVERNOTE TO THE SURFACE APPLICATION
        // ===================================================================================================

        public static void ProcessDirectory(String targetDirectory)
        {
            //LibraryItems.Clear();
            // Process the list of files found in the directory
            string[] fileEntries = Directory.GetFiles(@"C:\Users\Orit\Documents\GitHub\CalvinCalendar\Calvin\" + targetDirectory);
            foreach (string fileName in fileEntries)
            {
                LibraryItems.Add(new PhotoData(fileName, ""));
            }
        }

        // ===================================================================================================
        // METHOD: INITIALIZE SURFACE APPLICATION AND CREATE THREADS
        // ===================================================================================================

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = this;
            AllocConsole();
            Console.WriteLine("About to create a new queuing Thread. Current Thread:" + Thread.CurrentThread.ManagedThreadId);
            Dispatcher.Invoke((Action) delegate()
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                LibraryItems.Add(new PhotoData("Resources/Calvin1.png", ""));
                LibraryItems.Add(new PhotoData("Resources/Calvin2.png", ""));
                LibraryItems.Add(new PhotoData("Resources/Calvin3.png", ""));
                AddWindowAvailabilityHandlers();
                // Start the loop.
                EvernoteBackend();
            });
        }


        // ===================================================================================================
        // BEGIN THREAD CODE
        // ===================================================================================================

        public delegate void DoWorkDelegate(object sender, DoWorkEventArgs e);

        public void EvernoteBackend()
        {
            StartWorker(new DoWorkDelegate(bw_BackendWork));
        }
        public void UpdateFrontend()
        {
            StartWorker(new DoWorkDelegate(bw_FrontendWork));
        }

        public void StartWorker(DoWorkDelegate task)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(task);
            bw.RunWorkerAsync();
            Console.WriteLine("Done with starting the worker");
        }

        private void bw_BackendWork(Object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; (i <= 5); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    Console.WriteLine("Cancelling!");
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Hello from the background worker! Current Thread: " + Thread.CurrentThread.ManagedThreadId);
                    Process p = new Process(); // Create a new process for the python program to run in
                    string pythonFile = @"C:\\Users\\Orit\\Documents\\GitHub\\CalvinCalendar\\python\\test2.py";
                    p.StartInfo = new ProcessStartInfo(@"C:\Python27\python27.exe", pythonFile)
                    {
                        UseShellExecute = false,
                    };
                    p.EnableRaisingEvents = true;
                    Thread.Sleep(20);
                    //SurfaceWindow1.display.Update_Label(o, "hello");
                    p.Start();
                    Console.WriteLine("Started process");
                    //SurfaceWindow1.display.Update_Label(o, Console.ReadLine());
                    Thread.Sleep(40);
                    Console.Write("Thread went to sleep");
                }
            }
            Console.WriteLine("Background work done " + Thread.CurrentThread.ManagedThreadId);
            this.UpdateFrontend();
        }

        private void bw_FrontendWork(Object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Console.WriteLine("Hello from the newer thread!" + Thread.CurrentThread.ManagedThreadId);
            this.Dispatcher.Invoke((Action) (() =>
                SurfaceWindow1.ProcessDirectory("Resources/newImages/")
            ));
            Console.WriteLine("Made it visible!");
            // Comment this back in when you want to make it run infinitely
            //this.EvernoteBackend();
        }

        private static void bw_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Background worker successfully completed.");
        }
        private static void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Inside Progress Changed.  Hello" + e.ProgressPercentage);
        }

        // ===================================================================================================
        // END THREAD CODE
        // ===================================================================================================

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        // ===================================================================================================
        // FRONTEND UI: SURFACE CONTROLS
        // ===================================================================================================

        private void Scatter_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            ScatterViewItem draggedElement = null;

            // Find the ScatterViewitem object that is being touched.
            while (draggedElement == null && findSource != null)
            {
                if ((draggedElement = findSource as ScatterViewItem) == null)
                {
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;
                }
            }

            if (draggedElement == null)
            {
                return;
            }

            PhotoData data = draggedElement.Content as PhotoData;

            // Create the cursor visual
            ContentControl cursorVisual = new ContentControl()
            {
                Content = draggedElement.DataContext,
                Style = FindResource("CursorStyle") as Style
            };

            // Create a list of input devices. Add the touches that
            // are currently captured within the dragged element and
            // the current touch (if it isn't already in the list).
            List<InputDevice> devices = new List<InputDevice>();
            devices.Add(e.TouchDevice);
            foreach (TouchDevice touch in draggedElement.TouchesCapturedWithin)
            {
                if (touch != e.TouchDevice)
                {
                    devices.Add(touch);
                }
            }

            // Get the drag source object
            ItemsControl dragSource = ItemsControl.ItemsControlFromItemContainer(draggedElement);

            SurfaceDragDrop.BeginDragDrop(
                dragSource,                     // The ScatterView object that the cursor is dragged out from.
                draggedElement,                 // The ScatterViewItem object that is dragged from the drag source.
                cursorVisual,                   // The visual element of the cursor.
                draggedElement.DataContext,     // The data attached with the cursor.
                devices,                        // The input devices that start dragging the cursor.
                DragDropEffects.Move);          // The allowed drag-and-drop effects of this operation.

            // Prevents the default touch behavior from happening and disrupting our code.
            e.Handled = true;

            // Hide the ScatterViewItem for now. We will remove it if the DragDrop is successful.
            draggedElement.Visibility = Visibility.Hidden;

            //Threading.Calvin_Thread.Main();
        }

        private void Scatter_DragCanceled(object sender, SurfaceDragDropEventArgs e)
        {
            PhotoData data = e.Cursor.Data as PhotoData;
            ScatterViewItem svi = scatter.ItemContainerGenerator.ContainerFromItem(data) as ScatterViewItem;
            if (svi != null)
            {
                svi.Visibility = Visibility.Visible;
            }
        }

        private void Scatter_DragCompleted(object sender, SurfaceDragCompletedEventArgs e)
        {
            if (e.Cursor.CurrentTarget != scatter && e.Cursor.Effects == DragDropEffects.Move)
            {
                ScatterItems.Remove(e.Cursor.Data as PhotoData);
                e.Handled = true;
            }
        }

        private void Scatter_Drop(object sender, SurfaceDragDropEventArgs e)
        {
            // If it isn't already on the ScatterView, add it to the source collection.
            if (!ScatterItems.Contains(e.Cursor.Data))
            {
                ScatterItems.Add((PhotoData)e.Cursor.Data);
            }

            // Get the ScatterViewItem that Scatter automatically generated.
            ScatterViewItem svi =
                scatter.ItemContainerGenerator.ContainerFromItem(e.Cursor.Data) as ScatterViewItem;
            svi.Visibility = System.Windows.Visibility.Visible;
            svi.Width = e.Cursor.Visual.ActualWidth;
            svi.Height = e.Cursor.Visual.ActualHeight;
            svi.Center = e.Cursor.GetPosition(scatter);
            svi.Orientation = e.Cursor.GetOrientation(scatter);
            svi.Background = Brushes.Transparent;
            // Setting e.Handle to true ensures that default behavior is not performed.
            e.Handled = true;
        }

        private void ListBox_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            SurfaceListBoxItem draggedElement = null;

            // Find the SurfaceListBoxItem object that is being touched.
            while (draggedElement == null && findSource != null)
            {
                if ((draggedElement = findSource as SurfaceListBoxItem) == null)
                {
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;
                }
            }

            if (draggedElement == null)
            {
                return;
            }

            PhotoData data = draggedElement.Content as PhotoData;

            // Create the cursor visual
            ContentControl cursorVisual = new ContentControl()
            {
                Content = draggedElement.DataContext,
                Style = FindResource("CursorStyle") as Style
            };

            // Create a list of input devices. Add the touches that
            // are currently captured within the dragged element and
            // the current touch (if it isn't already in the list).
            List<InputDevice> devices = new List<InputDevice>();
            devices.Add(e.TouchDevice);
            foreach (TouchDevice touch in draggedElement.TouchesCapturedWithin)
            {
                if (touch != e.TouchDevice)
                {
                    devices.Add(touch);
                }
            }

            // Get the drag source object
            ItemsControl dragSource = ItemsControl.ItemsControlFromItemContainer(draggedElement);

            SurfaceDragDrop.BeginDragDrop(
                dragSource,
                draggedElement,
                cursorVisual,
                draggedElement.DataContext,
                devices,
                DragDropEffects.Move);

            // Prevents the default touch behavior from happening and disrupting our code.
            e.Handled = true;

            // Gray out the SurfaceListBoxItem for now. We will remove it if the DragDrop is successful.
            draggedElement.Opacity = 0.5;
        }

        private void ListBox_DragCanceled(object sender, SurfaceDragDropEventArgs e)
        {
            PhotoData data = e.Cursor.Data as PhotoData;
            SurfaceListBoxItem boxItem = ListBox.ItemContainerGenerator.ContainerFromItem(data) as SurfaceListBoxItem;
            if (boxItem != null)
            {
                boxItem.Opacity = 1.0;
            }
        }

        private void ListBox_DragCompleted(object sender, SurfaceDragCompletedEventArgs e)
        {
            if (e.Cursor.CurrentTarget != ListBox && e.Cursor.Effects == DragDropEffects.Move)
            {
                LibraryItems.Remove(e.Cursor.Data as PhotoData);
                e.Handled = true;
            }
        }

        private void ListBox_Drop(object sender, SurfaceDragDropEventArgs e)
        {
            // If it isn't already on the ListBox, add it to the source collection.
            if (!LibraryItems.Contains(e.Cursor.Data))
            {
                LibraryItems.Add((PhotoData)e.Cursor.Data);
            }

            // Get the SurfaceListBoxItem that ListBox automatically generated.
            SurfaceListBoxItem boxItem =
                ListBox.ItemContainerGenerator.ContainerFromItem(e.Cursor.Data) as SurfaceListBoxItem;
            boxItem.Visibility = System.Windows.Visibility.Visible;
            // Setting e.Handle to true ensures that default behavior is not performed.
            e.Handled = true;
        }
        public void Update_Label(object sender, string s)
        {
            Console.WriteLine("About to update button tread id: " + Thread.CurrentThread.ManagedThreadId);
            this.Dispatcher.Invoke((Action)(() =>
                PythonOutput.Content = s
            ));
            Console.Write("After updating button thread id:" + Thread.CurrentThread.ManagedThreadId);
        }

    }

}