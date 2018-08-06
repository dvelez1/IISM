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

namespace IISM.MF_Forms.WH
{
    /// <summary>
    /// Interaction logic for WH_Edit.xaml
    /// </summary>
    public partial class WH_Edit : Window
    {

        private int ID;
        IISM.DataModel.IISM_Dataset db = new IISM.DataModel.IISM_Dataset();


        public WH_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }
           

        private void frmWH_Edit_Initialized(object sender, EventArgs e)
        {
            ResetControls();
        }


        private void ResetControls()
        {
            ID = 0; cmbID.ItemsSource = null;
            txtName.Text = null; txtComments.Text = null;
            WorkWithMF_Warehouse.LoadComboBox(ref this.cmbID);
            
        }

        private void cmbID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(cmbID.SelectedValue.ToString());
                CheckControls.ClearTextBox(ref this.txtName, ref this.txtComments);
                WorkWithMF_Warehouse.LoadWarehouseId(ID, ref this.txtName, ref txtComments);
    
            }
            catch (Exception)
            {
                ID = 0;
            }
        }


        private void btmDone_Click(object sender, RoutedEventArgs e)
        {
           if( CheckControls.VerifyIfTextboxIsNull(this.txtName, this.txtComments))
            {
                goto Exit;
            }

            if(WorkWithMF_Warehouse.EditWarehouse(ID, this.txtName, this.txtComments))
            {
                goto Exit;
            }
           
            ResetControls();

            Exit:;
        }

    
        private void btmReturn_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show(); this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }


        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DataModel.IISM_Dataset.WarehouseDataTable dt = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(dt);


                var qry = from i in dt

                          select new
                          {
                              WhID = i.WhID.ToString("00"),
                              i.WhDesc,
                              i.Notes
                          };
                Classes.FillDataGridcs._lst.Clear();
                Classes.FillDataGridcs.DataGridName = "Warehouses";

                foreach (var item in qry)
                {
                    Classes.FillDataGridcs.AddElements(item);
                }

                dt.Dispose(); adpt.Dispose();

                IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
                OpenW.Show();
            }
            catch (Exception)
            {

               
            }

            
        }




    }
}
