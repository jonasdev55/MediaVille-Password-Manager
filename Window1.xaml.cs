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
using System.Data.SQLite;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public void setValues(string platform, string username, string password, string email, string phoneNumber)
        {
            Win1platform.Content = platform;
            Win1username.Content = username;
            Win1password.Content = password;
            Win1email.Content = email;
            Win1phone.Content = phoneNumber;
        }
    }
}
