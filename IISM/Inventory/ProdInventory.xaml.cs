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


namespace IISM.Inventory
{
  
    /// Manage Product's Inventory
    /// Developer: Dennis R. Vélez / June 2018


    public partial class ProdInventory : Window
    {

        private int ProdId ;
        private int Whid;

        public ProdInventory()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            IISM.Inventory.InventoriesMenu OpenW = new IISM.Inventory.InventoriesMenu();
            OpenW.Show();
            this.Close();
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!(AllComboBoxFilled()))
            {
                
                if (Convert.ToDouble(txtEditQty.Text)==0)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want edit the quantity of the product units to 0?", "Action", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        
                    }
                }

                Classes.ProdInventory.Create(ProdId, Whid, Convert.ToDouble(txtEditQty.Text));
                IISM.Inventory.ProdInventory OpenW = new IISM.Inventory.ProdInventory();
                OpenW.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Not All fields were filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            DeleteTxt();
        }

        private void frmProdInventory_Initialized(object sender, EventArgs e)
        {
            ProdId = 0; Whid = 0;

                DeleteTxt();
                if (Classes.ProdInventory.LoadComboBox_ProdNo(ref this.cmbID)) {this.Close();}
        }


        public void DeleteTxt()
        {
             CheckControls.ClearTextBox(ref this.txtqty, ref this.txtEditQty);   
        }

        public bool AllComboBoxFilled()
        {
             if (CheckControls.VerifyIfTextboxIsNull(this.txtqty, this.txtEditQty))
            {
                return true;
            }

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
                if (Whid <= 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }

                
            
            if ((CheckControls.IsNumeric(txtEditQty.Text)) && (CheckControls.IsNumeric(txtqty.Text)) )
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
                Whid = Convert.ToInt32(cmbID2.SelectedValue.ToString());
                Classes.ProdInventory.LoadWindowC_Qty(Whid, ProdId, ref txtqty,ref txtEditQty);
            }
            catch (Exception)
            {
                Whid = 0;
            }
        }

        private void cmbID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ProdId = Convert.ToInt32(cmbID.SelectedValue.ToString());
                Classes.ProdInventory.LoadComboBox_WH(ref cmbID2, ProdId);
            }
            catch (Exception)
            {
                ProdId = 0;
            }
            
        }



        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {


            DataModel.IISM_Dataset.ProdInventoryDataTable dt = new DataModel.IISM_Dataset.ProdInventoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.WarehouseDataTable dt2 = new DataModel.IISM_Dataset.WarehouseDataTable();
            DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
            adpt2.Fill(dt2);

            DataModel.IISM_Dataset.ProductsDataTable dt3 = new DataModel.IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt3 = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adpt3.Fill(dt3);




            var qry = from pi in dt
                      join wh in dt2 on pi.WhID equals wh.WhID
                      join p in dt3 on pi.ProdNoID equals p.ProdNoID
                      orderby pi.ProdNoID, pi.WhID
                      select new
                      {
                          WhId = pi.WhID.ToString("00"), wh.WhDesc,
                          ProdNo = pi.ProdNoID.ToString("0000"), p.ProdName,
                         Quantity= Convert.ToDecimal( pi.Quantity)

                      };

            Classes.FillDataGridcs._lst.Clear();
            Classes.FillDataGridcs.DataGridName = "Products Inventory";
            foreach (var item in qry)
            {
                Classes.FillDataGridcs.AddElements(item);
            }

            dt.Dispose(); adpt.Dispose(); dt2.Dispose(); adpt2.Dispose(); dt3.Dispose(); adpt3.Dispose();

            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();
        }





    }

}
