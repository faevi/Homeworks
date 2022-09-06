using System;
using System.Windows.Threading;
using System.Windows;

namespace Clock
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
         
        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                timeNow.Text = DateTime.Now.ToString("HH:mm:ss");
            }, Dispatcher);
        }
        
    }
}
