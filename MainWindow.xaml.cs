using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi;
using Xceed.Wpf.Toolkit;
using System.ComponentModel;
using WindowState = System.Windows.WindowState;

namespace Off_PC
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //
        //dll which helps logout user from system
        [DllImport("user32.dll")]
        public static extern int ExitWindowsEx(int uFlags, int dwReason);


        public MainWindow()
        {
            InitializeComponent();
            rBtn_Logout.IsEnabled = false;

            

            //timing
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += live_Timer;
            LiveTime.Start();

            PutTime.IsEnabled = false;
            Calendar.IsEnabled = false;
                        
        }
      


        void live_Timer(object sender, EventArgs e)
        {
            //timing
            LiveTimeLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }
       
       
	
        private void rBtn_NoSound_Checked(object sender, RoutedEventArgs e)
        {                       
            //PutTime.Value = PutTime.Minimum;
            PutTime.IsEnabled = false;
            Calendar.IsEnabled = false;
        }
        private void AmountOfTimeTo_Checked(object sender, RoutedEventArgs e)
        {
            Calendar.IsEnabled = false;

            if (AmountOfTimeTo.IsChecked == true)
            {
                PutTime.IsEnabled = true;
            }            
        }
        private void SetTheDate_Checked(object sender, RoutedEventArgs e)
        {
            PutTime.IsEnabled = false;                

        if (SetTheDate.IsChecked == true)
            {
                Calendar.IsEnabled = true;
                    
            }
        }
  


    private void Btn_BlockAbort_Checked(object sender, RoutedEventArgs e)
        {
          
            if (Btn_BlockAbort.IsChecked == true)
            {
                btn_Abort.IsEnabled = false;
                Warning.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (Btn_BlockAbort.IsChecked == false)
            {
                Warning.Foreground = new SolidColorBrush(Colors.White);
                btn_Abort.IsEnabled = true;
            }
            
        }

       

        // ------------------ OK--------------------
        public void Button_Click_Ok(object sender, RoutedEventArgs e)
        {       
                      
          //IntegerUpDown [getting value]
            int value = (int)PutTime.Value;
            int sec = value * 60;

            Btn_BlockAbort.IsEnabled = false;
            

            if (value > 0 && AmountOfTimeTo.IsChecked == true)
            {          
                if((bool)rBtn_Shutdown.IsChecked)
                {
                    //that process should shutdown computer and avoid coming up windows
                    var psi = new ProcessStartInfo("shutdown", "/s /f /t " + sec)
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(psi);
                }
                else if ((bool)rBtn_Reboot.IsChecked)
                {
                    //that process should reboot computer and avoid coming up windows
                    var psi = new ProcessStartInfo("shutdown", "/r /f /t" + sec)
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(psi);
                }
                else if ((bool)rBtn_Logout.IsChecked)
                {
                    //logout code which using user32.dll
                    ExitWindowsEx(4, 0);
                }

              


            }
        }

        // ------------------ ABORT --------------------

       

        private void Button_Click_Abort(object sender, RoutedEventArgs e)
        {
           
                
                if ((bool)rBtn_Shutdown.IsChecked)
                {
                    //that process should ABORT shutdown computer and avoid coming up windows
                    var psi = new ProcessStartInfo("shutdown", "/a")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(psi);
                }
                else if ((bool)rBtn_Reboot.IsChecked)
                {
                    //that process should ABORT shutdown computer and avoid coming up windows
                    var psi = new ProcessStartInfo("shutdown", "/a")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(psi);
                }
                else if ((bool)rBtn_Logout.IsChecked)
                {
                    //that process should ABORT shutdown computer and avoid coming up windows
                    var psi = new ProcessStartInfo("shutdown", "/a")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(psi);
                }
            

        }

        private void Btn_BlockAbort_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
