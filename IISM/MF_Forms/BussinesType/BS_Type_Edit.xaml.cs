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

namespace IISM.MF_Forms.BussinesType
{
    /// <summary>
    /// Interaction logic for WH_Edit.xaml
    /// </summary>
    public partial class BS_Type_Edit : Window
    {

        private int ID;
        IISM.DataModel.IISM_Dataset db = new IISM.DataModel.IISM_Dataset();


        public BS_Type_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
           
        }


            

        private void frmBS_Type_Edit_Initialized(object sender, EventArgs e)
        {
            ResetData();
        }


        private void cmbID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(cmbID.SelectedValue.ToString());
                CheckControls.ClearTextBox(ref this.txtName);
                
                if (WorkWithMF_BussinessType.LoadBSTypeId(ID, ref this.txtName))
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Error, please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //this.Close();
            }
        }


        private void btmDone_Click(object sender, RoutedEventArgs e)
        {
           if( CheckControls.VerifyIfTextboxIsNull(this.txtName))
            {
                goto Exit;
            }

            if(WorkWithMF_BussinessType.EditBussinessType(ID, this.txtName))
            {
                goto Exit;
            }
            CheckControls.ClearTextBox(ref this.txtName);
            
            ResetData();
            Exit:;

        }

    
        private void btmReturn_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show(); this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //CheckControls.ClearTextBox(ref this.txtName);
            //IISM.MF_Forms.BussinesType.BS_Type_Edit OpenW = new IISM.MF_Forms.BussinesType.BS_Type_Edit();
            //OpenW.Show();
            //this.Close();
            ResetData();
        }

        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {


            DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
            DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
            adpt.Fill(dt);

            var qry = from i in dt
                      select new
                      {
                          ID = i.BussinessTypeID.ToString("000"), i.BussinessDesc
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

        private void ResetData()
        {
            cmbID.ItemsSource = null;
            txtName.Text = null;
            
            try
            {
                if (WorkWithMF_BussinessType.LoadComboBox(ref this.cmbID))
                {
                    this.Close();
                }
            }
            catch (Exception)
            {


            }
        }

    }
}
