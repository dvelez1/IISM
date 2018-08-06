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


namespace IISM.Reports
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class InvoiceRptMenu : Window
    {
        public InvoiceRptMenu()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            IISM.MainWindow OpenW = new IISM.MainWindow();
            OpenW.Show(); this.Close();
                       
        }



 

        private void btnInvoiceReport_Click(object sender, RoutedEventArgs e)
        {
            IISM.Invoices.Reports.InvoiceReportsMenu OpenW = new IISM.Invoices.Reports.InvoiceReportsMenu();
            OpenW.Show();
            this.Close();
        }
    }
}
