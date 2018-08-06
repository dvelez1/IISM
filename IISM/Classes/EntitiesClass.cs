using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IISM.Classes
{

    class EntitiesClass
    {

        //**********************************
        public class InvoiceDet:IDisposable
        {

            public int ProdNo { get; set; }
            public string ProdName { get; set; }
            public int WhID { get; set; }
            public string WhName { get; set; }
            public decimal ProdPrice { get; set; }
            public decimal ProdNewPrice { get; set; }
            public decimal Units { get; set; }
            public decimal UnitsWithOffers { get; set; }
            public decimal TotalUnits { get; set; }
            public decimal Sales { get; set; }
            public decimal SalesWithOffers { get; set; }
            public decimal TotalSales { get; set; }
            public decimal Discount { get; set; }
            public decimal IVU { get; set; }
            public decimal SalesTax { get; set; }
            public decimal Gross { get; set; }


            public InvoiceDet()
            {

            }

            public void AddItems(InvoiceDet a)
            {
                ProdNo = a.ProdNo;
                WhID = a.WhID;
                ProdPrice = a.ProdPrice;
                ProdNewPrice = a.ProdNewPrice;
                Units = a.Units;
                Sales = a.Sales;
                Discount = a.Discount;
                IVU = a.IVU;
            }

            ~InvoiceDet()
            {
                //here you can write File.Close
                Console.WriteLine("Destructor");
            }
            public void Dispose()
            {
                //Close the file here
                GC.SuppressFinalize(this);
            }
        }
        //**********************************



        public class Invoice:IDisposable
        {
            public int InvNo { get; set; }
            public int CorpNo { get; set; }
            public string CorpDesc { get; set; }
            public DateTime Date { get; set; }
            public int year { get; set; }
            public int Month { get; set; }
            public decimal Gross { get; set; }
            public decimal Net { get; set; }
            public decimal TotalDiscount { get; set; }
            public decimal PayedByCustNo { get; set; }
            public decimal PendingPayment { get; set; }
            public int CustNo { get; set; }
            public string CustDesc { get; set; }
            public string OrderNo { get; set; }
            public decimal SalesTax { get; set; }

           public Invoice()
            {

            }

            ~Invoice()
            {
                //here you can write File.Close
                Console.WriteLine("Destructor");
            }
            public void Dispose()
            {
                //Close the file here
                GC.SuppressFinalize(this);
            }

        }


        //Class used to get Year and Month of the invoices to load Comoboxes or other related   // things ; Dennis Vélez ; 07-06-18

        public class GetMyDateOfInvoice:IDisposable
        {

            public int LastYear { get; set; }
            public int FirstYear { get; set; }
            int Month { get; set; }
            string MonthDesc { get; set; }

            public GetMyDateOfInvoice()
            {


                DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
                DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt select i.Year;
                if (qry.Count() == 0)
                {
                    FirstYear = 0; LastYear = 0;
                }
                else
                {
                    FirstYear = qry.Min(u => u);
                    LastYear = qry.Max(u => u);
                }

                dt.Dispose(); adpt.Dispose();
            }

            //Load Years and Month Comboboxes ; Dennis Vélez ; 07-06-18
            public void LoadYearCombobox(ref ComboBox cmbYear)
            {
                List<int> lst = new List<int>();
                for (int i = FirstYear; i <= LastYear; i++)
                {
                    lst.Add(i);
                }
                cmbYear.ItemsSource = lst;
            }


            public void LoadMonthComboBox(ref ComboBox cmbFP)
            {
                List<MonthValues> lst = new List<MonthValues>();
                lst.Add(new MonthValues { Month = 1, MonthDesc = "01 - January" });
                lst.Add(new MonthValues { Month = 2, MonthDesc = "02 - February" });
                lst.Add(new MonthValues { Month = 3, MonthDesc = "03 - March" });
                lst.Add(new MonthValues { Month = 4, MonthDesc = "04 - April" });
                lst.Add(new MonthValues { Month = 5, MonthDesc = "05 - May" });
                lst.Add(new MonthValues { Month = 6, MonthDesc = "06 - June" });
                lst.Add(new MonthValues { Month = 7, MonthDesc = "07 - July" });
                lst.Add(new MonthValues { Month = 8, MonthDesc = "08 - August" });
                lst.Add(new MonthValues { Month = 9, MonthDesc = "09 - September" });
                lst.Add(new MonthValues { Month = 10, MonthDesc = "10 - October" });
                lst.Add(new MonthValues { Month = 11, MonthDesc = "11 - November" });
                lst.Add(new MonthValues { Month = 12, MonthDesc = "12 - December" });
                cmbFP.ItemsSource = lst;
                cmbFP.DisplayMemberPath = "MonthDesc";
                cmbFP.SelectedValuePath = "Month";

            }


            ~GetMyDateOfInvoice()
            {
                //here you can write File.Close
                Console.WriteLine("Destructor");
            }
            public void Dispose()
            {
                //Close the file here
                GC.SuppressFinalize(this);
            }


        }

        public class MonthValues:IDisposable
        {
            public int Month { get; set; }
            public string MonthDesc { get; set; }

            ~MonthValues()
            {
                //here you can write File.Close
                Console.WriteLine("Destructor");
            }
            public void Dispose()
            {
                //Close the file here
                GC.SuppressFinalize(this);
            }

        }






    }



}


