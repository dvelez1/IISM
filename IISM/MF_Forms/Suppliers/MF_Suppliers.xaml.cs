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


namespace IISM.MF_Forms.Suppliers
{
  
    /// Manage Coportation's Master Files
    /// Developer: Dennis R. Vélez / June 2018


    public partial class MF_Suppliers : Window
    {

        private int BsID ;
        private int SPId;

        public MF_Suppliers()
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
            if (!(AllComboBoxFilled()))
            {              
                Supplier_.Create(SPId, txtName.Text, txtAddress.Text, txtPostal.Text, txtPhone.Text, txtEmail.Text, txtFAX.Text, BsID,  GlobalVar.CreateMFAction);
                ResetControls();
            }
            else
            {
                MessageBox.Show("Not All fields were filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        private void frmSuppliers_Initialized(object sender, EventArgs e)
        {
            ResetControls();
        }
        
        //Done DVG 07-10-18
        private void ResetControls()
        {
            cmbID.ItemsSource = null; cmbID2.ItemsSource = null; DeleteTxt();
            BsID = 0; SPId = 0;
            if (Classes.GlobalVar.CreateMFAction)
            {
                lblSPId.Visibility = Visibility.Hidden;
                cmbID.Visibility = Visibility.Hidden;
                cmbID.IsEnabled = false;
                DeleteTxt();
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) { this.Close(); }

            }
            else
            {
                lblSPId.Visibility = Visibility.Visible;
                cmbID.Visibility = Visibility.Visible;
                cmbID.IsEnabled = true;
                if (Supplier_.LoadComboBox(ref this.cmbID)) { this.Close(); }

            }
        }

        public void DeleteTxt()
        {
             CheckControls.ClearTextBox(ref this.txtName, ref this.txtAddress, ref txtPostal, ref txtPhone);
             CheckControls.ClearTextBox(ref this.txtEmail, ref this.txtFAX);
            
        }

        public bool AllComboBoxFilled()
        {
             if (CheckControls.VerifyIfTextboxIsNull(this.txtName, this.txtAddress,  txtPostal,  txtPhone))
            {
                return true;
            }
             if (CheckControls.VerifyIfTextboxIsNull( this.txtEmail,  this.txtFAX))
            {
                return true;
            }

            try
            {
                if (BsID <= 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }

            if (!(GlobalVar.CreateMFAction))
            {

     
                try
                {
                   if(SPId<=0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }

            return false;
        }
        

        private void cmbID2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            try
            {
                BsID = Convert.ToInt32(cmbID2.SelectedValue.ToString());
            }
            catch (Exception)
            {

                BsID = 0;
            }
        }

        private void cmbID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SPId = Convert.ToInt32(cmbID.SelectedValue.ToString());
                cmbID2.ItemsSource = null;
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                Supplier_.LoadWindow(SPId, ref txtName, ref txtAddress, ref txtPostal, ref txtPhone, ref txtEmail, ref txtFAX, ref cmbID2, ref BsID);
                          
            }
            catch (Exception)
            {
                SPId = 0;
            }
            
        }
        

        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataModel.IISM_Dataset.SUPPLIERSDataTable dt = new DataModel.IISM_Dataset.SUPPLIERSDataTable();
                DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter();
                adpt.Fill(dt);

                DataModel.IISM_Dataset.BussinessTypeDataTable dt2 = new DataModel.IISM_Dataset.BussinessTypeDataTable();
                DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
                adpt2.Fill(dt2);


                var qry = from s in dt
                          join bt in dt2 on s.BussinessTypeID equals bt.BussinessTypeID
                          select new
                          {
                              SupplierId = s.SupplierID.ToString("00000"),
                              s.Name,
                              s.Address,
                              s.Postal,
                              s.Phone,
                              s.EMAIL,
                              s.FAX,
                              BussinessTypeID = s.BussinessTypeID.ToString("000"),
                              bt.BussinessDesc
                          };
                Classes.FillDataGridcs._lst.Clear();
                Classes.FillDataGridcs.DataGridName = "Suppliers";
                foreach (var item in qry)
                {
                    Classes.FillDataGridcs.AddElements(item);
                }

                dt.Dispose(); adpt.Dispose(); dt2.Dispose(); adpt2.Dispose();

                IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
                OpenW.Show();
            }
            catch (Exception)
            {

                
            }
            
        }






    }
    
}
