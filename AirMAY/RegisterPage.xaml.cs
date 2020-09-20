﻿using AirMAY.Domain.Models;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        private readonly LoginService _loginService;
        private readonly FlightService _flightService;
        public RegisterPage(LoginService loginService, FlightService flightService)
        {
            InitializeComponent();

            _loginService = loginService;
            _flightService = flightService;
        }

        private async void loginButtonInRegister_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Login = LoginTextBoxInRegister.Text;
            user.Password = PassTextBoxInRegister.Text;
            user.Name = NameTextBoxInRegister.Text;
            user.Surname = SurnameTextBoxInRegister.Text;
            user.Email = EmailTextBoxInRegister.Text;
            if (await _loginService.IsLoginInAsync(user.Login, user.Password)) MessageBox.Show("Такой логин уже существует");
            else
            {
                await _loginService.RegisterIn(user);

                MainPageMAY mainPageMAY = new MainPageMAY(_flightService);
                this.Close();
                mainPageMAY.Show();
            }
        }
    }
}
