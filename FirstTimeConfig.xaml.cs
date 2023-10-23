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

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for FirstTimeConfig.xaml
    /// </summary>
    public partial class FirstTimeConfig : Window
    {
        public FirstTimeConfig()
        {
            InitializeComponent();
        }

        //save defaults in json under eachother. master password, default email, default phone number
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //save defaults
            System.IO.File.WriteAllText("defaults.json", "{\n\t\"masterpassword\": \"" + MasterPassword.Text + "\",\n\t\"defaultemail\": \"" + DefaultEmail.Text + "\",\n\t\"defaultphonenumber\": \"" + DefaultPhone.Text + "\"\n}");
            this.Close();
        }
    }
}
