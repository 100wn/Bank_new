using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bank_new.Classes
{
    public class MessageClass
    {
        public MessageBoxResult ShowInfo(string info)
        {
            return MessageBox.Show(info,"Информация", MessageBoxButton.OK);
        }
        public MessageBoxResult ShowError(string error) 
        {
            return MessageBox.Show(error, "Ошибка", MessageBoxButton.OK);
        }
    }
}
