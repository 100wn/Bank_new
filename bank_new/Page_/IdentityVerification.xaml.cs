using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Net.Http;
using System.Net.Http.Json;
using bank_new.Windows_;
using APIBank.Model;

namespace bank_new.Page_
{
    /// <summary>
    /// Логика взаимодействия для IdentityVerification.xaml
    /// </summary>
    public partial class IdentityVerification : Page
    {
        private readonly string numberPhone;
        private readonly int role;
        public IdentityVerification(string numberPhone, int role)
        {
            InitializeComponent();
            this.numberPhone = numberPhone;
            this.role = role;
        }

        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            string cod = codTextBox.Text;
            var httpClient = new HttpClient();
            var url = $"http://localhost:5082/api/Authorization/IdentityVerification?userNumberPhone={numberPhone}&cod={cod}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            var window = (WorkingWindow)Window.GetWindow(this);
            if (response.IsSuccessStatusCode)
            {
                if(role == 1)
                {
                    App.MessageClass.ShowInfo("Добро пожаловать");
                    window.MainFrame.Navigate(new UserMainMenu());
                }

                
                if(role == 2)
                {
                    App.MessageClass.ShowInfo("Добро пожаловать");
                    window.MainFrame.Navigate(new AdminMainMenu());
                }

            }
            else
            {
                App.MessageClass.ShowError("Неверный код безопасности");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (WorkingWindow)Window.GetWindow(this);
            window.MainFrame.Navigate(new Authorization());
        }
    }
}
