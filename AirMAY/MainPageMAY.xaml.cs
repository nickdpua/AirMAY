using AirMAY.Domain.Models;
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
            var res = await _flightService.GetAllFlight();
            List<Flight> flights = new List<Flight>();

            foreach (var item in res)
            {
                foreach (var times in item.FlightTimes)
                {
                    flights.Add(new Flight()
                    {
                        Price = item.Price,
                        FirstSity = item.FirstSity,
                        SecondSity = item.SecondSity,
                        FlightTimes = new List<FlightTime>() { times }
                    });
                }
            }
            mainListBox.ItemsSource = flights;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var res = await _flightService.GetAllFlight();
            List<Flight> flights = new List<Flight>();

            foreach (var item in res)
            {
                foreach (var times in item.FlightTimes)
                {
                    flights.Add(new Flight()
                    {
                        Price = item.Price,
                        FirstSity = item.FirstSity,
                        SecondSity = item.SecondSity,
                        FlightTimes =new List<FlightTime>() { times }
                    });
                }
            }
            mainListBox.ItemsSource = flights;
        }

        private void HistoryClick(object sender, RoutedEventArgs e)
        {

        }
    }
}