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
    /// Interaction logic for DeleteWarning.xaml
    /// </summary>
    public partial class DeleteWarning : Window
    {
        MainWindow MainWindow;

        public DeleteWarning(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            //delete from database than update dataGrid use connection from mainwindows
            PasswordDetails passwordDetails = (PasswordDetails)MainWindow.dataGrid.SelectedItem;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=passwords.db;Version=3;");
            m_dbConnection.Open();

            string sql = $"delete from passwords where platform = '{passwordDetails.Platform}'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();

            MainWindow.dataGrid.Items.Remove(MainWindow.dataGrid.SelectedItem);
            this.Close();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
