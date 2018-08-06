using System;
using System.Collections.Generic;
using System.Data;
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

namespace IISM.MF_Forms.OtherForms
{
    /// <summary>
    /// Interaction logic for frmDataGrid.xaml
    /// </summary>
    public partial class frmDataGrid : Window
    {
        public frmDataGrid()
        {
            InitializeComponent();
            DataModel.IISM_Dataset.OfferClassDataTable dt = new DataModel.IISM_Dataset.OfferClassDataTable();
            DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter();
            adpt.Fill(dt);
            IEnumerable<DataModel.IISM_Dataset.OfferClassRow> u = dt;
            this.DataGridExport.ItemsSource = u;

         
        }

        private static object LoadDt(object dt)
        {
            return dt;
        }

    }
}
