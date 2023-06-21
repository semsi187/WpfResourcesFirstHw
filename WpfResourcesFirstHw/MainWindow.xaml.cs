using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfResourcesFirstHw
{
    public partial class MainWindow : Window
    {
        private void Timer_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();

            processListView.Items.Clear();

            foreach (Process process in processes)
            {
                processListView.Items.Add(new
                {
                    Name = process.ProcessName,
                    ID = process.Id,
                    CPU = process.ProcessName.Length,
                    THREAD = process.Threads.Count,
                    //HANDLE = process.Threads
                });
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void processListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (processListView.SelectedItem != null)
            {
                dynamic selectedProcess = processListView.SelectedItem;
                int processId = selectedProcess.ID;

                try
                {
                    Process processToKill = Process.GetProcessById(processId);
                    processToKill.Kill();
                }
                catch (ArgumentException ex)
                {
                    // İşlem bulunamadı veya sonlandırılamadı hatası
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string programPath = SearchBox.Text;

            if (!string.IsNullOrWhiteSpace(programPath))
            {
                try
                {
                    Process.Start(programPath);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }

        }
    }


}
