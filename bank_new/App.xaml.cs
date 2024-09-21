using System.Configuration;
using System.Data;
using System.Windows;
using bank_new.Classes;


namespace bank_new
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MessageClass MessageClass { get; } = new MessageClass();
    }

}
