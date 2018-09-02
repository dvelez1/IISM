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
using System.Data;
using System.Collections.ObjectModel;
using System.IO;


namespace IISM.Invoices
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class Invoice_Edit : Window
    {
        long InvNo = 0; string OrdNo = null;
        string WhName; string ProdName;
        int WhID = 0; int ProdNo = 0;
        decimal Price = 0; decimal Ivu = 0;
        decimal qty = 0; decimal NewPrice = 0;
        decimal Payed_;  DateTime TransDate;
        List<EntitiesClass.InvoiceDet> InvDet = new List<EntitiesClass.InvoiceDet>();


        public Invoice_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }


     

        private void btnRawMaterialsInventory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Prontly will be implemented!", "Atention", MessageBoxButton.OK);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadData(); LoadDataStep2(); LoadDataStep3();
        }

#region "ComboBoxes"

       

        private void cmbWH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WhID = Convert.ToInt32(cmbWH.SelectedValue.ToString());
                cmbProd.ItemsSource = null;
                //Load Product
                IISM.Classes.InvoiceClss.LoadProduct(WhID, ref cmbProd);
            }
            catch (Exception)
            {
                this.txtPrice.Text = "0"; this.txtNewPrice.Text = "0"; this.txtUnits.Text = "0";
            }

        }

        private void cmbProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                ProdNo = Convert.ToInt32(cmbProd.SelectedValue.ToString());
                Classes.InvoiceClss.GetProdPriceAndAvlQty(ref  Price, ref qty, ref Ivu, ProdNo, WhID);
                this.txtPrice.Text = Price.ToString(); this.txtNewPrice.Text = Price.ToString();
                this.txtUnits.Text = "0"; this.txtOffUnits.Text = "0";
            }
            catch (Exception)
            {
                this.txtPrice.Text = "0"; this.txtNewPrice.Text = "0"; this.txtUnits.Text = "0";
                
            }


        }

        

#endregion



