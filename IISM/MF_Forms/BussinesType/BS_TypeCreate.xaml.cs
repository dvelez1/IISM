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


namespace IISM.MF_Forms.BussinesType
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class BS_TypeCreate : Window
    {
        public BS_TypeCreate()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show(); this.Close();
                       
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckControls.VerifyIfTextboxIsNull(txtName))
            {
                MessageBox.Show("Please, fill all Textboxes!","Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                goto ExitInstruction;
            }
            
            WorkWithMF_BussinessType.Create(this.txtName.Text);
            CheckControls.ClearTextBox(ref txtName);
            ExitInstruction:;
        }

        //Executer Report - DVG 07-20-18
        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {

            DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
            DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
            adpt.Fill(dt);

            var qry = from i in dt
                      select new
                      {
                          ID= i.BussinessTypeID.ToString("000"),
                          i.BussinessDesc
                      };
            Classes.FillDataGridcs._lst.Clear();
            Classes.FillDataGridcs.DataGridName = "Bussiness Type";
            foreach (var item in qry)
            {
                Classes.FillDataGridcs.AddElements(item);
            }

            dt.Dispose(); adpt.Dispose();

            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = null;
        }
    }
    
}
