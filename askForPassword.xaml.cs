using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for askForPassword.xaml
    /// </summary>
    public partial class askForPassword : Window
    {
        bool isPassCorrect = false;

        public askForPassword()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //check if password is correct
            string enteredPassword = passwordBox.Password;

            string masterPassword = System.IO.File.ReadAllText("defaults.json");
            masterPassword = masterPassword.Substring(masterPassword.IndexOf("masterpassword") + 18);
            masterPassword = masterPassword.Substring(0, masterPassword.IndexOf("\""));

            if (enteredPassword == masterPassword)
            {
                isPassCorrect = true;
                this.Close();
            }
            else
            {
                //show error message
                MessageBox.Show("Incorrect password");
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Check if the window is closing without the correct password
            if (!isPassCorrect)
            {
                e.Cancel = true; // Cancel the window closing
                Environment.Exit(0); // Exit the program
            }
        }
    }
}
