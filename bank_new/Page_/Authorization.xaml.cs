using APIBank.Model;
using bank_new.Windows_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace bank_new.Page_
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private async void entranceButton_Click(object sender, RoutedEventArgs e)
        {
            string numberPhone = numberPhoneTextBox.Text;
            string pass = passPasswordBox.Password;
            if (string.IsNullOrEmpty(numberPhone) || string.IsNullOrEmpty(pass))
            {
                App.MessageClass.ShowError("Заполните все поля");
                return;
            }
            var httpClient = new HttpClient();
            var url = $"http://localhost:5082/api/Authorization/Authorization?userNumberPhone={numberPhone}&password={pass}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
 
            if (response.IsSuccessStatusCode)
            {
                int userRole = int.Parse(await response.Content.ReadAsStringAsync());
                var window = (WorkingWindow)Window.GetWindow(this);
                window.MainFrame.Navigate(new IdentityVerification(numberPhone,userRole));
            }
            else
            {
                App.MessageClass.ShowError("Неверный логин или пароль");
            }

            //var user = httpClient.GetFromJsonAsync<User>($"http://localhost:5082/api/Authorization/Authorization?userNumberPhone={numberPhone}&password={pass}").Result;
            //if (user != null)
            //{
            //    var window = (WorkingWindow)Window.GetWindow(this);
            //    window.MainFrame.Navigate(new IdentityVerification(numberPhone));
            //}
            //else
            //{
            //    App.MessageClass.ShowError("Неверный логин или пароль");
            //}
        }

        private void regAccButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (WorkingWindow)Window.GetWindow(this);
            window.MainFrame.Navigate(new Registration());
        }
    }
}
