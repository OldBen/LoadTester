using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoadTester
{
    /// <summary>
    /// Logika interakcji dla klasy ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        Dictionary <int, int> data;

        public ChartWindow(ConcurrentDictionary<int, TimeSpan> data)
        {
            this.data = new Dictionary<int, int>();
            foreach (KeyValuePair<int, TimeSpan> entry in data) this.data.Add(entry.Key, Convert.ToInt32(entry.Value.TotalMilliseconds));
            InitializeComponent();
            chtChart.DataContext = this.data;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChartWindow1_ContentRendered(object sender, EventArgs e)
        {
        }
    }
}
