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


namespace IISM.Invoices
{
    /// <summary>
    /// Interaction logic for WH_Create.xaml
    /// </summary>
    public partial class Invoice_MF : Window
    {
        int CorpoId = 0;long InvNo = 0; int CstNo = 0; string OrdNo = null;
        string WhName; string ProdName;
        int WhID = 0; int ProdNo = 0;
        decimal Price = 0; decimal Ivu = 0;
        decimal qty = 0; decimal NewPrice = 0;
        decimal Payed_;  DateTime TransDate;
        List<EntitiesClass.InvoiceDet> InvDet = new List<EntitiesClass.InvoiceDet>();
      
        public Invoice_MF()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnRTN_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 10;
            GlobalVar.CallEditForm();
            this.Close();
            //IISM.MainWindow OpenW = new IISM.MainWindow();
            //OpenW.Show(); this.Close();
                       
        }

     

        private void btnRawMaterialsInventory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Prontly will be implemented!", "Atention", MessageBoxButton.OK);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadCorporation();
        }

#region "ComboBoxes"

        private void cmbCorp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CorpoId = Convert.ToInt32( cmbCorp.SelectedValue.ToString());
            CorpAndCustReset(false);
            //cmbWH.ItemsSource = null; 
            //Classes.WorkWithMF_Warehouse.LoadComboBox(ref cmbWH);

        }

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

        private void cmbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CstNo = Convert.ToInt32(cmbCustomer.SelectedValue.ToString());
                CorpAndCustReset(true);
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

        private void CorpAndCustReset( bool CustomerReset)
        {
            txtPayed.Text = "0"; this.txtInvDate.Text = DateTime.Now.ToString();
            dtgridDetails.ItemsSource = null;
            InvDet.Clear();

            if (!CustomerReset)
            {
                cmbCustomer.ItemsSource = null; cmbProd.ItemsSource = null; cmbWH.ItemsSource = null;
                Classes.Customers_.LoadComboBox(ref cmbCustomer);
            }
  

            if(CustomerReset)
            {
                ResetInvoiceDetailToBeAdded();
            }
            
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
        

        //Load Corporation Code; Developer: Dennis
        private void LoadCorporation()
        {
            txtInvno.IsEnabled = false; txtInvno.Visibility = Visibility.Visible;
            txtPrice.IsEnabled = false;
            this.InvNo = InvoiceClss.GetInvoiceId();
            txtInvno.Text = InvNo.ToString("00000");
            Classes.InvoiceClss.LoadCorpCmb(ref cmbCorp);
            txtPayed.Text = "0"; this.txtInvDate.Text = DateTime.Now.ToString();
            txtGrossSales.Text = string.Format("{0:C}", "0");
            txtSalesTax.Text = string.Format("{0:C}", "0");
            txtPayed.Text = string.Format("{0:C}", "0");
            txtPendingPayment.Text = string.Format("{0:C}", "0");
            cmbCustomer.ItemsSource = null;
            txtOrdNo.Text = null;
        }


        private void Clear()
        {
            GlobalVar.CreateMFAction = true;
            IISM.Invoices.Invoice_MF Open10 = new IISM.Invoices.Invoice_MF();
            Open10.Show();
            this.Close();
        }


        #endregion




        private void btnADD_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Payed_ = Convert.ToDecimal(this.txtPayed.Text);
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

                this.txtGrossSales.Text = string.Format("{0:C}", SummarySales);
                this.txtSalesTax.Text = string.Format("{0:C}", SummarySalesTax);
                this.txtPendingPayment.Text = string.Format("{0:C}", SummaryPendingPayment);
            }

            ExitMethod:;

        }



        private void btmCreate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Payed_ = Convert.ToDecimal(txtPayed.Text);
                TransDate = Convert.ToDateTime(txtInvDate.Text);
                OrdNo = this.txtOrdNo.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Error with Payed Quantity or Invoice Date! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                goto ExitIntruction;
            }
          

            //Review Units by Products are available (Not was edited by other user)
            var qry = from i in InvDet
                      select new { i.ProdNo, i.WhID, i.TotalUnits };

            if (qry.Count()==0)
            {
                MessageBox.Show("Error: Not Invoice Details!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                goto ExitIntruction;
            }

            foreach (var item in qry)
            {
                decimal SecondUnits=0;
                InvoiceClss.GetProdPriceAndAvlQty(ref SecondUnits, item.ProdNo, item.WhID);
                if (SecondUnits<item.TotalUnits)
                {
                    MessageBox.Show("Error: Total Units for the Product: " + item.ProdNo.ToString() + " and the Warehouse:  " + item.WhID.ToString() + " was reduced to: " + SecondUnits.ToString() + "units by other user! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    goto ExitIntruction;
                }
            }
            qry = null;

            //Create Invoice
            long SecondInv = InvoiceClss.GetInvoiceId();
            InvNo = SecondInv;

            if (CorpoId==0 || InvNo==0 || CstNo ==0)
            {
                MessageBox.Show("Error:CorpId or InvNo or CstNo is/are equal to 0: Please, try again." , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                goto ExitIntruction;
            }

            //Create Invoice Details
            if (InvoiceClss.CreateInvoice( InvDet, CorpoId,InvNo,Payed_,TransDate, OrdNo, CstNo))
            {

                if (InvoiceClss.CreateInvoiceDet(InvDet, InvNo))
                {

                    //Update Payments By Customer
                    if (!InvoiceClss.UpdatePaymentHistory(InvNo, Payed_, TransDate, true, OrdNo))
                    {
                        goto ExitIntruction;
                    }

                    //Update Product Inventory 
                    InvoiceClss.UpdateProductInventory(InvDet, InvNo);

                }
                else
                {
                    //Create InvoiceDet Failed 
                    goto ExitIntruction;
                }


            }
            else
            {
                //Create Invoice Failed 
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
