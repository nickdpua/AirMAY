using AirMAY.Services;
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

namespace AirMAY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LoginService _loginService;
        public MainWindow() { }
        public MainWindow(LoginService loginService)
        {
            _loginService = loginService;
            InitializeComponent();
        }

        private async void loginButtonInLogin_Click(object sender, RoutedEventArgs e)
        {
            await _loginService.IsLoginInAsync(LoginTextBoxInLogin.Text, PassTextBoxInLogin.Text);
        }

        private void registerButtonInLogin_Click(object sender, RoutedEventArgs e)
        {
            var registerPage = new RegisterPage(_loginService);
            this.Close();
            registerPage.Show();
        }
    }
}
