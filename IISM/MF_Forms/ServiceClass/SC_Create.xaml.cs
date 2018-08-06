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
using IISM.Classes;


namespace IISM.MF_Forms.ServiceClass
{
    /// <summary>
    /// Interaction logic for ServiceClass.xaml
    /// </summary>
    public partial class SC_Create : Window
    {
        public SC_Create()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show(); this.Close();
                       
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckControls.VerifyIfTextboxIsNull(this.txtName))
            {
                Close();
            }
            Classes.ServiceClass.Create(this.txtName.Text);
            CheckControls.ClearTextBox(ref txtName);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CheckControls.ClearTextBox(ref txtName);
        }
    }
}
