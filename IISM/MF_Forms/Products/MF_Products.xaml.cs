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


namespace IISM.MF_Forms.Products
{
  
    /// Manage Product's Master Files
    /// Developer: Dennis R. Vélez / June 2018


    public partial class MF_Products : Window
    {

        private int ProdId ;
        private int CatID;

        public MF_Products()
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
                Products_.Create(ProdId, txtName.Text,System.Convert.ToDecimal(txtPrice.Text),txtDesc.Text,Products_.IsActive,System.Convert.ToDouble(txtIVU.Text), CatID,Convert.ToDecimal( txtCost.Text),  GlobalVar.CreateMFAction);
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

        private void frmProducts_Initialized(object sender, EventArgs e)
        {
            ResetControls();
        }
        

        private void ResetControls()
        {
            ProdId = 0; CatID = 0;
            DeleteTxt();
            cmbID.ItemsSource = null; cmbID2.ItemsSource = null;
            if (Classes.GlobalVar.CreateMFAction)
            {
                lblSPId.Visibility = Visibility.Hidden;
                cmbID.Visibility = Visibility.Hidden;
                cmbID.IsEnabled = false;
                DeleteTxt();
                if (ProductCategory.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                chkActive.IsChecked = true;
                Products_.IsActive = true;
            }
            else
            {
                lblSPId.Visibility = Visibility.Visible;
                cmbID.Visibility = Visibility.Visible;
                cmbID.IsEnabled = true;
                if (Products_.LoadComboBox(ref this.cmbID)) { this.Close(); }

            }
        }

        public void DeleteTxt()
        {
             CheckControls.ClearTextBox(ref this.txtName, ref this.txtPrice, ref txtDesc, ref txtIVU);
            CheckControls.ClearTextBox(ref txtCost);     
        }

        public bool AllComboBoxFilled()
        {
             if (CheckControls.VerifyIfTextboxIsNull(this.txtName, this.txtPrice, txtDesc, txtIVU))
            {
                return true;
            }
            
             if (CheckControls.VerifyIfTextboxIsNull(this.txtCost))
            {
                return true;
            }
             
                        

            if (!(GlobalVar.CreateMFAction)) //Edit
            {

                try
                {
                    if (ProdId <= 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return true;
                }

                try
                {
                   if(CatID<=0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }
            else //Create
            {

                try
                {
                    if (CatID <= 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
                
            }



            if ((CheckControls.IsNumeric(txtPrice.Text)) && (CheckControls.IsNumeric(txtIVU.Text)) && (CheckControls.IsNumeric(txtCost.Text)))
            {
                
            }
            else
            {
                return true;
            }


            return false;
        }

        
        private void cmbID2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            try
            {
                CatID = Convert.ToInt32(cmbID2.SelectedValue.ToString());
            }
            catch (Exception)
            {
                CatID = 0;
            }
        }

        private void cmbID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ProdId = Convert.ToInt32(cmbID.SelectedValue.ToString());
                cmbID2.ItemsSource = null;
                if (ProductCategory.LoadComboBox(ref this.cmbID2)) { this.Close(); }
                Products_.LoadWindow(ProdId,ref txtName, ref txtPrice, ref txtDesc, ref txtIVU, ref cmbID2, ref chkActive, ref CatID,ref txtCost);
            }
            catch (Exception)
            {
                ProdId = 0;
            }
            
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            if (chkActive.IsChecked ?? false)
            {
                if (chkActive.IsChecked == true)
                {
                    Products_.IsActive = true;
                }
                else
                {
                    Products_.IsActive = false;
                }
            }
            else
            {
                Products_.IsActive = false;
            }
        }


        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {
            
            DataModel.IISM_Dataset.ProductsDataTable dt = new DataModel.IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.ProducCategoryDataTable dt2 = new DataModel.IISM_Dataset.ProducCategoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
            adpt2.Fill(dt2);

            var qry = from p in dt
                      join c in dt2 on p.PCatID equals c.PCatID
                      select new
                      {
                        Prodno= p.ProdNoID.ToString("0000"),p.ProdName,p.Price,p.Description,
                        IsActive = p.Active, SalesTax= p.IVU, p.Cost, PCatID = p.PCatID.ToString("000"), c.PCatDesc
                      };
            Classes.FillDataGridcs._lst.Clear();
            Classes.FillDataGridcs.DataGridName = "Products";
            foreach (var item in qry)
            {
                Classes.FillDataGridcs.AddElements(item);
            }

            dt.Dispose(); adpt.Dispose(); dt2.Dispose(); adpt2.Dispose();

            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();
        }


    }
    
}
