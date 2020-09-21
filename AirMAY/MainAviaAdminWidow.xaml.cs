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
    /// <summary>
    /// Логика взаимодействия для MainAviaAdminWidow.xaml
    /// </summary>
    public partial class MainAviaAdminWidow : Window
    {
        private readonly FlightService _flightService;
        private readonly ChatService _chatService;
        private readonly LoginService _loginService;
        public MainAviaAdminWidow(FlightService flightService, ChatService chatService, LoginService loginService)
        {
            InitializeComponent();

            _flightService = flightService;
            _loginService = loginService;
            _chatService = chatService;
            _chatService.CommandReciveEvent += ReciveMessage;
            _chatService.Start();

            //var button = new Button() { Content = $"Valera", Height = 50, Width =75, FontSize = 18, Foreground = Brushes.White, Background = Brushes.Transparent, BorderBrush = Brushes.Transparent, BorderThickness = new Thickness(0), Tag = "asdasd"};
            //button.Click += SelectChatButton_Click;
            //chatWithUsersListView.Items.Add(button);
        }

        private async Task CreateAndShowMessage(Command command)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Width = chatListView.ActualWidth };
                    stackPanel.Children.Add(new TextBlock() { Text = command.Nickname + ": ", Foreground = Brushes.Coral });
                    stackPanel.Children.Add(new TextBlock() { Text = command.Message });
                    stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                    chatListView.Items.Add(stackPanel);
                });
            });
        }

        private async Task ReciveMessage(Command command)
        {
            if (_chatService.ChatIdList.FirstOrDefault(x => x == command.ChatId) == null)
            {
                _chatService.ChatIdList.Add(command.ChatId);
                var button = new Button() { Content = $"{command.Nickname}", Height = 50, Width = 75, FontSize = 18, Foreground = Brushes.White, Background = Brushes.Transparent, BorderBrush = Brushes.Transparent, BorderThickness = new Thickness(0), Tag = command.ChatId };
                button.Click += SelectChatButton_Click;
                chatWithUsersListView.Items.Add(button);
            }


            if (_chatService.ChatHistories.FirstOrDefault(x => x.ChatId == command.ChatId) == null)
            {
                var chatHistory = new ChatHistory() { ChatId = command.ChatId };
                chatHistory.Messages.Add(command.Nickname + ": " + command.Message);
                _chatService.ChatHistories.Add(chatHistory);
            }
            else
            {
                if (_chatService.CurrentTag == command.ChatId) _chatService.ChatHistories.First(x => x.ChatId == _chatService.CurrentTag).Messages.Add(command.Nickname + ": " + command.Message);
                else _chatService.ChatHistories.First(x => x.ChatId == command.ChatId).Messages.Add(command.Nickname + ": " + command.Message);
            }

            if (_chatService.CurrentTag != null)
            {
                if (_chatService.CurrentTag == command.ChatId)
                {
                    await CreateAndShowMessage(command);
                }
                else
                {
                    chatListView.Items.Clear();

                    //_chatService.ChatHistories.First(x => x.ChatId == command.ChatId).Messages.Add(command.Nickname + ": " + command.Message);
                    var chatHistory = _chatService.ChatHistories.First(x => x.ChatId == command.ChatId).Messages;
                    foreach (var item in chatHistory)
                    {
                        var nickMessage = item.Split(": ");
                        await CreateAndShowMessage(new Command() { Nickname = nickMessage[0], Message = nickMessage[1] });
                    }
                }
            }
            else await CreateAndShowMessage(command);

            _chatService.CurrentTag = command.ChatId;

        }

        private void SelectChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
                _chatService.CurrentTag = button.Tag.ToString();
            chatListView.Items.Clear();

            var chatHistory = _chatService.ChatHistories.First(x => x.ChatId == _chatService.CurrentTag).Messages;

            foreach (var item in chatHistory)
            {
                var nickMessage = item.Split(": ");
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Width = chatListView.ActualWidth };
                stackPanel.Children.Add(new TextBlock() { Text = nickMessage[0] + ": ", Foreground = Brushes.Coral });
                stackPanel.Children.Add(new TextBlock() { Text = nickMessage[1] });
                stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                chatListView.Items.Add(stackPanel);
            }
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
                        stackPanel.Children.Add(new TextBlock() { Text = massageTextBox.Text, TextWrapping = TextWrapping.Wrap });
                        stackPanel.Children.Add(new TextBlock() { Text = DateTime.Now.ToShortTimeString(), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(chatListView.ActualWidth - 20, 0, 0, 0) });

                        chatListView.Items.Add(stackPanel);

                       if(_chatService.CurrentTag != null) _chatService.ChatHistories.First(x => x.ChatId == _chatService.CurrentTag)?.Messages.Add(_loginService.User.Login + ": " + massageTextBox.Text);

                        _chatService.SendCommand(new Command() { Message = massageTextBox.Text, Nickname = _loginService.User.Login, ChatId = _chatService.CurrentTag });
                        massageTextBox.Text = "";
                    }
                    else MessageBox.Show("Введите сообщение");
                });
            });

        }
        private async void AviaButton_Click(object sender, RoutedEventArgs e)
        {
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

        }

        private void ShowChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChatGrid.Visibility == Visibility.Hidden) ChatGrid.Visibility = Visibility.Visible;
            else ChatGrid.Visibility = Visibility.Hidden;
        }

        private void HideChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChatGrid.Visibility == Visibility.Hidden) ChatGrid.Visibility = Visibility.Visible;
            else ChatGrid.Visibility = Visibility.Hidden;
        }

    }
}
