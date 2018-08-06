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

namespace IISM.MF_Forms
{
    /// <summary>
    /// Interaction logic for MF_WH.xaml
    /// </summary>
    public partial class MF_WH : Window
    {
        public MF_WH()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {

            GlobalVar.CreateMFAction = true;

            switch(GlobalVar.GlobalValue)
            {
                case 1:
                    IISM.MF_Forms.WH.WH_Create OpenW = new IISM.MF_Forms.WH.WH_Create();
                    OpenW.Show();
                    this.Close();
                    break;
                case 2:
                    IISM.MF_Forms.BussinesType.BS_TypeCreate Open2 = new IISM.MF_Forms.BussinesType.BS_TypeCreate();
                    Open2.Show();
                    this.Close();
                    break;
                case 3:
                    IISM.MF_Forms.ServiceClass.SC_Create Open3 = new IISM.MF_Forms.ServiceClass.SC_Create();
                    Open3.Show();
                    this.Close();
                    break;
                case 4:
                    IISM.MF_Forms.ProdCategory.PC_Create Open4 = new IISM.MF_Forms.ProdCategory.PC_Create();
                    Open4.Show();
                    this.Close();
                    break;
                case 5:
                    IISM.MF_Forms.OfferClass.OffClass_Create Open5 = new IISM.MF_Forms.OfferClass.OffClass_Create();
                    Open5.Show();
                    this.Close();
                    break;
                case 6:
                    IISM.MF_Forms.Corporation.Corp_Create Open6 = new IISM.MF_Forms.Corporation.Corp_Create();
                    Open6.Show();
                    this.Close();
                    break;
                case 7:
                    IISM.MF_Forms.Suppliers.MF_Suppliers Open7 = new IISM.MF_Forms.Suppliers.MF_Suppliers();
                    Open7.Show();
                    this.Close();
                    break;
                case 8:
                    IISM.MF_Forms.Customers.Customer_MF Open8 = new IISM.MF_Forms.Customers.Customer_MF();
                    Open8.Show();
                    this.Close();
                    break;
                case 9:
                    IISM.MF_Forms.Products.MF_Products Open9 = new IISM.MF_Forms.Products.MF_Products();
                    Open9.Show();
                    this.Close();
                    break;
                case 10:
                    IISM.Invoices.Invoice_MF Open10 = new IISM.Invoices.Invoice_MF();
                    Open10.Show();
                    this.Close();
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }

        }

        private void btnRtn_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVar.GlobalValue==10)
            {
                IISM.MainWindow OpenW = new IISM.MainWindow();
                OpenW.Show();
                this.Close();
            }
            else
            { 
                IISM.MF_Forms.MainMasterFile OpenW = new IISM.MF_Forms.MainMasterFile();
                OpenW.Show();
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            GlobalVar.CreateMFAction = false;

            switch (GlobalVar.GlobalValue)
            {
                case 1:
                    IISM.MF_Forms.WH.WH_Edit OpenW = new IISM.MF_Forms.WH.WH_Edit();
                    OpenW.Show();
                    this.Close();
                    break;
                case 2:
                    IISM.MF_Forms.BussinesType.BS_Type_Edit Open2 = new IISM.MF_Forms.BussinesType.BS_Type_Edit();
                    Open2.Show();
                    this.Close();
                    break;
                case 3:
                    IISM.MF_Forms.ServiceClass.SC_Edit Open3 = new IISM.MF_Forms.ServiceClass.SC_Edit();
                    Open3.Show();
                    this.Close();
                    break;
                case 4:
                    IISM.MF_Forms.ProdCategory.PC_Edit Open4 = new IISM.MF_Forms.ProdCategory.PC_Edit();
                    Open4.Show();
                    this.Close();
                    break;
                case 5:
                    IISM.MF_Forms.OfferClass.OffClass_Edit Open5 = new IISM.MF_Forms.OfferClass.OffClass_Edit();
                    Open5.Show();
                    this.Close();
                    break;
                case 6:
                    Corporation.Corp_Create Open6 = new Corporation.Corp_Create();
                    Open6.Show();
                    this.Close();
                    break;
                case 7:
                    IISM.MF_Forms.Suppliers.MF_Suppliers Open7 = new IISM.MF_Forms.Suppliers.MF_Suppliers();
                    Open7.Show();
                    this.Close();
                    break;
                case 8:
                    IISM.MF_Forms.Customers.Customer_MF Open8 = new IISM.MF_Forms.Customers.Customer_MF();
                    Open8.Show();
                    this.Close();
                    break;
                case 9:
                    IISM.MF_Forms.Products.MF_Products Open9 = new IISM.MF_Forms.Products.MF_Products();
                    Open9.Show();
                    this.Close();
                    break;
                case 10:
                    IISM.Invoices.Invoice_GetInvNo Open10 = new IISM.Invoices.Invoice_GetInvNo();
                    Open10.Show();
                    this.Close();
                    break;

                default:
                    MessageBox.Show("Error");
                    break;
            }

        }
    }
}
