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


namespace IISM.MF_Forms.Customers
{
    /// Manage Coportation's Master Files
    /// Interaction logic for WH_Create.xaml
    /// Developer: Dennis R. Vélez / June 2018
    /// 

         
    public partial class Customer_MF : Window
    {

        private int BsID ;
        private int CustID;

        public Customer_MF()
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

                Customers_.Create(CustID, txtName.Text, txtAddress.Text, txtPostal.Text, txtPhone.Text, txtEmail.Text, txtFAX.Text, BsID, Customers_.IsActive, GlobalVar.CreateMFAction);
                //IISM.MF_Forms.Customers.Customer_MF OpenW = new IISM.MF_Forms.Customers.Customer_MF();
                //OpenW.Show();
                //this.Close();
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

        private void frmCustomerMF_Initialized(object sender, EventArgs e)
        {
            //BsID = 0; CustID = 0;
            //if (Classes.GlobalVar.CreateMFAction)
            //{
            //    lblID.Visibility = Visibility.Hidden;
            //    cmbID.Visibility = Visibility.Hidden;
            //    cmbID.IsEnabled = false;
            //    DeleteTxt();
            //    if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) {this.Close();}
            //    this.chkActive.IsChecked = true; Customers_.IsActive = true;
            //}
            //else
            //{
            //    lblID.Visibility = Visibility.Visible;
            //    cmbID.Visibility = Visibility.Visible;
            //    cmbID.IsEnabled = true;
            //    if (Customers_.LoadComboBox(ref this.cmbID)) { this.Close(); }

            //}

            ResetControls();

        }

        public void ResetControls()
        {
            BsID = 0; CustID = 0;
            cmbID.ItemsSource = null; cmbID2.ItemsSource = null;
            DeleteTxt();
            if (Classes.GlobalVar.CreateMFAction)
            {
                lblID.Visibility = Visibility.Hidden;
                cmbID.Visibility = Visibility.Hidden;
                cmbID.IsEnabled = false;
                DeleteTxt();
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                this.chkActive.IsChecked = true; Customers_.IsActive = true;
            }
            else
            {
                lblID.Visibility = Visibility.Visible;
                cmbID.Visibility = Visibility.Visible;
                cmbID.IsEnabled = true;
                if (Customers_.LoadComboBox(ref this.cmbID)) { this.Close(); }

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
                   if(CustID<=0)
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
        
        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            if (chkActive.IsChecked ?? false)
            {
                if (chkActive.IsChecked == true)
                {
                    Customers_.IsActive = true;
                }
                else
                {
                    Customers_.IsActive = false;
                }
            }
            else
            {
                Customers_.IsActive = false;
            }
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
                
                CustID = Convert.ToInt32(cmbID.SelectedValue.ToString());
                cmbID2.ItemsSource = null;
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                Customers_.LoadWindow(CustID, ref txtName, ref txtAddress, ref txtPostal, ref txtPhone, ref txtEmail, ref txtFAX, ref cmbID2, ref chkActive, ref BsID);

            }
            catch (Exception)
            {

                CustID = 0;
            }
            
        }
        
        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {


            DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
            DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.CustomersDataTable dt2 = new DataModel.IISM_Dataset.CustomersDataTable();
            DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
            adpt2.Fill(dt2);

            var qry = from Cust in dt2
                      join bs in dt on Cust.BussinessTypeID equals bs.BussinessTypeID
                      select new
                      {
                          CustID = Cust.CstnoID.ToString("0000"), Cust.CstName, Cust.Address, Cust.Postal,
                          Cust.Phone, Cust.Email, Cust.Fax,IsActive = Cust.Active, Cust.BussinessTypeID, bs.BussinessDesc
                      };
            Classes.FillDataGridcs._lst.Clear();
            Classes.FillDataGridcs.DataGridName = "Customers";
            foreach (var item in qry)
            {
                Classes.FillDataGridcs.AddElements(item);
            }

            dt.Dispose(); adpt.Dispose(); dt2.Dispose();adpt2.Dispose();

            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();
        }


    }
    
}
