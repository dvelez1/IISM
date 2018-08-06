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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IISM.Classes;

namespace IISM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }


        private void btmMaintenance_Click(object sender, RoutedEventArgs e)
        {
            IISM.MF_Forms.MainMasterFile OpenW = new IISM.MF_Forms.MainMasterFile();
            OpenW.Show();
            this.Close();

        }

  

        private void btnINVOICE_Click_1(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 10;
            GlobalVar.CallEditForm();
            this.Close();
            //IISM.Invoices.Invoice_MF NewForm = new IISM.Invoices.Invoice_MF();
            //NewForm.Show(); this.Close();
            
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            IISM.Inventory.InventoriesMenu OpenW = new IISM.Inventory.InventoriesMenu();
            OpenW.Show();
            this.Close();


                   }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            IISM.Reports.InvoiceRptMenu OpenW= new IISM.Reports.InvoiceRptMenu();
            OpenW.Show();
            this.Close();
        }





        //private void btnInventory_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Inventory In Progress");
        //}


        //private void btnReports_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Reports In Progress");
        //}



    }
}
