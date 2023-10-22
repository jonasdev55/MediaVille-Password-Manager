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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        MainWindow MainWindow;
        public Edit(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;

            //Set textbox values
            PasswordDetails passwordDetails = (PasswordDetails)MainWindow.dataGrid.SelectedItem;
            platform.Text = passwordDetails.Platform;
            username.Text = passwordDetails.Username;
            password.Text = passwordDetails.Password;
            email.Text = passwordDetails.Email;
            phonenumber.Text = passwordDetails.PhoneNumber;
        }

        private void SaveItem(object sender, RoutedEventArgs e)
        {
            //save to database than update dataGrid use connection from mainwindows
            PasswordDetails passwordDetails = (PasswordDetails)MainWindow.dataGrid.SelectedItem;
            MainWindow.dataGrid.Items.Remove(MainWindow.dataGrid.SelectedItem);

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=passwords.db;Version=3;");
            m_dbConnection.Open();

            string sql = $"update passwords set platform = '{platform.Text}', username = '{username.Text}', password = '{password.Text}', email = '{email.Text}', phonenumber = '{phonenumber.Text}' where platform = '{passwordDetails.Platform}'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();

            passwordDetails = new PasswordDetails(platform.Text, username.Text, password.Text, email.Text, phonenumber.Text);
            MainWindow.dataGrid.Items.Add(passwordDetails);
            this.Close();
        }
    }
}
