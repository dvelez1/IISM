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


namespace IISM.MF_Forms.ProdCategory
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class PC_Create : Window
    {
        public PC_Create()
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
            if (CheckControls.VerifyIfTextboxIsNull(txtName))
            {
                goto Exit;
            }
            Classes.ProductCategory.Create(this.txtName.Text);
            CheckControls.ClearTextBox(ref txtName);
            Exit:;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CheckControls.ClearTextBox(ref this.txtName);
        }


        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {


            DataModel.IISM_Dataset.ProducCategoryDataTable dt = new DataModel.IISM_Dataset.ProducCategoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
            adpt.Fill(dt);

            var qry = from i in dt
                      select new
                      {
                          ID = i.PCatID.ToString("000"),i.PCatDesc
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
