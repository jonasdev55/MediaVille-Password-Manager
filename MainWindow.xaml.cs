using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.SQLite;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //check if first time running
            if (!System.IO.File.Exists("defaults.json"))
            {
                FirstTimeConfig firstRun = new FirstTimeConfig();
                firstRun.ShowDialog();
            }


            askForPassword askForPassword = new askForPassword();
            askForPassword.ShowDialog();

            //initialize local database
            if (!System.IO.File.Exists("passwords.db"))
            {
                SQLiteConnection.CreateFile("passwords.db");
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=passwords.db;Version=3;");
                m_dbConnection.Open();
                string sql = "create table passwords (platform varchar(100), username varchar(100), password varchar(100), email varchar(100), phonenumber varchar(100))";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                sql = "insert into passwords (platform, username, password, email, phonenumber) values ('platform', 'username', 'password', 'email', 'phonenumber')";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }

            //populate dataGrid
            SQLiteConnection m_dbConnection2 = new SQLiteConnection("Data Source=passwords.db;Version=3;");
            m_dbConnection2.Open();
            string sql2 = "select * from passwords";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection2);
            SQLiteDataReader reader = command2.ExecuteReader();

            while (reader.Read())
            {
                PasswordDetails passwordDetails = new PasswordDetails(reader["platform"].ToString(), reader["username"].ToString(), reader["password"].ToString(), reader["email"].ToString(), reader["phonenumber"].ToString());
                dataGrid.Items.Add(passwordDetails);
            }

            string sqldel = "delete from passwords where platform = 'platform'";
            SQLiteCommand commanddel = new SQLiteCommand(sqldel, m_dbConnection2);
            commanddel.ExecuteNonQuery();
            m_dbConnection2.Close();
        }

        private void ViewItem(object sender, RoutedEventArgs e)
        {
            //get selected item from database
            PasswordDetails passwordDetails = (PasswordDetails)dataGrid.SelectedItem;

            //open window1 and pass values
            Window1 window1 = new Window1();
            window1.setValues(passwordDetails.Platform, passwordDetails.Username, passwordDetails.Password, passwordDetails.Email, passwordDetails.PhoneNumber);
            window1.ShowDialog();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2(this);
            window2.ShowDialog();
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            DeleteWarning deleteWarning = new DeleteWarning(this);
            deleteWarning.ShowDialog();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit(this);
            edit.ShowDialog();
        }
    }

    public class PasswordDetails
    {
        public string Platform { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public PasswordDetails(string platform, string username, string password, string email, string phoneNumber)
        {
            Platform = platform;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
