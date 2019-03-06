using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Media;
using System.Runtime.CompilerServices;
using Sanderling;
using Sanderling.Motor;
using BotEngine.Motor;
using Sanderling.Interface.MemoryStruct;
using WindowsInput;
using WindowsInput.Native;
using WormLib;
using System.Text.RegularExpressions;
using System.Timers;
using Bib3.Geometrik;
using BotEngine.Client;
using BotEngine.Common;
using BotEngine.Common.Motor;
using Window = System.Windows.Window;
using MouseButton = System.Windows.Input.MouseButton;
using Clipboard = System.Windows.Forms.Clipboard;


namespace HelpMeScan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetOpenClipboardWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        private IntPtr? _eveMainWindow;
        private int? _eveClientId;

        public IntPtr? EveMainWindow
        {
            get => _eveMainWindow;
            set => _eveMainWindow = value; 
        }

        public int? EveClientId
        {
            get => _eveClientId;
            set => _eveClientId = value;
        }


        public string ClipString = "{0} {1}";


        public List<String> Players = new List<string>()
        {
            "Studley Maniac",
            "Charles Manson-666",
            "Charlie Manson-666",
            "Lieutenant JG Studley Maniac",
        };

        //private string Results = string.Empty;

        public static void RunAsStaThread(Action goForIt)
        {
            AutoResetEvent @event = new AutoResetEvent(false);
            Thread thread = new Thread(
                () =>
                {
                    goForIt();
                    @event.Set();
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            @event.WaitOne();
        }

       

       
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ///-----------------------------------------------------------------------------
        /// <summary>
        /// Gets the Process that's holding the clipboard
        /// </summary>
        /// <returns>A Process object holding the clipboard, or null</returns>
        ///-----------------------------------------------------------------------------
        public Process ProcessHoldingClipboard()
        {
            Process theProc = null;

            IntPtr hwnd = GetOpenClipboardWindow();

            if (hwnd != IntPtr.Zero)
            {
                uint processId;
                uint threadId = GetWindowThreadProcessId(hwnd, out processId);

                Process[] procs = Process.GetProcesses();
                foreach (Process proc in procs)
                {
                    IntPtr handle = proc.MainWindowHandle;

                    if (handle == hwnd)
                    {
                        theProc = proc;
                    }
                    else if (processId == proc.Id)
                    {
                        theProc = proc;
                    }
                }
            }

            return theProc;
        }




        public MainWindow()
        {
            this.Left = 13.0;
            this.Top = 198.0;
            string testor = Extensions.Julian4();
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();

            //Clipboard.Clear();
            
            EveClientId = Extensions.GetEveOnlineClientProcessId();
            EveMainWindow = Extensions.GetEveMainWindow();
            ComboBox.ItemsSource = Players;
            ComboBox.SelectedIndex = 0;
            //sensor = new Sensor();
            string fmt = $"Inside MainWindow eveClient {EveClientId:X8} eveMainWindow {_eveMainWindow:X8}";
            Log.Debug(fmt);
           
        }


        public string ClientID = $"272ec715901c47a3b555c77ee7ed5e3f";
        public string SecretKey = $"Zf8YKDFe8GbbTBEPu8wpC7CIl513YUXFtZqMu8Dk";
        public string CallBack = $"https://localhost/callback";
        

        private void EveSSO_Click(object sender, RoutedEventArgs e)
        {
            //Log.Debug("inside SSO_Click");
            //System.Timers.Timer tm = new System.Timers.Timer();
            //tm.Interval = 10 * 60_0000; // 10 minutes
            //tm.Elapsed += TimerFired;
            //tm.Start();
        }
     

        private void Classify_OnClick(object sender, RoutedEventArgs e)
        {
            Log.Debug(new StackFrame(0, true));
            Bookmarker bm=new Bookmarker(EveMainWindow.Value,EveClientId.Value);
        }

      
        private void Bookmark_Click(object sender, RoutedEventArgs e)
        {

            Log.Debug(new StackFrame(0, true));
            Bookmarker bm=new Bookmarker(DateTime.Now.ToString("HHmm"));
            
        }
    }
}