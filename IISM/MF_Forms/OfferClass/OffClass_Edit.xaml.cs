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
using IISM.DataModel;
using IISM.Classes;
using System.Data;

namespace IISM.MF_Forms.OfferClass
{
    /// <summary>
    /// Interaction logic for WH_Edit.xaml
    /// </summary>
    public partial class OffClass_Edit : Window
    {

        private int ID;
        IISM.DataModel.IISM_Dataset db = new IISM.DataModel.IISM_Dataset();


        public OffClass_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

             

        private void frmOffClass_Edit_Initialized(object sender, EventArgs e)
        {
            if (OfferClass_.LoadComboBox(ref this.cmbID))
            {
                this.Close();
            }
        }


        private void cmbID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(cmbID.SelectedValue.ToString());
                CheckControls.ClearTextBox(ref this.txtName);
                
                if (OfferClass_.LoadWindow(ID, ref this.txtName,ref this.txtNotes))
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error, please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }


        private void btmDone_Click(object sender, RoutedEventArgs e)
        {
           if( CheckControls.VerifyIfTextboxIsNull(this.txtName))
            {
                this.Close();
            }

            if(OfferClass_.Edit(ID, this.txtName,txtNotes))
            {
                this.Close();
            }
            CheckControls.ClearTextBox(ref this.txtName,ref this.txtNotes);

            IISM.MF_Forms.OfferClass.OffClass_Edit OpenW = new IISM.MF_Forms.OfferClass.OffClass_Edit();
            OpenW.Show();
            this.Close();


        }

    
        private void btmReturn_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show(); this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CheckControls.ClearTextBox(ref this.txtName, ref this.txtNotes);
            IISM.MF_Forms.OfferClass.OffClass_Edit OpenW = new IISM.MF_Forms.OfferClass.OffClass_Edit();
            OpenW.Show();
            this.Close();
        }
    }
}