#region "Methods And Functions"

        //Add details of Invoice and verify if can be added; Developer: Dennis Vélez ; Date: 06-30-18
        private bool LoadDetailsData(ref EntitiesClass.InvoiceDet a)
        {
            
            ProdName = cmbProd.Text; WhName = cmbWH.Text;

            
            try
            {     

                try
                {
                    NewPrice = Convert.ToDecimal(this.txtNewPrice.Text);
                    Price = Convert.ToDecimal(this.txtPrice.Text);
                    a.Units = Convert.ToDecimal(txtUnits.Text);
                    a.UnitsWithOffers = Convert.ToDecimal(txtOffUnits.Text);
                    a.TotalUnits = a.Units + a.UnitsWithOffers;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Some value entered is not numeric. Please, review entered data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
               
                

                if (qty<(a.Units + a.UnitsWithOffers))
                {
                    MessageBox.Show("Error: There are not enough products in the warehouse! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!(a.UnitsWithOffers==0))
                {
                    if (NewPrice>Price)
                    {
                        MessageBox.Show("Error: New Price is greater than the Product Price! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }

                a.ProdNo = ProdNo; a.WhID = WhID; a.ProdPrice = Price; a.ProdNewPrice = NewPrice;

                if (ReviewIfDetailsWasAdded(WhID,ProdNo))
                {
                    MessageBox.Show("Error: A product was added previously from the same warehouse! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                a.Sales = a.Units * Price; a.SalesWithOffers = a.UnitsWithOffers * NewPrice; a.TotalSales = a.Sales + a.SalesWithOffers;
                a.Discount = (a.UnitsWithOffers * Price); a.Discount = a.Discount - a.SalesWithOffers;
                a.ProdName = ProdName; a.WhName = WhName; a.IVU = Ivu;

                try { a.SalesTax = a.TotalSales * (Ivu / 100); }
                catch (Exception) { a.SalesTax = 0; }

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Please, fill all information with the correct Data type! " , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        //
        private void ResetInvoiceDetailToBeAdded()
        {

            cmbWH.ItemsSource = null; Classes.WorkWithMF_Warehouse.LoadComboBox(ref cmbWH);
            cmbProd.ItemsSource = null; txtUnits.Text = null; txtPrice.Text = null; txtOffUnits.Text = null; txtNewPrice.Text = null;
               
        }

        

        //Avoid add elements when WH and Product exist. Developer: Dennis Vélez ; Date: 06-30-18
        private bool ReviewIfDetailsWasAdded(int WH, int Prod)
        {

            var qry = from i in InvDet
                      where i.WhID == WH && i.ProdNo == Prod
                      select i;
            if (qry.Count() == 0)
            {
                return false;
            }
            return true;
        }
        

        //Load Data Step1 - Reset Controls; Developer: Dennis Vélez 07-08-18
        private void LoadData()
        {
            txtInvno.IsEnabled = false; txtInvno.Visibility = Visibility.Visible;
            txtPrice.IsEnabled = false;
            txtCorp.IsEnabled = false;txtCustomer.IsEnabled = false;

            this.InvNo = GlobalVar.InvNoId;
            txtInvno.Text = InvNo.ToString("00000");
           
            txtToPay.Text = "0";
            txtSales.Text = string.Format("{0:C}", 0);
            txtSalesTax.Text = string.Format("{0:C}", 0);
            txtToPay.Text = string.Format("{0:C}", 0);
            txtPendingPayment.Text = string.Format("{0:C}", 0);
            txtGross.Text = string.Format("{0:C}", 0);
            txtOrdNo.Text = null;


            //Colocar invisible los comboboxez
            DisableInvoiceDetailsControls();


        }

        private void DisableInvoiceDetailsControls()
        {
            cmbWH.Visibility = Visibility.Hidden; cmbWH.IsEnabled = false;
            cmbProd.Visibility = Visibility.Hidden; cmbProd.IsEnabled = false;
            txtPrice.Visibility = Visibility.Hidden;txtNewPrice.Visibility = Visibility.Hidden;txtNewPrice.IsEnabled = false;
            txtUnits.Visibility = Visibility.Hidden; txtUnits.IsEnabled = false;
            txtOffUnits.Visibility = Visibility.Hidden;txtOffUnits.IsEnabled = false;
            btnADD.Visibility = Visibility.Hidden;btnADD.IsEnabled = false;

        }

        //Load Data Step2 - Get Data Invoice Header; Developer: Dennis Vélez ; Date: 07-08-18
        private void LoadDataStep2()
        {

           EntitiesClass.Invoice Invoice = new EntitiesClass.Invoice();
           Classes.InvoiceClss.LoadInvoiceHeader(ref Invoice, InvNo);

            txtInvno.Text = Invoice.InvNo.ToString("00000");
            txtCorp.Text = Invoice.CorpNo.ToString("00") + " - " + Invoice.CorpDesc;
            txtCustomer.Text = Invoice.CustNo.ToString("0000") + " - " + Invoice.CustDesc;
            txtOrdNo.Text = Invoice.OrderNo.ToString();
            txtInvDate.Text = Invoice.Date.ToString();

            txtPendingPayment.Text = string.Format("{0:C}", Invoice.PendingPayment) ;
            txtSales.Text = string.Format("{0:C}", Invoice.Net) ;
            txtSalesTax.Text = string.Format("{0:C}", Invoice.SalesTax);
            txtGross.Text = string.Format("{0:C}", Invoice.Gross) ;
            txtDiscount.Text = string.Format("{0:C}", Invoice.TotalDiscount) ;
            txtPayed.Text = string.Format("{0:C}", Invoice.PayedByCustNo) ;

            Invoice.Dispose();

        }

        //Load Data Step3 - Get Data Invoice Details ; Developer: Dennis Vélez ; Date: 07-08-18
        private void LoadDataStep3()
        {

            List<EntitiesClass.InvoiceDet> InvDet = new List< EntitiesClass.InvoiceDet > ();
            InvoiceClss.LoadEditInvoiceDetail(ref InvDet, InvNo);

            var qry = from i in InvDet
                      select new
                      {
                          Wh = i.WhID,
                          i.WhName,
                          Prod = i.ProdNo,
                          Name = i.ProdName,
                          i.Units,
                          Sales = Math.Round(i.TotalSales, 2),
                          UwOff = i.UnitsWithOffers,
                          SaleswOff = i.SalesWithOffers,
                          Discount = Math.Round(i.Discount, 2),
                          SalesTax = Math.Round(i.SalesTax, 2),
                          Gross = Math.Round(i.Gross)
                      };


            dtgridDetails.AutoGenerateColumns = true;
            dtgridDetails.ItemsSource = qry;
            dtgridDetails.Items.Refresh();

        

        }




        //Clear Payment / And New InProgress Transaction. Dennis Vélez / 07-07-18
        private void Clear()
        {
 
            this.txtToPay.Text = null;
            ResetInvoiceDetailToBeAdded();
            LoadData(); LoadDataStep2(); LoadDataStep3();

        }

        //DVG 07-07-18
        private void ReturnToInvoiceSelection()
        {
            IISM.Invoices.Invoice_GetInvNo W = new IISM.Invoices.Invoice_GetInvNo();
            W.Show(); this.Close();
        }


        #endregion


 #region "Controls Events"


        private void btnADD_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Payed_ = Convert.ToDecimal(this.txtToPay.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error:  Payed Quantyty is not numeric. Please, review entered data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                goto ExitMethod;
            }

            EntitiesClass.InvoiceDet a = new EntitiesClass.InvoiceDet();
            if (LoadDetailsData(ref a))
            {
                InvDet.Add(a);

                var qry = from i in InvDet
                          select new { Wh = i.WhID, i.WhName, Prod = i.ProdNo, Name = i.ProdName, i.Units,
                                        Price = i.ProdPrice, UwOff = i.UnitsWithOffers, NewPrice = i.ProdNewPrice,
                                            Sales = Math.Round(i.TotalSales, 2), Discount = Math.Round(i.Discount, 2),
                                                SalesTax = Math.Round(i.SalesTax, 2) };


                dtgridDetails.AutoGenerateColumns = true;
                dtgridDetails.ItemsSource = qry;
                dtgridDetails.Items.Refresh();
                ResetInvoiceDetailToBeAdded();

                decimal SummarySales = 0; decimal SummarySalesTax = 0; decimal SummaryPendingPayment = 0;


                foreach (var item in InvDet)
                {
                    SummarySales = SummarySales + item.TotalSales;
                    SummarySalesTax = SummarySalesTax + item.SalesTax;
                    SummaryPendingPayment = (SummarySales + SummarySalesTax) - Payed_;
                }

                this.txtSales.Text = string.Format("{0:C}", SummarySales);
                this.txtSalesTax.Text = string.Format("{0:C}", SummarySalesTax);
                this.txtPendingPayment.Text = string.Format("{0:C}", SummaryPendingPayment);
            }

            ExitMethod:;

        }



        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Payed_ = Convert.ToDecimal(txtToPay.Text);
                TransDate = Convert.ToDateTime(txtInvDate.Text);
                OrdNo = this.txtOrdNo.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Error with Payed Quantity or Invoice Date! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                goto ExitIntruction;
            }


            //Update Payments By Customer
            if (!InvoiceClss.UpdatePaymentHistory(InvNo, Payed_, TransDate, false, OrdNo))
            {
                goto ExitIntruction;
            }


            //Clear Data
            Clear();
            ExitIntruction:;

        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();

        }

        //Done DVG 07-07-18
        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            ReturnToInvoiceSelection();
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Message: Are you sure to Delete the Invoice! ", "Please Evaluate Carefully", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {


            }

            MessageBox.Show("Message: In progress", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

    }

    //public class Item
    //{

    //    public int WhID { get; set; }
    //    public string WhName { get; set; }
    //    public int ProdNo { get; set; }
    //    public string ProdName { get; set; }
    //    public decimal Units { get; set; }
    //    public decimal ProdPrice { get; set; }
    //    public decimal UnitsWithOffers { get; set; }
    //    public decimal ProdNewPrice { get; set; }
    //    public decimal Sales { get; set; }
    //    public decimal Discount { get; set; }
    //    public decimal SalesTax { get; set; }
    //}

}
