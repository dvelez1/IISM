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


namespace IISM.MF_Forms.WH
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class WH_Create : Window
    {
        public WH_Create()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show(); this.Close();
                       
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CheckControls.ClearTextBox(ref txtName, ref txtComments);

        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text)||string.IsNullOrEmpty(this.txtComments.Text))
            {
                MessageBox.Show("Please, fill all Textboxes!","Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                goto Exit;
            }
            WorkWithMF_Warehouse.CreateWH(this.txtName.Text, this.txtComments.Text);
            CheckControls.ClearTextBox(ref txtName, ref txtComments);

            Exit:;
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
