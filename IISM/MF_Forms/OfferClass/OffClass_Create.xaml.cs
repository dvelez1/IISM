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


namespace IISM.MF_Forms.OfferClass
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class OffClass_Create : Window
    {
        public OffClass_Create()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
           
            GlobalVar.CallEditForm();
            this.Close();
                       
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckControls.VerifyIfTextboxIsNull(txtName,txtNotes))
            {
                MessageBox.Show("Please, fill all Textboxes!","Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            OfferClass_.Create(this.txtName.Text,txtNotes.Text);
            CheckControls.ClearTextBox(ref txtName, ref txtNotes);
        }

        private void btnCLEAR_Click(object sender, RoutedEventArgs e)
        {
            CheckControls.ClearTextBox(ref this.txtName, ref this.txtNotes);
        }
    }
}
