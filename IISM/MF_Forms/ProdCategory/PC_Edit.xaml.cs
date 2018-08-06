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

namespace IISM.MF_Forms.ProdCategory
{
    /// <summary>
    /// Interaction logic for WH_Edit.xaml
    /// </summary>
    public partial class PC_Edit : Window
    {

        private int ID;
        IISM.DataModel.IISM_Dataset db = new IISM.DataModel.IISM_Dataset();


        public PC_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

         

        private void frmPC_Edit_Initialized(object sender, EventArgs e)
        {

             ResetControls();
        }


        private void ResetControls()
        {
            txtName.Text = null;
            cmbID.ItemsSource = null;
            if (Classes.ProductCategory.LoadComboBox(ref this.cmbID))
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
                
                if (Classes.ProductCategory.LoadWindow(ID, ref this.txtName))
                {
                    MessageBox.Show("Error, please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception)
            {
                
            }
        }


        private void btmDone_Click(object sender, RoutedEventArgs e)
        {
           if( CheckControls.VerifyIfTextboxIsNull(this.txtName))
            {
                goto Exit;
            }

            if(Classes.ProductCategory.Edit(ID, this.txtName))
            {
                goto Exit;
            }
            CheckControls.ClearTextBox(ref this.txtName);


            ResetControls();

            Exit:;
        }

    
        private void btmReturn_Click(object sender, RoutedEventArgs e)
        {
            Classes.GlobalVar.CallEditForm();
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {


            DataModel.IISM_Dataset.ProducCategoryDataTable dt = new DataModel.IISM_Dataset.ProducCategoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
            adpt.Fill(dt);

            var qry = from i in dt
                      select new
                      {
                          ID = i.PCatID.ToString("000"),
                          i.PCatDesc
                      };
            Classes.FillDataGridcs._lst.Clear();
            Classes.FillDataGridcs.DataGridName = "Product Category";
            foreach (var item in qry)
            {
                Classes.FillDataGridcs.AddElements(item);
            }

            dt.Dispose(); adpt.Dispose();

            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();
        }


    }
}
