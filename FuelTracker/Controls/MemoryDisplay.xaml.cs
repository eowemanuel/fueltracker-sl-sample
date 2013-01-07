using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FuelTracker.Controls
{
    public partial class MemoryDisplay : UserControl
    {
        DispatcherTimer timer;
        public MemoryDisplay()
        {
            InitializeComponent();

            CollectButton.Click += new RoutedEventHandler(CollectButton_Click);     


            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


        }

        void CollectButton_Click(object sender, RoutedEventArgs e)
        {
            CollectMemory();
            GetAndDisplayMemomory();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            GetAndDisplayMemomory();
        }

        protected void CollectMemory()
        {
            GC.Collect();
        }

        protected long GetMemory()
        {
            long memory = GC.GetTotalMemory(false);
            return memory;
        }

        protected void DisplayMemory(double value)
        {
            MemoryValueTextBlock.Dispatcher.BeginInvoke(
                () =>
                {
                    MemoryValueTextBlock.Text = String.Format("{0}", value);
                }
                );
        }

        protected void GetAndDisplayMemomory()
        {
            long memory = GetMemory();
            double memoryInMB = memory / 1024;
            DisplayMemory(memoryInMB);
        }


    }
}
