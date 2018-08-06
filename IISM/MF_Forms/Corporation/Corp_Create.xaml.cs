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


namespace IISM.MF_Forms.Corporation
{
    /// Manage Coportation's Master Files
    /// Interaction logic for WH_Create.xaml
    /// Developer: Dennis R. Vélez / June 2018
    /// 

         
    public partial class Corp_Create : Window
    {

        private int BsID ;
        private int CorpID;

        public Corp_Create()
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

                Corporation_.Create(CorpID, txtName.Text, txtAddress.Text, txtPostal.Text, txtPhone.Text, txtEmail.Text, txtFAX.Text, BsID, Corporation_.IsActive, GlobalVar.CreateMFAction);
                //IISM.MF_Forms.Corporation.Corp_Create OpenW = new IISM.MF_Forms.Corporation.Corp_Create();
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
            DeleteTxt();
            ResetControls();
        }

        private void frmCreateCorp_Initialized(object sender, EventArgs e)
        {
            //BsID = 0; CorpID = 0;
            //if (Classes.GlobalVar.CreateMFAction)
            //{
            //    lblID.Visibility = Visibility.Hidden;
            //    cmbID.Visibility = Visibility.Hidden;
            //    cmbID.IsEnabled = false;
            //    DeleteTxt();
            //    if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) {this.Close();}
            //    this.chkActive.IsChecked = true; Corporation_.IsActive = true;
            //}
            //else
            //{
            //    lblID.Visibility = Visibility.Visible;
            //    cmbID.Visibility = Visibility.Visible;
            //    cmbID.IsEnabled = true;
            //    if (Corporation_.LoadComboBox(ref this.cmbID)) { this.Close(); }

            //}

            ResetControls();

        }



        private void ResetControls()
        {
            DeleteTxt();
            BsID = 0; CorpID = 0;
            cmbID.ItemsSource = null;
            cmbID2.ItemsSource = null;
            if (Classes.GlobalVar.CreateMFAction)
            {
                lblID.Visibility = Visibility.Hidden;
                cmbID.Visibility = Visibility.Hidden;
                cmbID.IsEnabled = false;
                DeleteTxt();
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                this.chkActive.IsChecked = true; Corporation_.IsActive = true;
            }
            else
            {
                lblID.Visibility = Visibility.Visible;
                cmbID.Visibility = Visibility.Visible;
                cmbID.IsEnabled = true;
                if (Corporation_.LoadComboBox(ref this.cmbID)) { this.Close(); }

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
                   if(CorpID<=0)
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
                    Corporation_.IsActive = true;
                }
                else
                {
                    Corporation_.IsActive = false;
                }
            }
            else
            {
                Corporation_.IsActive = false;
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
                CorpID = Convert.ToInt32(cmbID.SelectedValue.ToString());
                cmbID2.ItemsSource = null;
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                Corporation_.LoadWindow(CorpID, ref txtName, ref txtAddress, ref txtPostal, ref txtPhone, ref txtEmail, ref txtFAX, ref cmbID2, ref chkActive, ref BsID);
            }
            catch (Exception)
            {

                CorpID = 0;
            }
            
        }





        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {
            
            DataModel.IISM_Dataset.CorporationDataTable dt = new DataModel.IISM_Dataset.CorporationDataTable();
            DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.BussinessTypeDataTable dt2 = new DataModel.IISM_Dataset.BussinessTypeDataTable();
            DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
            adpt2.Fill(dt2);

            var qry = from corp in dt
                      join bs in dt2 on corp.BussinessTypeID equals bs.BussinessTypeID
                      select new
                      {
                          CorpId = corp.CorpID.ToString("00"), CorpName = corp.CorpDesc,
                          corp.Address, corp.Postal, corp.Phone, corp.Email, corp.Fax, IsActive = corp.Active,
                          BussinessTypeID = corp.BussinessTypeID.ToString("000"), bs.BussinessDesc
                      };
            Classes.FillDataGridcs._lst.Clear();
            Classes.FillDataGridcs.DataGridName = "Corporations";
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
