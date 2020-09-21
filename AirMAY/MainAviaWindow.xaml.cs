using AirMAY.Domain.Models;
using AirMAY.Services;
using AirMAY.Services.Model;
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
using System.Windows.Shapes;

namespace AirMAY
{
    public partial class MainAviaWindow : Window
    {
        private readonly FlightService _flightService;
        private readonly ChatService _chatService;
        private readonly LoginService _loginService;
        public MainAviaWindow(FlightService flightService, ChatService chatService, LoginService loginService)
        {
            InitializeComponent();

            _flightService = flightService;
            _loginService = loginService;
            _chatService = chatService;
            _chatService.CommandReciveEvent += ReciveMessage;
            _chatService.Start();
        }

        private async Task ReciveMessage(Command command)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    _chatService.CurrentTag = command.ChatId;

                    StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Width = chatListView.ActualWidth };
                    stackPanel.Children.Add(new TextBlock() { Text = command.Nickname + ": ", Foreground = Brushes.Coral });
                    stackPanel.Children.Add(new TextBlock() { Text = command.Message });
                    stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                    chatListView.Items.Add(stackPanel);
                });
            });
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (massageTextBox.Text != "")
                    {
                        StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Width = chatListView.ActualWidth };
                        stackPanel.Children.Add(new TextBlock() { Text = _loginService.User.Login + ": ", Foreground = Brushes.Coral });
                        stackPanel.Children.Add(new TextBlock() { Text = massageTextBox.Text });
                        stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                        chatListView.Items.Add(stackPanel);

                        _chatService.SendCommand(new Command() { Message = massageTextBox.Text, Nickname = _loginService.User.Login, ChatId = _chatService.CurrentTag });
                        massageTextBox.Text = "";
                    }
                    else MessageBox.Show("Введите сообщение");
                });
            });

        }
        private async void AviaButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.Visibility = Visibility.Visible;
            mainListView.ItemsSource = await _flightService.GetAllFlight();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainListView.ItemsSource = await _flightService.GetAllFlight();

            _chatService.Start();
            if (_loginService.User.Login == "Admin")
                _chatService.SendCommand(new Services.Model.Command() { Nickname = _loginService.User.Login, UserStatus = "Admin" });
            else _chatService.SendCommand(new Services.Model.Command() { Nickname = _loginService.User.Login, UserStatus = "NoAdmin" });

            await _chatService.ReadCommand();
        }

        private void HistoryClick(object sender, RoutedEventArgs e)
        {
            addButton.Visibility = Visibility.Hidden;
            var ress = _loginService.User.FlightUser;
            List<Flight> flights = new List<Flight>();
            foreach (var item in ress)
                flights.Add(item.Flight);

            mainListView.ItemsSource = flights;
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

        private async void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mainListView.SelectedItem != null)
                {
                    var res = (await _flightService.FindByConditionAsync(x => x.Id == (mainListView.SelectedItem as Flight).Id)).FirstOrDefault();
                    res.FlightUser.Add(new FlightUser() { UserId = _loginService.User.Id, FlightId = res.Id });
                    await _flightService.Change(res);
                }
            }
            catch (Exception)  { MessageBox.Show("Такой билет уже куплен"); }
        }
    }
}