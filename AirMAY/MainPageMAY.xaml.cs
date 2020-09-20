using AirMAY.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirMAY
{
    /// <summary>
    /// Interaction logic for MainPageMAY.xaml
    /// </summary>
    public partial class MainPageMAY : Window
    {
        private readonly FlightService _flightService;
        public MainPageMAY(FlightService flightService)
        {
            InitializeComponent();

            _flightService = flightService;
        }

        private async void AviaButton_Click(object sender, RoutedEventArgs e)
        {
            mainListBox.ItemsSource = await _flightService.GetAllFlight();
        }
    }
}
