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


namespace IISM.Invoices
{
    /// <summary>
    /// Interaction logic for Invoice_GetInvNo.xaml
    /// </summary>
    public partial class Invoice_GetInvNo : Window
    {

        struct Variables
        {
            public int CustNo;
            public int Year;
            public int Month;
            public long InvNo;

        }

        Variables struc;

        public Invoice_GetInvNo()
        {
            
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 10;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ResetStruct();
            LoadYear();
            

        }

        private void ResetStruct()
        {
            struc.CustNo = 0;
            struc.InvNo = 0;
            struc.Month = 0;
            struc.Year = 0;

            cmbFY.ItemsSource = null;
            cmbFP.ItemsSource = null;
            cmbCustNo.ItemsSource = null;
            cmbInvNo.ItemsSource = null;





        }

        private void cmbFY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                struc.Year = Convert.ToInt32( cmbFY.SelectedValue.ToString());
                cmbFP.ItemsSource = null;
                struc.InvNo = 0;
                using (EntitiesClass.GetMyDateOfInvoice Myclass = new EntitiesClass.GetMyDateOfInvoice())
                {
                    Myclass.LoadMonthComboBox(ref cmbFP);
                }
                    
                
            }
            catch (Exception)
            {

                
            }


        }

        private void cmbFP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                struc.Month = Convert.ToInt32 (cmbFP.SelectedValue.ToString());
                cmbCustNo.ItemsSource = null;
                struc.InvNo = 0;
                InvoiceClss.LoadInvoiceCombobox(ref cmbCustNo, struc.Year, struc.Month, true,struc.CustNo);
            }
            catch (Exception)
            {

            }

        }

        private void cmbCustNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                struc.CustNo = Convert.ToInt32(cmbCustNo.SelectedValue.ToString());
                cmbInvNo.ItemsSource = null;
                struc.InvNo = 0;
                InvoiceClss.LoadInvoiceCombobox(ref cmbInvNo, struc.Year, struc.Month, false, struc.CustNo);
            }
            catch (Exception)
            {

                
            }
        }

        private void cmbInvNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                struc.InvNo = Convert.ToInt64(cmbInvNo.SelectedValue.ToString());
            }
            catch (Exception)
            {
                struc.InvNo = 0;

            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetStruct();
            LoadYear();
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (struc.InvNo != 0)
            {
                GlobalVar.InvNoId = struc.InvNo;
                ResetStruct();
                IISM.Invoices.Invoice_Edit W = new IISM.Invoices.Invoice_Edit();
                W.Show(); this.Close();
            }
            else
            {
                MessageBox.Show("Error: Please, select a valid Invoice Number! ", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            

        }


        private void LoadYear()
        {
            IISM.Classes.EntitiesClass.GetMyDateOfInvoice MyClass = new EntitiesClass.GetMyDateOfInvoice();
            MyClass.LoadYearCombobox(ref cmbFY);
            MyClass.Dispose();
        }

        
    }




}
