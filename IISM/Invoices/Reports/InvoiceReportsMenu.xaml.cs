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


namespace IISM.Invoices.Reports
{
    /// <summary>
    /// Interaction logic for Invoice_GetInvNo.xaml
    /// </summary>
    public partial class InvoiceReportsMenu : Window
    {

        struct Variables
        {
            public int CustNo;
            public int Year;
            public int FirstMonth;
            public int LastMonth;
            public long InvNo;
            public bool IsWithInvoiceDetails;

        }

        Variables struc;

        public InvoiceReportsMenu()
        {
            
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            IISM.Reports.InvoiceRptMenu OpenW = new IISM.Reports.InvoiceRptMenu();
            OpenW.Show();
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ResetStruct();           
        }

        private void ResetStruct()
        {
            struc.CustNo = 0;
            struc.InvNo = 0;
            struc.FirstMonth = 0;
            struc.LastMonth = 0;
            struc.Year = 0;
            struc.IsWithInvoiceDetails = false;

            cmbFY.ItemsSource = null;
            cmbFP.ItemsSource = null;
            cmbFP_Last.ItemsSource = null;
            cmbCustNo.ItemsSource = null;
            LoadYear();
            this.chkActive.IsChecked = false;
           

        }

        private void cmbFY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                struc.CustNo = 0;
                struc.Year = Convert.ToInt32( cmbFY.SelectedValue.ToString());
                cmbFP.ItemsSource = null; cmbFP_Last.ItemsSource = null; cmbCustNo.ItemsSource = null;
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
                struc.FirstMonth = Convert.ToInt32 (cmbFP.SelectedValue.ToString());
                struc.CustNo = 0;
                cmbCustNo.ItemsSource = null; cmbFP_Last.ItemsSource = null;
                using (EntitiesClass.GetMyDateOfInvoice Myclass = new EntitiesClass.GetMyDateOfInvoice())
                {
                    Myclass.LoadMonthComboBox(ref cmbFP_Last);
                }

            }
            catch (Exception)
            {

            }

        }

        private void cmbFP_Last_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbCustNo.ItemsSource = null;
                struc.LastMonth = Convert.ToInt32(cmbFP_Last.SelectedValue.ToString());
                struc.CustNo = 0; 
                InvoiceClss.LoadCustomersComboBox(ref cmbCustNo, struc.Year,struc.FirstMonth, struc.LastMonth);
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
               
            }
            catch (Exception)
            {

                
            }
        }
         
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetStruct();
            LoadYear();
        }

        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (struc.IsWithInvoiceDetails)
            {
                InvoiceClss.SalesReportInvoiceDetails(struc.Year, struc.FirstMonth, struc.LastMonth, struc.CustNo);
            }
            else
            {
                InvoiceClss.SalesReport(struc.Year, struc.FirstMonth, struc.LastMonth, struc.CustNo);
            }            
        }

        private void LoadYear()
        {
            IISM.Classes.EntitiesClass.GetMyDateOfInvoice MyClass = new EntitiesClass.GetMyDateOfInvoice();
            MyClass.LoadYearCombobox(ref cmbFY);
            MyClass.Dispose();
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            if (chkActive.IsChecked ?? false)
            {
                if (chkActive.IsChecked == true)
                {
                    struc.IsWithInvoiceDetails = true;
                }
                else
                {
                    struc.IsWithInvoiceDetails = false;
                }
            }
            else
            {
                struc.IsWithInvoiceDetails = false;
            }
        }
    }




}
