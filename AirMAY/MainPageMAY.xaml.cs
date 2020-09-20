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
        private readonly ChatService _chatService;
        private readonly LoginService _loginService;
        public MainPageMAY(FlightService flightService, ChatService chatService, LoginService loginService)
        {
            InitializeComponent();

            _flightService = flightService;
            _chatService = chatService;
            _loginService = loginService;
        }

        private async void AviaButton_Click(object sender, RoutedEventArgs e)
        {
            mainListBox.ItemsSource = await _flightService.GetAllFlight();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChatGrid.Visibility == Visibility.Hidden) ChatGrid.Visibility = Visibility.Visible;
            else ChatGrid.Visibility = Visibility.Hidden;
        }

        private void HideChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChatGrid.Visibility == Visibility.Hidden) ChatGrid.Visibility = Visibility.Visible;
            else ChatGrid.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _chatService.Start();
            if (_loginService.User.Login == "Admin")
                _chatService.SendCommand(new Services.Model.Command() { Nickname = _loginService.User.Login, UserStatus = "Admin" });
        }
    }
}