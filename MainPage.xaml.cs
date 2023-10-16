using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Timers;
using Microsoft.Maui;

namespace MauiMicrochartsSample
{
    public partial class MainPage : ContentPage
    { 
        private List<ChartEntry> entries = new List<ChartEntry>();
        private SerialPort serialPort = new SerialPort("COM7", 9600); // Replace "COM7" with your Arduino's COM port
        private System.Timers.Timer timer;

        public MainPage()
        {
            InitializeComponent();

            //const string ArduinoBluetoothTransceiverName = "HC-08";

            //var connector = DependencyService.Get<IBluetoothConnector>();
            ////Gets a list of all connected Bluetooth devices
            //var ConnectedDevices = connector.GetConnectedDevices();
            ////Connects to the Arduino
            //var arduino = ConnectedDevices.FirstOrDefault(d => d == ArduinoBluetoothTransceiverName);
            //connector.Connect(arduino);

            // Open the serial port
            serialPort.Open();

            // Initialize the timer
            timer = new System.Timers.Timer(1000); // Set interval to 1 second (1000 ms)
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Read data from the Arduino
            string data = serialPort.ReadLine();
            int value = int.Parse(data);

            if (entries.Count > 20) // Adjust this value as needed
            {
                entries.RemoveAt(0);
            }
            // Create a new chart entry with the value
            var entry = new ChartEntry(value)
            {
                Label = DateTime.Now.ToString("hh:mm:ss"),
                ValueLabel = value.ToString(),
                Color = SKColor.Parse("#77d065")
            };

            // Add the entry to the list
            entries.Add(entry);

            // Update the chart
            MainThread.BeginInvokeOnMainThread(() =>
            {
                chartView.Chart = new LineChart { Entries = entries, IsAnimated = false };
            });
        }
    }
}
//using Microcharts;
//using Microcharts.Maui;
//using SkiaSharp;
//using System;
//using System.Collections.Generic;
//using System.Timers;

//namespace MauiMicrochartsSample
//{
//    public partial class MainPage : ContentPage
//    {
//        private List<ChartEntry> entries = new List<ChartEntry>();
//        private Random random = new Random();
//        private System.Timers.Timer timer;

//        public MainPage()
//        {
//            InitializeComponent();

//            // Initialize the timer
//            timer = new System.Timers.Timer(1000); // Set interval to 1 second (1000 ms)
//            timer.Elapsed += OnTimerElapsed;
//            timer.Start();
//        }

//        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
//        {
//            // Generate a random number from 0 to 4096
//            var randomNumber = random.Next(0, 4097);

//            if (entries.Count > 20) // Adjust this value as needed
//            {
//                entries.RemoveAt(0);
//            }
//            // Create a new chart entry with the random number
//            var entry = new ChartEntry(randomNumber)
//            {
//                Label = DateTime.Now.ToString("hh:mm:ss"),
//                ValueLabel = randomNumber.ToString(),
//                Color = SKColor.Parse("#77d065")
//            };

//            // Add the entry to the list
//            entries.Add(entry);

//            // Update the chart
//            MainThread.BeginInvokeOnMainThread(() =>
//            {
//                chartView.Chart = new LineChart { Entries = entries, IsAnimated = false };
//            });
//        }
//    }
//}
