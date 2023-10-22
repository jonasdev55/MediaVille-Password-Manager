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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        MainWindow MainWindow;

        public Window2(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void SaveItem(object sender, RoutedEventArgs e)
        {
            //save to database than update dataGrid use connection from mainwindows
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=passwords.db;Version=3;");
            m_dbConnection.Open();

            string sql = "insert into passwords (platform, username, password, email, phonenumber) values ('" + platform.Text + "', '" + username.Text + "', '" + password.Text + "', '" + email.Text + "', '" + phonenumber.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();

            PasswordDetails passwordDetails = new PasswordDetails(platform.Text, username.Text, password.Text, email.Text, phonenumber.Text);
            MainWindow.dataGrid.Items.Add(passwordDetails);
            this.Close();
        }
    }
}
