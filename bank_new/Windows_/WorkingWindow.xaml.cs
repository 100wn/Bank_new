using bank_new.Page_;
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

namespace bank_new.Windows_
{
    /// <summary>
    /// Логика взаимодействия для WorkingWindow.xaml
    /// </summary>
    public partial class WorkingWindow : Window
    {
        public WorkingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Authorization());
        }

        private void collapseButton_MouseMove(object sender, MouseEventArgs e)
        {
            collapseButton.Background = (Brush)new BrushConverter().ConvertFromString("#FFE2E2E2");

        }

        private void collapseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            collapseButton.Background = (Brush)new BrushConverter().ConvertFromString("#0003A9F4");
        }

        private void expandButton_MouseLeave(object sender, MouseEventArgs e)
        {
            expandButton.Background = (Brush)new BrushConverter().ConvertFromString("#0003A9F4");
        }

        private void expandButton_MouseMove(object sender, MouseEventArgs e)
        {
            expandButton.Background = (Brush)new BrushConverter().ConvertFromString("#FFE2E2E2");
        }

        private void closeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            closeButton.Background = (Brush)new BrushConverter().ConvertFromString("#0003A9F4");
        }

        private void closeButton_MouseMove(object sender, MouseEventArgs e)
        {
             closeButton.Background = (Brush)new BrushConverter().ConvertFromString("#FFFF6C6C");

        }
        private void TopPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                expandButtonImage.Source = new BitmapImage(new Uri("C:\\Users\\elise\\source\\repos\\bank_new\\bank_new\\Resources\\icons8-кнопка-развернуть-32.png", UriKind.Absolute));


                WindowState = WindowState.Normal;
            }
            else
            {
                expandButtonImage.Source = new BitmapImage(new Uri("C:\\Users\\elise\\source\\repos\\bank_new\\bank_new\\Resources\\icons8-восстановление-32.png", UriKind.Absolute));

                WindowState = WindowState.Maximized;
            }
        }

        private void collapseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
