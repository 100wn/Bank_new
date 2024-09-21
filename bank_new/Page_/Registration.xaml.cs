using APIBank.Model;
using bank_new.Windows_;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (WorkingWindow)Window.GetWindow(this);
            window.MainFrame.Navigate(new Authorization());
        }

        private async void entranceButton_Click(object sender, RoutedEventArgs e)
        {
            string numberPhone = numberPhoneTextBox.Text; string email = emailTextBox.Text;
            string pass = passPasswordBox.Password; string fio = $"{surnameTextBox.Text} {nameTextBox.Text} {patronymicTextBox.Text}";
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(numberPhone))
            {
                numberPhoneTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните номер телефона");
            }
            if (string.IsNullOrEmpty(pass))
            {
                passPasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните пароль");
            }
            if (string.IsNullOrEmpty(email))
            {
                emailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните электронную почту");
            }
            if (string.IsNullOrEmpty(birthdateDatePicker.Text))
            {
                birthdateDatePicker.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните дату рождения");
            }
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                nameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните свое имя");
            }
            if (string.IsNullOrEmpty(surnameTextBox.Text))
            {
                surnameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните свое фамилию");
            }
            if (string.IsNullOrEmpty(patronymicTextBox.Text))
            {
                patronymicTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Заполните свое отчество");
            }
            if (birthdateDatePicker.SelectedDate == null)
            {
                birthdateDatePicker.BorderBrush = new SolidColorBrush(Colors.Red);
                errors.AppendLine("Невернный формат даты рождения");
            }
            else
            {
                DateTime birthDate = birthdateDatePicker.SelectedDate.Value;
                int age = DateTime.Now.Year - birthDate.Year;
                if (age < 14)
                {
                    birthdateDatePicker.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors.AppendLine("Вам должно быть не менее 14 лет");
                }
                if (!Regex.IsMatch(numberPhone, @"^\+?\d{10,15}$"))
                {
                    numberPhoneTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors.AppendLine("Неправильный формат номера телефона");
                }
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    emailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors.AppendLine("Неправильный формат адреса электронной почты");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }


                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                DateOnly date = new DateOnly(birthDate.Year, birthDate.Month, birthDate.Day);
                HttpClient client = new HttpClient();
                var url = $"http://localhost:5082/api/Authorization/AddUser?userNumberPhone={numberPhone}&email={email}&fio={fio}&password={pass}&bistday={birthDate.ToString("yyyy-MM-dd")}";
                var response = await client.PostAsync(url,null);

                if (response.IsSuccessStatusCode)
                {
                    App.MessageClass.ShowInfo("Вы успешно зарегистрировались");
                    var window = (WorkingWindow)Window.GetWindow(this);
                    window.MainFrame.Navigate(new Authorization());
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    App.MessageClass.ShowError(error);
                }

            }

        }
    }
}
