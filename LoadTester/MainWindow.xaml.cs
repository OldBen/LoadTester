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
using System.Net;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;


namespace LoadTester
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            blkResponse.Text = "Awaiting response...";
            String url = txtUrl.Text;
            int iterations;
            if (txtRequests.Text != "") iterations = int.Parse(txtRequests.Text);
            else iterations = 1;
            float rampUp;
            if (txtRampUp.Text != "") rampUp = float.Parse(txtRampUp.Text) * 1000;
            else rampUp = 1;
            checkResponseTime(url, iterations, rampUp);            
        }

        public async void checkResponseTime(String url, int iterations, float rampUp)
        {
            ConcurrentDictionary<int, TimeSpan> output = new ConcurrentDictionary<int, TimeSpan>();
            String message = "";
            int delay = (rampUp != 0 ? Convert.ToInt32(rampUp / iterations) : 0);
            List<Task> tasks = new List<Task>();
            try {
                for (int i = 0; i < iterations; i++)
                {
                    tasks.Add(Task.Run(() => getRequestResponse(i+1, url, output)));
                    await Task.Delay(delay);
                }
                await Task.WhenAll(tasks);
                TimeSpan totalTime = new TimeSpan();
                TimeSpan maxTime = output[1];
                TimeSpan minTime = output[1];
                foreach (KeyValuePair<int, TimeSpan> result in output)
                {
                    totalTime = totalTime.Add(result.Value);
                    if (result.Value > maxTime) maxTime = result.Value;
                    if (result.Value < minTime) minTime = result.Value;

                }
                ChartWindow chart = new ChartWindow(output);
                chart.Show();   
                TimeSpan avg = new TimeSpan(totalTime.Ticks / iterations);
                message = "Average response time: " + Math.Round(avg.TotalMilliseconds, 2).ToString() + " ms\n";
                message += "Minimal time: " + Math.Round(minTime.TotalMilliseconds, 2).ToString() + " ms\n";
                message += "Maximum time: " + Math.Round(maxTime.TotalMilliseconds, 2).ToString() + " ms\n";
            }
            catch (Exception)
            {
                message = "There was an error. Make sure URL is correct and the site is online.";
            }
                blkResponse.Text = message;
            
        }

        public async Task getRequestResponse(int id, String url, ConcurrentDictionary<int, TimeSpan> output)
        {
            Stopwatch timer = new Stopwatch();
            HttpClient client = new HttpClient();
            timer.Start();
            HttpResponseMessage response = await client.GetAsync(url);
            timer.Stop();
            output.GetOrAdd(id, timer.Elapsed);
        }

        private void NumericTextBoxValidate(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

    



}
