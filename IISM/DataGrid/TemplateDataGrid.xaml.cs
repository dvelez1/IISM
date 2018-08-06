using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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



using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace IISM.DataGrid
{
    /// <summary>
    /// Interaction logic for TemplateDataGrid.xaml
    /// </summary>
    public partial class TemplateDataGrid : Window
    {
        public TemplateDataGrid()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            txtName.Text = null;
            txtName.Text = FillDataGridcs.DataGridName;
            dtGrid.AutoGenerateColumns = true;
            dtGrid.ItemsSource=FillDataGridcs._lst;
            dtGrid.Items.Refresh();
        }

        private void btnToExcel_Click(object sender, RoutedEventArgs e)
        {

            ExportToExcel();

        }

        private void ExportToExcel()
        {
            dtGrid.SelectAllCells();
            dtGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dtGrid);
            String resultat = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.CommaSeparatedValue);
            String result = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.Text);
            dtGrid.UnselectAllCells();
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Prueba\test.xls");
            file.WriteLine(result.Replace(',', ' '));
            file.Close();
            Process.Start(@"C:\Prueba\test.xls");

        }









    }
}
