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

namespace IISM.MF_Forms.UnlockApp.WPF_Unlock
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string user;
            string pswd;
        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Unlock_Btn_Click(object sender, RoutedEventArgs e)
        {

            this.user = this.txtName.Text;
            this.pswd = this.passwordbox_NAME.Password;

            if (this.user == "TempUnlock")
            {

                if(this.pswd=="TempUnlock_KE")
                {
                    IISM.MainWindow OpenW = new IISM.MainWindow();
                    OpenW.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.passwordbox_NAME.Password = null;
                }

            }
            else
            {
                MessageBox.Show("Incorrect UserName.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.passwordbox_NAME.Password = null;
                this.txtName.Text = null;
            }



        }

        private void Exit_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.txtName.Text = null;
        }
    }
}
