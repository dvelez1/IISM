using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IISM.DataModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using IISM.Classes;

namespace IISM.Classes
{
    //Dennis Vélez / Class Warehouse / 03-04-18
    public class WorkWithMF_Warehouse
    {

        //Done Dennis Vélez / Date:03-04-18
        public static bool CreateWH(string Desc, string Notes)
        {

            try
            {

                DataModel.IISM_Dataset.WarehouseDataTable wh = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(wh);
                DataModel.IISM_Dataset.WarehouseRow NewR = wh.NewWarehouseRow();
                var Count = wh.Count;

                if (Count == 0)
                {
                    NewR.WhID = 1;
                }
                else
                {
                    NewR.WhID = wh.Max(p => p.WhID) + 1;
                }

                NewR.WhDesc = Desc; NewR.Notes = Notes;
                wh.Rows.Add(NewR);
                adpt.Update(wh);
                wh.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date:03-04-18
        public static bool LoadWarehouseId(int id, ref TextBox Desc, ref TextBox Notes)
        {
            try
            {

                DataModel.IISM_Dataset.WarehouseDataTable wh = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(wh);

                var query = (from w in wh
                             where w.WhID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.WhDesc;
                    Notes.Text = item.Notes;
                }

                wh.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing Warehouse: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


        //Done Dennis Vélez / Date:03-04-18
        public static bool EditWarehouse(int id, TextBox Desc, TextBox Notes_)
        {

            try
            {

                DataModel.IISM_Dataset.WarehouseDataTable wh = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(wh);

                IEnumerable<IISM_Dataset.WarehouseRow> resultset = wh.Where(x => x.WhID == id);

                foreach (var row in resultset)
                {
                    row.WhDesc = Desc.Text;
                    row.Notes = Notes_.Text;
                }
                adpt.Update(wh);
                wh.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error Warehouse: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }

        //Done Dennis Vélez / Date:03-04-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
 
                DataModel.IISM_Dataset.WarehouseDataTable wh = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(wh);

                var qry = from i in wh
                          select new { i.WhID, WhDesc = i.WhID.ToString("00") + " - " + i.WhDesc };

                if (cmb.Items.Count == 0)
                {
                    //cmb.ItemsSource = wh;
                    //cmb.DisplayMemberPath = wh.WhDescColumn.ToString();
                    //cmb.SelectedValuePath = wh.WhIDColumn.ToString();
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "WhDesc";
                    cmb.SelectedValuePath = "WhID";

                    wh.Dispose(); adpt.Dispose();
                }
               else
                {
                   


                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Warehouse: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            

        }

    }
    //***************Class End***********************
    


    //Dennis Vélez / Class BussinessType / 03-06-18
    public class WorkWithMF_BussinessType
    {
        //Done Dennis Vélez / Date:03-06-18
        public static bool Create(string Desc)
        {
            try
            {

                DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
                DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.BussinessTypeRow NewR = dt.NewBussinessTypeRow();
                var Count = dt.Count;

                if (Count == 0)
                {
                    NewR.BussinessTypeID = 1;
                }
                else
                {
                    NewR.BussinessTypeID = dt.Max(p => p.BussinessTypeID) + 1;
                }

                NewR.BussinessDesc = Desc; 
                dt.Rows.Add(NewR);
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }

        //Done Dennis Vélez / Date:03-06-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {

                DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
                DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new {i.BussinessTypeID, BussinessDesc = i.BussinessTypeID.ToString("000") + " - " + i.BussinessDesc };
                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "BussinessDesc";
                    cmb.SelectedValuePath = "BussinessTypeID";
                    dt.Dispose();adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Bussiness Type: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }



        }

        //Done Dennis Vélez / Date:03-06-18
        public static bool EditBussinessType(int id, TextBox Desc)
        {

            try
            {

                DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
                DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.BussinessTypeRow> resultset = dt.Where(x => x.BussinessTypeID == id);

                foreach (var row in resultset)
                {
                    row.BussinessDesc = Desc.Text;
                }
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error BussinessTyoe: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }

        //Done Dennis Vélez / Date:03-06-18
        public static bool LoadBSTypeId(int id, ref TextBox Desc)
        {
            try
            {

                DataModel.IISM_Dataset.BussinessTypeDataTable dt = new DataModel.IISM_Dataset.BussinessTypeDataTable();
                DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.BussinessTypeTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.BussinessTypeID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.BussinessDesc;
                   
                }

                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing BussinessYpe: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        
    }
    //***************Class End***********************


    //Dennis Vélez / Class ServiceClass / 03-07-18
    public class ServiceClass
    {
        
        //Done Dennis Vélez / Date:03-07-18
        public static bool Create(string Desc)
        {
            try
            {

                DataModel.IISM_Dataset.ServiceClassDataTable dt = new DataModel.IISM_Dataset.ServiceClassDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.ServiceClassRow NewR = dt.NewServiceClassRow();
                var Count = dt.Count;

                if (Count == 0)
                {
                    NewR.SClassID = 1;
                }
                else
                {
                    NewR.SClassID = dt.Max(p => p.SClassID) + 1;
                }

                NewR.SclassDesc = Desc;
                dt.Rows.Add(NewR);
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            
        }

        //Done Dennis Vélez / Date: 03-07-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {

                DataModel.IISM_Dataset.ServiceClassDataTable dt = new DataModel.IISM_Dataset.ServiceClassDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                            select new {i.SClassID, SclassDesc = i.SClassID.ToString("000") + " - " + i.SclassDesc };
                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "SclassDesc";
                    cmb.SelectedValuePath = "SClassID";
                    dt.Dispose();adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - ServiceClass: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }



        }

        //Done Dennis Vélez / Date:03-07-18
        public static bool Edit(int id, TextBox Desc)
        {

            try
            {

                DataModel.IISM_Dataset.ServiceClassDataTable dt = new DataModel.IISM_Dataset.ServiceClassDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.ServiceClassRow> resultset = dt.Where(x => x.SClassID == id);

                foreach (var row in resultset)
                {
                    row.SclassDesc = Desc.Text;
                }
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error ServiceClass: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date: 03-07-18
        public static bool LoadWindow(int id, ref TextBox Desc)
        {
            try
            {

                DataModel.IISM_Dataset.ServiceClassDataTable dt = new DataModel.IISM_Dataset.ServiceClassDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceClassTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.SClassID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.SclassDesc;

                }

                dt.Dispose();adpt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing ServiceClass: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************


    //Dennis Vélez / Class ProductCategory / 03-07-18
    public class ProductCategory
    {

        //Done Dennis Vélez / Date: 03-07-18
        public static bool Create(string Desc)
        {
            try
            {

                DataModel.IISM_Dataset.ProducCategoryDataTable dt = new DataModel.IISM_Dataset.ProducCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.ProducCategoryRow NewR = dt.NewProducCategoryRow();
                var Count = dt.Count;

                if (Count == 0)
                {
                    NewR.PCatID = 1;
                }
                else
                {
                    NewR.PCatID = dt.Max(p => p.PCatID) + 1;
                }

                NewR.PCatDesc = Desc;
                dt.Rows.Add(NewR);
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date: 03-07-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {

                DataModel.IISM_Dataset.ProducCategoryDataTable dt = new DataModel.IISM_Dataset.ProducCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new {i.PCatID, PCatDesc= i.PCatID.ToString("000") + " - " + i.PCatDesc };

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "PCatDesc";
                    cmb.SelectedValuePath = "PCatID";
                    dt.Dispose(); adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - ProductCategory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }



        }

        //Done Dennis Vélez / Date:03-07-18
        public static bool Edit(int id, TextBox Desc)
        {

            try
            {

                DataModel.IISM_Dataset.ProducCategoryDataTable dt = new DataModel.IISM_Dataset.ProducCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.ProducCategoryRow> resultset = dt.Where(x => x.PCatID == id);

                foreach (var row in resultset)
                {
                    row.PCatDesc = Desc.Text;
                }
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error ProductCategory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date: 03-07-18
        public static bool LoadWindow(int id, ref TextBox Desc)
        {
            try
            {

                DataModel.IISM_Dataset.ProducCategoryDataTable dt = new DataModel.IISM_Dataset.ProducCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProducCategoryTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.PCatID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.PCatDesc;

                }

                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing ProductCategory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************



    //Dennis Vélez /  / 03-08-18
    public class OfferClass_
    {

        //Done Dennis Vélez / Date: 03-08-18
        public static bool Create(string Desc, string notes)
        {
            try
            {

                DataModel.IISM_Dataset.OfferClassDataTable dt = new DataModel.IISM_Dataset.OfferClassDataTable();
                DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.OfferClassRow NewR = dt.NewOfferClassRow();
                var Count = dt.Count;

                if (Count == 0)
                {
                    NewR.OfferrClassID = 1;
                }
                else
                {
                    NewR.OfferrClassID = dt.Max(p => p.OfferrClassID) + 1;
                }

                NewR.OClassDesc = Desc;
                NewR.Notes = notes;
                dt.Rows.Add(NewR);
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date: 03-08-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.OfferClassDataTable dt = new DataModel.IISM_Dataset.OfferClassDataTable();
                DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.OfferrClassID, OClassDesc = i.OfferrClassID.ToString("000") + " - " + i.OClassDesc};

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "OClassDesc";
                    cmb.SelectedValuePath = "OfferrClassID";
                    dt.Dispose();adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - OfferClass: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }



        }

        //Done Dennis Vélez / Date: 03-08-18
        public static bool Edit(int id, TextBox Desc, TextBox notes)
        {

            try
            {

                DataModel.IISM_Dataset.OfferClassDataTable dt = new DataModel.IISM_Dataset.OfferClassDataTable();
                DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.OfferClassRow> resultset = dt.Where(x => x.OfferrClassID == id);

                foreach (var row in resultset)
                {
                    row.OClassDesc = Desc.Text;
                    row.Notes = notes.Text;
                }
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error OferrClass: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date:03-17-18
        public static bool LoadWindow(int id, ref TextBox Desc, ref TextBox notes)
        {
            try
            {

                DataModel.IISM_Dataset.OfferClassDataTable dt = new DataModel.IISM_Dataset.OfferClassDataTable();
                DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.OfferClassTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.OfferrClassID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.OClassDesc;
                    notes.Text = item.Notes;
                }

                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing OfferClass: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************



    //Dennis Vélez / Corporation / 06/08/18
    public class Corporation_
    {
        public static bool IsActive { get; set; }
        
        //Done Dennis Vélez / Date: 03-19-18
        public static bool Create(int? id,string Desc, string Address, string Postal, string Phone, string Email, string Fax, int BussinessTypeID,bool Active,bool CreateAction)
        {
            try{            

                DataModel.IISM_Dataset.CorporationDataTable dt = new DataModel.IISM_Dataset.CorporationDataTable();
                DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.CorporationRow NewR = dt.NewCorporationRow();

                IEnumerable<IISM_Dataset.CorporationRow> resultset = dt.Where(x => x.CorpID == id);

                if (CreateAction)
                {

                    var Count = dt.Count;

                    if (Count == 0)
                    {
                        NewR.CorpID = 1;
                    }
                    else
                    {
                        NewR.CorpID = dt.Max(p => p.CorpID) + 1;
                    }

                    NewR.CorpDesc = Desc;
                    NewR.Address = Address;
                    NewR.Postal = Postal;
                    NewR.Phone = Phone;
                    NewR.Email = Email;
                    NewR.Fax = Fax;
                    NewR.Active = Active;
                    NewR.BussinessTypeID = BussinessTypeID;

                    dt.Rows.Add(NewR);
                    adpt.Update(dt);
                    dt.Dispose();adpt.Dispose();

                    return false;

                }
                else
                {

                    
                    if (resultset.Count()==0)
                    {
                        MessageBox.Show("Edit error: Corporation ID does not exist ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }
                    foreach (var row in resultset)
                    {
                        row.CorpDesc = Desc;
                        row.Address = Address;
                        row.Postal = Postal;
                        row.Phone = Phone;
                        row.Email = Email;
                        row.Fax = Fax;
                        row.Active = Active;
                        row.BussinessTypeID = BussinessTypeID;
                    }
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();
                    return false;

                }



            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date: DVG 03-19-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.CorporationDataTable dt = new DataModel.IISM_Dataset.CorporationDataTable();
                DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.CorpID, CorpDesc= i.CorpID.ToString("000") + " - " + i.CorpDesc };
                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "CorpDesc";
                    cmb.SelectedValuePath = "CorpID";
                    dt.Dispose(); adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Corporation: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            
        }


        //Done Dennis Vélez / Date: DVG 03-19-18
        public static bool Edit(int id, string Desc, string Address, string Postal, string Phone, string Email, string Fax, int BussinessTypeID, bool Active)
        {

            try
            {

                DataModel.IISM_Dataset.CorporationDataTable dt = new DataModel.IISM_Dataset.CorporationDataTable();
                DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.CorporationRow> resultset = dt.Where(x => x.CorpID == id);

                foreach (var row in resultset)
                {
                    row.CorpDesc = Desc;
                    row.Address = Address;
                    row.Postal = Postal;
                    row.Phone = Phone;
                    row.Email = Email;
                    row.Fax = Fax;
                    row.Active = Active;
                    row.BussinessTypeID = BussinessTypeID;
                }
                adpt.Update(dt);
                dt.Dispose(); adpt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error Corporation: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date: Verify DVG 03-19-18
        public static bool LoadWindow(int id, ref TextBox Desc, ref TextBox Address, ref TextBox Postal, ref TextBox Phone, ref TextBox Email, ref TextBox Fax, ref ComboBox cmb2, ref CheckBox chk, ref int BsId)
        {
            try
            {

                DataModel.IISM_Dataset.CorporationDataTable dt = new DataModel.IISM_Dataset.CorporationDataTable();
                DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.CorpID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.CorpDesc;
                    Address.Text = item.Address;
                    Postal.Text = item.Postal;
                    Phone.Text = item.Phone;
                    Email.Text = item.Email;
                    Fax.Text = item.Fax;
                    cmb2.SelectedValue = item.BussinessTypeID;
                    BsId = item.BussinessTypeID;
                    Corporation_.IsActive = item.Active;
                    chk.IsChecked = item.Active;
                    
                }

                dt.Dispose();adpt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing Corporation: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        
    }
    //***************Class End***********************
    


    //Dennis Vélez / Customers / 03-19-18 - Pending Verify some fields
    public class Customers_
    {

        public static bool IsActive { get; set; }

        //Done Dennis Vélez / Date: 06-07-2018
        public static bool Create(int? id, string Name, string Address, string Postal, string Phone, string Email, string Fax, int BussinessTypeID, bool Active, bool CreateAction)
        {
            try
            {

                DataModel.IISM_Dataset.CustomersDataTable dt = new DataModel.IISM_Dataset.CustomersDataTable();
                DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.CustomersRow NewR = dt.NewCustomersRow();

                IEnumerable<IISM_Dataset.CustomersRow> resultset = dt.Where(x => x.CstnoID == id);

                if (CreateAction)
                {
                    var Count = dt.Count;
                    if (Count == 0)
                    {
                        NewR.CstnoID = 1;
                    }
                    else
                    {
                        NewR.CstnoID = dt.Max(p => p.CstnoID) + 1;
                    }

                    NewR.CstName = Name;
                    NewR.Address = Address;
                    NewR.Postal = Postal;
                    NewR.Phone = Phone;
                    NewR.Email = Email;
                    NewR.Fax = Fax;
                    NewR.Active = Active;
                    NewR.BussinessTypeID = BussinessTypeID;

                    dt.Rows.Add(NewR);
                    adpt.Update(dt);
                    dt.Dispose();adpt.Dispose();

                    return false;
                }
                else
                {
                    if (resultset.Count()==0)
                    {
                        MessageBox.Show("Edit error: Customer ID does not exist ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }
                    foreach (var row in resultset)
                    {
                        row.CstName = Name;
                        row.Address = Address;
                        row.Postal = Postal;
                        row.Phone = Phone;
                        row.Email = Email;
                        row.Fax = Fax;
                        row.Active = Active;
                        row.BussinessTypeID = BussinessTypeID;
                    }
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();
                    
                    return false;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Insert/Edit error Customer: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date: 03-19-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.CustomersDataTable dt = new DataModel.IISM_Dataset.CustomersDataTable();
                DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.CstnoID, CstName = i.CstnoID.ToString("000") + " - " +  i.CstName };

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "CstName";
                    cmb.SelectedValuePath = "CstnoID";
                    dt.Dispose(); adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Customers: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }


        //Done Dennis Vélez / Date:   06-07-2018 (Not Apply)
        public static bool Edit(int id, string Name, string Address, string Postal, string Phone, string Email, string Fax, int BussinessTypeID, bool Active)
        {

            try
            {

                DataModel.IISM_Dataset.CustomersDataTable dt = new DataModel.IISM_Dataset.CustomersDataTable();
                DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.CustomersRow> resultset = dt.Where(x => x.CstnoID == id);

                foreach (var row in resultset)
                {
                    row.CstName = Name;
                    row.Address = Address;
                    row.Postal = Postal;
                    row.Phone = Phone;
                    row.Email = Email;
                    row.Fax = Fax;
                    row.Active = Active;
                    row.BussinessTypeID = BussinessTypeID;
                }
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error Customers: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date:  06-07-2018
        public static bool LoadWindow(int id, ref TextBox Name, ref TextBox Address, ref TextBox Postal, ref TextBox Phone, ref TextBox Email, ref TextBox Fax, ref ComboBox cmbBsnId, ref CheckBox chk, ref int BsId)
        {
            try
            {

                DataModel.IISM_Dataset.CustomersDataTable dt = new DataModel.IISM_Dataset.CustomersDataTable();
                DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
                adpt.Fill(dt);
                
                var query = (from w in dt
                             where w.CstnoID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Name.Text = item.CstName;
                    Address.Text = item.Address;
                    Postal.Text = item.Postal;
                    Phone.Text = item.Phone;
                    Email.Text = item.Email;
                    Fax.Text = item.Fax;
                    cmbBsnId.SelectedValue = item.BussinessTypeID;
                    BsId = item.BussinessTypeID;
                    Customers_.IsActive = item.Active;
                    chk.IsChecked = item.Active;
                }

                dt.Dispose();adpt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Window MF Customer: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************

        

    //Dennis Vélez / SUPPLIER /  03-19-18
    public class Supplier_
    {

        //Done Dennis Vélez / Date: 03-19-18
        public static bool Create(int? id, string Name, string Address, string Postal, string Phone, string Email, string Fax, int BussinessTypeID, bool CreateAction)
        {
            try
            {

                DataModel.IISM_Dataset.SUPPLIERSDataTable dt = new DataModel.IISM_Dataset.SUPPLIERSDataTable();
                DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.SUPPLIERSRow NewR = dt.NewSUPPLIERSRow();
                IEnumerable<IISM_Dataset.SUPPLIERSRow> resultset = dt.Where(x => x.SupplierID == id);

                if (CreateAction)
                {
                    var Count = dt.Count;

                    if (Count == 0)
                    {
                        NewR.SupplierID = 1;
                    }
                    else
                    {
                        NewR.SupplierID = dt.Max(p => p.SupplierID) + 1;
                    }

                    NewR.Name = Name;
                    NewR.Address = Address;
                    NewR.Postal = Postal;
                    NewR.Phone = Phone;
                    NewR.EMAIL = Email;
                    NewR.FAX = Fax;
                    NewR.BussinessTypeID = BussinessTypeID;

                    dt.Rows.Add(NewR);
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();

                    return false;
                }
                else
                {

                    if (resultset.Count()==0)
                    {
                        MessageBox.Show("Edit error: Supplier ID does not exist ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }
                    else
                    {
                        foreach (var row in resultset)
                        {
                            row.Name = Name;
                            row.Address = Address;
                            row.Postal = Postal;
                            row.Phone = Phone;
                            row.EMAIL = Email;
                            row.FAX = Fax;
                            row.BussinessTypeID = BussinessTypeID;
                            
                        }
                        adpt.Update(dt);
                        dt.Dispose(); adpt.Dispose();
                        return false;
                    }
                    
                  
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("SupplierS Master File Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }
        

        //Done Dennis Vélez / Date:06-07-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.SUPPLIERSDataTable dt = new DataModel.IISM_Dataset.SUPPLIERSDataTable();
                DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.SupplierID, Name= i.SupplierID.ToString("000") + " - " + i.Name};

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "Name";
                    cmb.SelectedValuePath = "SupplierID";
                    dt.Dispose(); adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Supplier: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                 return true;
            }

        }


        //Done Dennis Vélez / Date:  03-19-18 (Not in use. DVG 06-07-18)
        public static bool Edit(int id, string Name, string Address, string Postal, string Phone, string Email, string Fax, int BussinessTypeID, bool Active)
        {

            try
            {

                DataModel.IISM_Dataset.SUPPLIERSDataTable dt = new DataModel.IISM_Dataset.SUPPLIERSDataTable();
                DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.SUPPLIERSRow> resultset = dt.Where(x => x.SupplierID == id);

                foreach (var row in resultset)
                {
                    row.Name = Name;
                    row.Address = Address;
                    row.Postal = Postal;
                    row.Phone = Phone;
                    row.EMAIL = Email;
                    row.FAX = Fax;
                    row.BussinessTypeID = BussinessTypeID;
                }
                adpt.Update(dt);
                dt.Dispose();adpt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error Supplier: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date:  - 06-07-18
        public static bool LoadWindow(int id, ref TextBox Name, ref TextBox Address, ref TextBox Postal, ref TextBox Phone, ref TextBox Email, ref TextBox Fax, ref ComboBox cmbBsnId, ref int BsID)
        {
            try
            {

                DataModel.IISM_Dataset.SUPPLIERSDataTable dt = new DataModel.IISM_Dataset.SUPPLIERSDataTable();
                DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.SUPPLIERSTableAdapter();
                adpt.Fill(dt);
                
                var query = (from w in dt
                             where w.SupplierID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Name.Text = item.Name;
                    Address.Text = item.Address;
                    Postal.Text = item.Postal;
                    Phone.Text = item.Phone;
                    Email.Text = item.EMAIL;
                    Fax.Text = item.FAX;
                    cmbBsnId.SelectedValue = item.BussinessTypeID;
                    BsID = item.BussinessTypeID;
                }

                dt.Dispose();adpt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error filling controls Editing Supplier: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************



    //Dennis Vélez / ServiceAndMaterialCategory / 03-20-18 - Pending Verify some fields
    public class ServiceAndMaterialCategory_
    {

        //Done Dennis Vélez / Date: 03-20-18
        public static bool Create(string Desc, int SClassID)
        {
            try
            {

                DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable dt = new DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.ServiceAndMaterialCategoryRow NewR = dt.NewServiceAndMaterialCategoryRow();
                var Count = dt.Count;

                if (Count == 0)
                {
                    NewR.CatID = 1;
                }
                else
                {
                    NewR.CatID = dt.Max(p => p.CatID) + 1;
                }

                NewR.CatDesc = Desc;
                NewR.SClassID = SClassID;

                dt.Rows.Add(NewR);
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Insert error: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date: 03-20-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable dt = new DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter();
                adpt.Fill(dt);

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = dt;
                    cmb.DisplayMemberPath = dt.CatDescColumn.ToString();
                    cmb.SelectedValuePath = dt.CatIDColumn.ToString();
                    dt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - ServiceAndMaterialCategory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }


        //Done Dennis Vélez / Date:  03-20-18
        public static bool Edit(int id, string Desc,  int SClassID)
        {

            try
            {

                DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable dt = new DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.ServiceAndMaterialCategoryRow> resultset = dt.Where(x => x.CatID == id);

                foreach (var row in resultset)
                {
                    row.CatDesc = Desc;
                    row.SClassID = SClassID;
                }
                adpt.Update(dt);
                dt.Dispose();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error ServiceAndMaterialCategory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date:  - Pending Verify some fields
        public static bool LoadWindow(int id, ref TextBox Desc, ref ComboBox cmbSClassID)
        {
            try
            {

                DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable dt = new DataModel.IISM_Dataset.ServiceAndMaterialCategoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ServiceAndMaterialCategoryTableAdapter();
                adpt.Fill(dt);
                
                var query = (from w in dt
                             where w.SClassID == id
                             select w).ToList();
                foreach (var item in query)
                {
                    Desc.Text = item.CatDesc;
                    cmbSClassID.SelectedItem = item.SClassID; // Revisar
                }

                dt.Dispose();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("error filling Texbox Editing ServiceAndMaterialCategory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************


    //Dennis Vélez / Products / 
    public class Products_
    {
        public static bool IsActive { get; set; }

        //Done Dennis Vélez / Date: 06-07-18
        public static bool Create(int? id,string Name,decimal Price, string Desc, bool Active, double IVU, int PCatID, decimal Cost, bool CreateAction)
        {
            try
            {

                DataModel.IISM_Dataset.ProductsDataTable dt = new DataModel.IISM_Dataset.ProductsDataTable();
                DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.ProductsRow NewR = dt.NewProductsRow();

                IEnumerable<IISM_Dataset.ProductsRow> resultset = dt.Where(x => x.ProdNoID == id);
                
                if (CreateAction)
                { 
                    var Count = dt.Count;

                    if (Count == 0)
                    {
                        NewR.ProdNoID = 1;
                    }
                    else
                    {
                        NewR.ProdNoID = dt.Max(p => p.ProdNoID) + 1;
                    }

                    NewR.ProdName = Name;
                    NewR.Price = Price;
                    NewR.Description = Desc;
                    NewR.Active = Active;
                    NewR.IVU = IVU;
                    NewR.PCatID = PCatID;
                    NewR.Cost = Cost;

                    dt.Rows.Add(NewR);
                    adpt.Update(dt);
                    dt.Dispose();adpt.Dispose();

                return false;

                }
                else
                {
                    if (resultset.Count()==0)
                    {
                        MessageBox.Show("Edit error: Product ID does not exist ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }

                    foreach (var row in resultset)
                    {
                        row.ProdName = Name;
                        row.Price = Price;
                        row.Description = Desc;
                        row.Active = Active;
                        row.IVU = IVU;
                        row.Active = Active;
                        row.PCatID = PCatID;
                        row.Cost = Cost;
                    }
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();

                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Insert/Edit errror: Table Products! " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Done Dennis Vélez / Date: 06-12-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.ProductsDataTable dt = new DataModel.IISM_Dataset.ProductsDataTable();
                DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
                adpt.Fill(dt);

                var qry = (from i in dt
                      select new { i.ProdNoID, ProdName = i.ProdNoID.ToString("000") + " - " + i.ProdName }).ToList();

                if (cmb.Items.Count == 0)
                {
                    //cmb.ItemsSource = dt;
                    //cmb.DisplayMemberPath = dt.ProdNameColumn.ToString();
                    //cmb.SelectedValuePath = dt.ProdNoIDColumn.ToString();

                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "ProdName";
                    cmb.SelectedValuePath = "ProdNoID";

                    dt.Dispose();adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Products: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }


        //Done Dennis Vélez / Date:  03-20-18
        public static bool Edit(int id, string Name, decimal Price, string Desc, bool Active, double IVU, int PCatID)
        {

            try
            {

                DataModel.IISM_Dataset.ProductsDataTable dt = new DataModel.IISM_Dataset.ProductsDataTable();
                DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
                adpt.Fill(dt);

                IEnumerable<IISM_Dataset.ProductsRow> resultset = dt.Where(x => x.ProdNoID == id);

                foreach (var row in resultset)
                {
                    row.ProdName = Name;
                    row.Price = Price;
                    row.Description = Desc;
                    row.Active = Active;
                    row.IVU = IVU;
                    row.PCatID = PCatID;
                }
                adpt.Update(dt);
                dt.Dispose();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Update error Products: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }


        }


        //Done Dennis Vélez / Date:  - 06-07-18
        public static bool LoadWindow(int id, ref TextBox Name, ref TextBox Price,ref TextBox Desc,ref TextBox IVU, ref ComboBox cmbPCatID, ref CheckBox chk, ref int PcId, ref TextBox Cost)
        {
            try
            {

                DataModel.IISM_Dataset.ProductsDataTable dt = new DataModel.IISM_Dataset.ProductsDataTable();
                DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.ProdNoID == id
                             select w).ToList();

                foreach (var item in query)
                {
                    Name.Text = item.ProdName;
                    Price.Text = item.Price.ToString();
                    Desc.Text = item.Description;
                    IVU.Text = item.IVU.ToString();
                    cmbPCatID.SelectedValue = item.PCatID;
                    PcId = item.PCatID;
                    Products_.IsActive = item.Active;
                    chk.IsChecked = item.Active;
                    Cost.Text = item.Cost.ToString();
                }

                dt.Dispose();adpt.Dispose();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error filling Windows Products: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }


    }
    //***************Class End***********************



     //Done Dennis Velez / 06-08-18 (Was implemented before. Not in use. DVG 06-12-18
    public class Warehouse
    {

        //Done Dennis Vélez / Date: 06-08-18    
        public static bool Create(int? id, string WhDesc,  string Notes,  bool CreateAction)
        {
            try
            {

                DataModel.IISM_Dataset.WarehouseDataTable dt = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.WarehouseRow NewR = dt.NewWarehouseRow();

                IEnumerable<IISM_Dataset.WarehouseRow> resultset = dt.Where(x => x.WhID == id);

                if (CreateAction)
                {
                    var Count = dt.Count;

                    if (Count == 0)
                    {
                        NewR.WhID = 1;
                    }
                    else
                    {
                        NewR.WhID = dt.Max(p => p.WhID) + 1;
                    }

                    NewR.WhDesc = WhDesc;
                    NewR.Notes = Notes;
                    
                    dt.Rows.Add(NewR);
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();

                    return false;

                }
                else
                {
                    if (resultset.Count() == 0)
                    {
                        MessageBox.Show("Edit error: Warehouse ID does not exist ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }

                    foreach (var row in resultset)
                    {
                        row.WhDesc = WhDesc;
                        row.Notes = Notes;
                    }
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();

                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Insert/Edit errror: Table Warehouse! " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }


        //Done Dennis Vélez / Date: 06-08-18
        public static bool LoadComboBox(ref ComboBox cmb)
        {
            try
            {
                DataModel.IISM_Dataset.WarehouseDataTable dt = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(dt);

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = dt;
                    cmb.DisplayMemberPath = dt.WhDescColumn.ToString();
                    cmb.SelectedValuePath = dt.WhIDColumn.ToString();
                    dt.Dispose(); adpt.Dispose();
                }
                else
                {



                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Warehouse: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }


        //Done Dennis Vélez / Date:  - 06-08-18
        public static bool LoadWindow(int id, ref TextBox WhDesc, ref TextBox Notes)
        {
            try
            {

                DataModel.IISM_Dataset.WarehouseDataTable dt = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.WhID == id
                             select w).ToList();

                foreach (var item in query)
                {
                    WhDesc.Text = item.WhDesc;
                    Notes.Text = item.Notes;
                }

                dt.Dispose(); adpt.Dispose();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error filling Windows Warehouse: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }
        

    }
    //***************Class End***********************


    //Class to Update the Product Inventory. DVG 06-09-18
    public class ProdInventory
    {


        //Done Dennis Vélez / Date: 06-09-18    
        public static bool Create(int ProdNoId, int WhId, double Quantity)
        {
            try
            {

                DataModel.IISM_Dataset.ProdInventoryDataTable dt = new DataModel.IISM_Dataset.ProdInventoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.ProdInventoryRow NewR = dt.NewProdInventoryRow();

                IEnumerable<IISM_Dataset.ProdInventoryRow> resultset = dt.Where(x => x.ProdNoID == ProdNoId && x.WhID==WhId);
              
               
                    //Create
                    
                    if (resultset.Count() == 0)
                    {

                        NewR.WhID = WhId;
                        NewR.ProdNoID = ProdNoId;
                        NewR.Quantity = Quantity;
                   
                        dt.Rows.Add(NewR);
                        adpt.Update(dt);
                        dt.Dispose(); adpt.Dispose();
                        return false;
                    }
                    else
                    {
                    //Edit

                        foreach (var row in resultset)
                        {
                            row.Quantity = Quantity;
                        }
                        adpt.Update(dt);
                        dt.Dispose(); adpt.Dispose();

                        return false;
                     }

            }
            catch (Exception e)
            {
                MessageBox.Show("Insert/Edit error! Product Inventory." + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }
        
        //Done Dennis Vélez / Date: 06-09-18
        public static bool LoadComboBox_ProdNo(ref ComboBox cmbProdNo)
        {
            try
            {
      
                DataModel.IISM_Dataset.ProductsDataTable dt = new DataModel.IISM_Dataset.ProductsDataTable();
                DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.ProdNoID, ProdName= i.ProdNoID.ToString("0000") + " - " + i.ProdName};
              

                if(cmbProdNo.Items.Count==0)
                {
                    cmbProdNo.ItemsSource = qry;
                    cmbProdNo.DisplayMemberPath = "ProdName";
                    cmbProdNo.SelectedValuePath = "ProdNoID";
                    dt.Dispose(); adpt.Dispose();
                }
 
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Product: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        
        public static bool LoadComboBox_WH(ref ComboBox cmbWH, int ProdNo)
        {
            try
            {

                DataModel.IISM_Dataset.WarehouseDataTable dt = new DataModel.IISM_Dataset.WarehouseDataTable();
                DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.WhID, WhDesc = i.WhID.ToString("00") + " - " + i.WhDesc };


                if (cmbWH.Items.Count == 0)
                {
                    cmbWH.ItemsSource = qry;
                    cmbWH.DisplayMemberPath = "WhDesc";
                    cmbWH.SelectedValuePath = "WhID";
                    dt.Dispose(); adpt.Dispose();
                }

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Warehouse: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

               

        //Load Quantity of Inventory ; Developer: Dennis Vélez ; Date: 06-09-18
        public static bool LoadWindowC_Qty(int WhId, int ProdId, ref TextBox QtyOfProd, ref TextBox qtyEdit)
        {
            try
            {

                DataModel.IISM_Dataset.ProdInventoryDataTable dt = new DataModel.IISM_Dataset.ProdInventoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.WhID == WhId && w.ProdNoID== ProdId
                             select w).ToList();


                foreach (var item in query)
                {
                    QtyOfProd.Text = item.Quantity.ToString(); qtyEdit.Text = item.Quantity.ToString();
                }

                if (query.Count() == 0)
                {
                    qtyEdit.Text = "0"; QtyOfProd.Text = "0";
                }

                dt.Dispose(); adpt.Dispose();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error filling Quantity of Products Inventory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

    }


    //Class to Update the Product Inventory. DVG 06-09-18
    public class InvoiceClss
    {


     #region"Invoice"

        //Done Dennis Vélez / Date:  
        public static bool Create(int ProdNoId, int WhId, double Quantity)
        {
            try
            {

                DataModel.IISM_Dataset.ProdInventoryDataTable dt = new DataModel.IISM_Dataset.ProdInventoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.ProdInventoryRow NewR = dt.NewProdInventoryRow();

                IEnumerable<IISM_Dataset.ProdInventoryRow> resultset = dt.Where(x => x.ProdNoID == ProdNoId && x.WhID == WhId);


                //Create

                if (resultset.Count() == 0)
                {

                    NewR.WhID = WhId;
                    NewR.ProdNoID = ProdNoId;
                    NewR.Quantity = Quantity;

                    dt.Rows.Add(NewR);
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();
                    return false;
                }
                else
                {
                    //Edit

                    foreach (var row in resultset)
                    {
                        row.Quantity = Quantity;
                    }
                    adpt.Update(dt);
                    dt.Dispose(); adpt.Dispose();

                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Insert/Edit error! Product Inventory." + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Load ComboBox with Invoices Done: Dennis Vélez / Date: 06-19-18
        public static bool LoadInvoiceCmb(ref ComboBox cmbINV, int CorpID)
        {
            try
            {

                DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
                DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt where i.CorpID==CorpID select new { i.InvoiceID};
                
                if (cmbINV.Items.Count == 0)
                {
                    cmbINV.ItemsSource = qry;
                    cmbINV.DisplayMemberPath = "InvoiceID";
                    cmbINV.SelectedValuePath = "InvoiceID";
                    dt.Dispose(); adpt.Dispose();
                }

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - No Invoices associate to Selected Corporation: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

        //Subject - Load ComboBox Corporation / Developer: Dennis Vélez / Date: 06-19-18
        public static bool LoadCorpCmb(ref ComboBox cmb)
        {
            try
            {

                DataModel.IISM_Dataset.CorporationDataTable dt = new DataModel.IISM_Dataset.CorporationDataTable();
                DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
                adpt.Fill(dt);

                var qry = from i in dt
                          select new { i.CorpID, CorpDesc = i.CorpID.ToString("00") + " - " + i.CorpDesc };


                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = qry;
                    cmb.DisplayMemberPath = "CorpDesc";
                    cmb.SelectedValuePath = "CorpID";
                    dt.Dispose(); adpt.Dispose();
                }

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading ComboBox - Corporation: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

        }

   
        //Subject - Get Next Invoice Number ; Developer: Dennis Vélez ; Date: 06-26-18
        public static long GetInvoiceId()
        {

            IISM_Dataset.InvoiceDataTable dt = new IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);
            DataModel.IISM_Dataset.InvoiceRow NewR = dt.NewInvoiceRow();
            if (dt.Count == 0)
            {
                return 1;
            }
            else
            {
                return dt.Max(p => p.InvoiceID) + 1;
            }
            
        }


        //Load Quantity of Inventory ; Developer: Dennis Vélez ; Date: 
        public static bool LoadWindowC_Qty(int WhId, int ProdId, ref TextBox QtyOfProd, ref TextBox qtyEdit)
        {
            try
            {

                DataModel.IISM_Dataset.ProdInventoryDataTable dt = new DataModel.IISM_Dataset.ProdInventoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
                adpt.Fill(dt);

                var query = (from w in dt
                             where w.WhID == WhId && w.ProdNoID == ProdId
                             select w).ToList();


                foreach (var item in query)
                {
                    QtyOfProd.Text = item.Quantity.ToString(); qtyEdit.Text = item.Quantity.ToString();
                }

                if (query.Count() == 0)
                {
                    qtyEdit.Text = "0"; QtyOfProd.Text = "0";
                }

                dt.Dispose(); adpt.Dispose();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error filling Quantity of Products Inventory: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        //Load Product ComboBox. Developer: Dennis Vélez ; Date: 06-27-18
        public static bool LoadProduct(int WhId, ref ComboBox cmbProd)
        {
            DataModel.IISM_Dataset.ProdInventoryDataTable dt = new IISM_Dataset.ProdInventoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.ProductsDataTable dtProd = new IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adptProd = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adptProd.Fill(dtProd);

            var qry = from prod in dtProd
                      join wh in dt on prod.ProdNoID equals wh.ProdNoID
                      where wh.WhID == WhId
                      select new { prod.ProdNoID, ProdName = prod.ProdNoID.ToString("000") + '-' + prod.ProdName, wh.Quantity,prod.Price,prod.IVU };
            
            if (cmbProd.Items.Count == 0)
            {
                cmbProd.ItemsSource = qry;
                cmbProd.DisplayMemberPath = "ProdName";
                cmbProd.SelectedValuePath = "ProdNoID";
            }
            dt.Dispose(); adpt.Dispose(); dtProd.Dispose(); adptProd.Dispose();
            return false;
        }

        //Get Product Price and Qty available by Warehouse. DVG 06-30-18
        public static void GetProdPriceAndAvlQty(ref decimal Price, ref decimal AvailableQty, ref decimal Ivu, int ProdNo, int Whid)
        {
            DataModel.IISM_Dataset.ProdInventoryDataTable dt = new IISM_Dataset.ProdInventoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.ProductsDataTable dtProd = new IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adptProd = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adptProd.Fill(dtProd);
            
            var qry = from prod in dtProd
                      join wh in dt on prod.ProdNoID equals wh.ProdNoID
                      where wh.WhID == Whid && wh.ProdNoID==ProdNo
                      select new { prod.ProdNoID, ProdName = prod.ProdNoID.ToString("000") + '-' + prod.ProdName, wh.Quantity, prod.Price, prod.IVU };

            foreach (var item in qry)
            {
                Price = item.Price;
                Ivu = (decimal) item.IVU;
                AvailableQty = (decimal) item.Quantity;
               
            }

            adpt.Dispose();dt.Dispose();adptProd.Dispose(); dtProd.Dispose();

        }

        //Overwrite to get Available qty only for Product and Warehouse. DVG 06-30-18
        public static void GetProdPriceAndAvlQty( ref decimal AvailableQty, int ProdNo, int Whid)
        {
            DataModel.IISM_Dataset.ProdInventoryDataTable dt = new IISM_Dataset.ProdInventoryDataTable();
            DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.ProductsDataTable dtProd = new IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adptProd = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adptProd.Fill(dtProd);

            var qry = from prod in dtProd
                      join wh in dt on prod.ProdNoID equals wh.ProdNoID
                      where wh.WhID == Whid && wh.ProdNoID == ProdNo
                      select new { prod.ProdNoID, ProdName = prod.ProdNoID.ToString("000") + '-' + prod.ProdName, wh.Quantity, prod.Price, prod.IVU };

            foreach (var item in qry)
            {
               
                AvailableQty = (decimal)item.Quantity;

            }

            adpt.Dispose(); dt.Dispose(); adptProd.Dispose(); dtProd.Dispose();

        }


        //Update Product Inventory when invoices are created. ; Developer: Dennis Vélez ; Date: 07-09-18
        internal static bool UpdateProductInventory(List<EntitiesClass.InvoiceDet> InvDet, long InvNo)
        {


            try
            {

                DataModel.IISM_Dataset.ProdInventoryDataTable dt = new DataModel.IISM_Dataset.ProdInventoryDataTable();
                DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.ProdInventoryTableAdapter();
                adpt.Fill(dt);
                
                foreach (var item in InvDet)
                {
                    IEnumerable<IISM_Dataset.ProdInventoryRow> resultset = dt.Where(x => x.WhID == item.WhID && x.ProdNoID==item.ProdNo);
                    foreach (var row in resultset)
                    {
                        row.Quantity = row.Quantity - (double) item.TotalUnits;
                    }
                }
                adpt.Update(dt);
                dt.Dispose(); adpt.Dispose();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Updating Product Inventory: " + InvNo.ToString("0000") + " ---> " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        //Create Invoice ; Developer; Dennis Vélez ; Date: 06-30-18
        internal static bool CreateInvoice( List<EntitiesClass.InvoiceDet> InvoiceDet_, int CorpId, long InvNo, decimal Payed, DateTime InvDate, string OrdNo, int CstNo)
        {

            try
            {
                decimal Sales = 0; decimal SalesTax = 0; decimal TotalDiscount = 0;

                foreach (var item in InvoiceDet_)
                {
                    Sales = Sales + item.TotalSales;
                    SalesTax = SalesTax + item.SalesTax;
                    TotalDiscount = TotalDiscount + item.Discount;
                }


                DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
                DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.InvoiceRow NewR = dt.NewInvoiceRow();

                NewR.InvoiceID = Convert.ToInt32(InvNo);
                NewR.CorpID = CorpId;
                NewR.Sales = Sales;
                NewR.SalesTax = SalesTax;
                NewR.TotalDiscount = TotalDiscount;
                NewR.InvoiceDate = InvDate;
                NewR.Year = Convert.ToInt16(InvDate.Year);
                NewR.Period = Convert.ToInt16(InvDate.Month);
                NewR.PayedByCustomer = Payed;
                NewR.Gross = Sales + SalesTax;
                NewR.Pending = (NewR.Gross - Payed);
                NewR.CstnoID = CstNo;
               
                try
                {
                    NewR.OrdNoID = OrdNo;
                }
                catch (Exception)
                {
                    NewR.OrdNoID = null;
                }
                

                dt.Rows.Add(NewR);
                adpt.Update(dt);
                dt.Dispose(); adpt.Dispose();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error creating the Invoice. Please, try again! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            

        }

        //Create InvoiceDet ; Developer; Dennis Vélez ; Date: 07-02-18
        internal static bool CreateInvoiceDet(List<EntitiesClass.InvoiceDet> InvoiceDet_, long InvNo)
        {

            try
            {
               
                DataModel.IISM_Dataset.InvoiceDetDataTable dt = new DataModel.IISM_Dataset.InvoiceDetDataTable();
                DataModel.IISM_DatasetTableAdapters.InvoiceDetTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceDetTableAdapter();
                adpt.Fill(dt);
                DataModel.IISM_Dataset.InvoiceDetRow NewR = dt.NewInvoiceDetRow();
                
                foreach (var item in InvoiceDet_)
                {
                    NewR.InvoiceID = Convert.ToInt32(InvNo);
                    NewR.ProdNoID = item.ProdNo;
                    NewR.WhID = item.WhID;
                    NewR.Units = (double) item.Units;
                    NewR.Sales = item.Sales;
                    NewR.UnitsWithOffer = (int) item.UnitsWithOffers;
                    NewR.SalesWithOffer = item.SalesWithOffers;
                    NewR.Discount = item.Discount;
                    NewR.SalesTax = item.SalesTax;
                    NewR.TotalSales = item.TotalSales;
                    NewR.TotalUnits = (int) NewR.Units + NewR.UnitsWithOffer;
                    NewR.Gross = NewR.TotalSales + NewR.SalesTax;
                    dt.Rows.Add(NewR.ItemArray);
                }
                adpt.Update(dt);
                dt.Dispose(); adpt.Dispose();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating the InvoiceDet. Please, notify to the administrator! InvoiceId: " + InvNo.ToString("0000") + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }






        }


        //Method to Update Invoice Payments and InvoicePayments / Developer: Dennis Vélez / Date:07-08-18
        public static bool UpdatePaymentHistory(long InvNo, decimal NewPayment, DateTime TransDate, bool IsTheFirstTransaction, string OrdNo)
        {

            decimal PrevPayment = 0; decimal Gross = 0; 

            DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);
            IEnumerable<IISM_Dataset.InvoiceRow> resultset = dt.Where(x => x.InvoiceID == InvNo);

            DataModel.IISM_Dataset.InvoicePaymentsDataTable dt2 = new DataModel.IISM_Dataset.InvoicePaymentsDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoicePaymentsTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.InvoicePaymentsTableAdapter();
            adpt2.Fill(dt2);
            DataModel.IISM_Dataset.InvoicePaymentsRow NewR = dt2.NewInvoicePaymentsRow();

            try
            {
                var qry = from i in dt where i.InvoiceID == InvNo select i;

                foreach (var item in qry)
                {
                    PrevPayment = item.PayedByCustomer;
                    Gross = item.Gross;
                }

                //Update PayedByCustomer + Pending (Invoice)
                if (!IsTheFirstTransaction)
                { 
                    foreach (var row in resultset)
                    {
                        row.PayedByCustomer = NewPayment + PrevPayment;
                        row.Pending = Gross - row.PayedByCustomer;
                        row.OrdNoID = OrdNo;
                    }
                    adpt.Update(dt);
                }
                //Insert table InvoicePayments

                var qry2 = from i in dt2 where i.InvoiceID == InvNo select i;

                NewR.InvoiceID = (int) InvNo;
                 if( IsTheFirstTransaction)
                {
                    NewR.AutoNumber = 1;
                    NewR.PrevPayment = 0;
                }
                 else
                {
                    NewR.AutoNumber = qry2.Max(p => p.AutoNumber) + 1;
                    NewR.PrevPayment = PrevPayment;
                }
                                 
                NewR.Gross = Gross;
                NewR.LastPayment = NewPayment;
                NewR.TransDate = TransDate;

                dt2.Rows.Add(NewR);
                adpt2.Update(dt2);
                dt.Dispose(); dt2.Dispose(); adpt.Dispose(); adpt2.Dispose();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: Update Payments by Customer. Please, notify to the administrator! InvoiceId: " + InvNo.ToString("0000") + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dt.Dispose(); dt2.Dispose(); adpt.Dispose(); adpt2.Dispose();
                return false;
            }

            

        }

        #endregion




    #region "EditInvoice"

        //Load Invoice Header (Edit Invoice Windows) Dennis Vélez / 07-07-18
        internal static void LoadInvoiceHeader(ref EntitiesClass.Invoice InvNoClass, long InvNo)
        {

            DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.CustomersDataTable dt2 = new DataModel.IISM_Dataset.CustomersDataTable();
            DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
            adpt2.Fill(dt2);

            DataModel.IISM_Dataset.CorporationDataTable dt3 = new DataModel.IISM_Dataset.CorporationDataTable();
            DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter adpt3 = new DataModel.IISM_DatasetTableAdapters.CorporationTableAdapter();
            adpt3.Fill(dt3);

            try
            {
                var qry = from i in dt
                          join custno in dt2 on i.CstnoID equals custno.CstnoID
                          join Corp in dt3 on i.CorpID equals Corp.CorpID
                          where i.InvoiceID == InvNo
                          select new { i.InvoiceID, i.CstnoID, custno.CstName, i.CorpID, Corp.CorpDesc, i.OrdNoID, i.InvoiceDate, i.SalesTax, i.Sales, i.TotalDiscount, i.PayedByCustomer, i.Pending };

                foreach (var item in qry)
                {
                    InvNoClass.InvNo = item.InvoiceID;
                    InvNoClass.CustNo = item.CstnoID;
                    InvNoClass.CustDesc = item.CstName;
                    InvNoClass.CorpNo = item.CorpID;
                    InvNoClass.CorpDesc = item.CorpDesc;
                    InvNoClass.OrderNo = item.OrdNoID;
                    InvNoClass.Date = item.InvoiceDate;
                    InvNoClass.SalesTax = item.SalesTax;
                    InvNoClass.Net = item.Sales;
                    InvNoClass.Gross = item.Sales + item.SalesTax;
                    InvNoClass.TotalDiscount = item.TotalDiscount;
                    InvNoClass.PendingPayment = item.Pending;
                    InvNoClass.PayedByCustNo = item.PayedByCustomer;

                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error Loading Invoice Header: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                      
            dt.Dispose();adpt.Dispose(); dt2.Dispose();dt3.Dispose();adpt2.Dispose();adpt3.Dispose();
        }


        //Load InvoiceDeta (Edit Invoice Windows) Dennis Vélez / 07-08-18
        internal static void LoadEditInvoiceDetail( ref List<EntitiesClass.InvoiceDet> InvDet_, long InvNoId)
        {
            DataModel.IISM_Dataset.InvoiceDetDataTable dt = new DataModel.IISM_Dataset.InvoiceDetDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceDetTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceDetTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.ProductsDataTable dtProd = new IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adptProd = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adptProd.Fill(dtProd);

            DataModel.IISM_Dataset.WarehouseDataTable dtWH = new DataModel.IISM_Dataset.WarehouseDataTable();
            DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adptWH = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
            adptWH.Fill(dtWH);

            try
            {
                var qry = from i in dt
                          join prod in dtProd on i.ProdNoID equals prod.ProdNoID
                          join wh in dtWH on i.WhID equals wh.WhID
                          where i.InvoiceID == InvNoId

                          select new { i.ProdNoID, prod.ProdName, i.WhID, wh.WhDesc, i.Units, i.Sales, i.UnitsWithOffer, i.SalesWithOffer, i.Discount, i.SalesTax, i.TotalUnits, i.TotalSales, i.Gross };

                foreach (var item in qry)
                {
                    InvDet_.Add(new EntitiesClass.InvoiceDet
                    {
                        ProdNo = item.ProdNoID,
                        ProdName = item.ProdName,
                        WhID = item.WhID,
                        WhName = item.WhDesc,
                        Units = Convert.ToDecimal(item.Units),
                        Sales = item.Sales,
                        UnitsWithOffers = item.UnitsWithOffer,
                        SalesWithOffers = item.SalesWithOffer,
                        Discount = item.Discount,
                        SalesTax = item.SalesTax,
                        TotalUnits = item.TotalUnits,
                        TotalSales = item.TotalSales,
                        Gross = item.Gross
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Loading Invoice Details: " + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        

            dt.Dispose();adpt.Dispose();
            adptProd.Dispose(); dtProd.Dispose();
            adptWH.Dispose(); dtWH.Dispose();

        }




        #endregion

        //Load Invoices ComboBox / Developer: Dennis Vélez / Date: 07-06-18
        public static void LoadInvoiceCombobox(ref ComboBox cmb, int Year, int Month, bool IsCustomerCombobox, int? cstno)
        {

            DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.CustomersDataTable dt2 = new DataModel.IISM_Dataset.CustomersDataTable();
            DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
            adpt2.Fill(dt2);

            
            if (IsCustomerCombobox)
            {

               var CstQry = (from c in dt2
                              join i in dt on c.CstnoID equals i.CstnoID
                              where i.Year == Year && i.Period == Month
                              orderby c.CstName, i.InvoiceDate
                              select new
                              {
                                  c.CstnoID,
                                  CstName= c.CstnoID.ToString("0000") + " - " + c.CstName
                              }).Distinct();

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = CstQry;
                    cmb.DisplayMemberPath = "CstName";
                    cmb.SelectedValuePath = "CstnoID";
                }

            }
            else
            {

                var InvQry = (from c in dt2
                              join i in dt on c.CstnoID equals i.CstnoID
                              where i.Year == Year && i.Period == Month && i.CstnoID==cstno
                              orderby c.CstName, i.InvoiceDate
                              select new
                              {
                                  i.InvoiceID,
                                  InvoiceDate= "InvNo: " + i.InvoiceID.ToString("0000") + "  Date: " + i.InvoiceDate.Date.ToShortDateString()
                              }).ToList();

                if (cmb.Items.Count == 0)
                {
                    cmb.ItemsSource = InvQry;
                    cmb.DisplayMemberPath = "InvoiceDate";
                    cmb.SelectedValuePath = "InvoiceID";
                }


            }

            dt.Dispose();dt2.Dispose(); adpt.Dispose();adpt2.Dispose();


        }



        public static void LoadCustomersComboBox(ref ComboBox cmb, int Year, int Month, int LastMonth)
        {

            DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.CustomersDataTable dt2 = new DataModel.IISM_Dataset.CustomersDataTable();
            DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
            adpt2.Fill(dt2);

            var qry = (from i in dt
                      join c in dt2 on i.CstnoID equals c.CstnoID
                      where i.Year == Year &&
                      (i.Period >= Month && i.Period <= LastMonth)
                      orderby c.CstName
                      select new
                      {
                          i.CstnoID, CstName = i.CstnoID.ToString("000") + " - " + c.CstName
                      }).Distinct();

            if (cmb.Items.Count == 0)
            {
                cmb.ItemsSource = qry;
                cmb.DisplayMemberPath = "CstName";
                cmb.SelectedValuePath = "CstnoID";
            }


            dt.Dispose(); dt2.Dispose(); adpt.Dispose(); adpt2.Dispose();


        }


        //Report to DataGrid - Sales Report ; Developer: Dennis Vélez ; Date: 07-11-18
        public static void SalesReport( int Year, int FirstMonth, int LastMonth, int CstNo)
        {

            DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);

            DataModel.IISM_Dataset.CustomersDataTable dt2 = new DataModel.IISM_Dataset.CustomersDataTable();
            DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
            adpt2.Fill(dt2);

            Classes.FillDataGridcs._lst.Clear();
            if (!(CstNo == 0))
            {
                //Filtered by Customer
                var qry = (from i in dt
                           join c in dt2 on i.CstnoID equals c.CstnoID
                           where i.Year == Year && i.CstnoID==CstNo &&
                           (i.Period >= FirstMonth && i.Period <= LastMonth)
                           orderby i.InvoiceDate
                           select new
                           {
                               InvoiceID = i.InvoiceID.ToString("00000"),
                               i.OrdNoID,
                               CstnoID = i.CstnoID.ToString("0000"),
                               c.CstName,
                               InvoiceDate = i.InvoiceDate.ToShortDateString(),
                               Sales = String.Format("{0:C}", i.Sales),
                               SalesTax = String.Format("{0:C}", i.SalesTax),
                               Gross = String.Format("{0:C}", i.Gross),
                               TotalDiscount = String.Format("{0:C}", i.TotalDiscount),
                               PayedByCustomer = String.Format("{0:C}", i.PayedByCustomer),
                               Pending = String.Format("{0:C}", i.Pending),
                           });

                Classes.FillDataGridcs.DataGridName = "Sales Report by Customer: " + Year + "| From Month: " + FirstMonth.ToString("00") + " to Month: " + LastMonth.ToString("00");
                foreach (var item in qry)
                {
                    Classes.FillDataGridcs.AddElements(item);
                }

            }
            else
            {
                //  All Customers
                                
                var qry = (from i in dt
                           join c in dt2 on i.CstnoID equals c.CstnoID
                           where i.Year == Year &&
                           (i.Period >= FirstMonth && i.Period <= LastMonth)
                           orderby i.InvoiceDate
                           select new
                           {
                               InvoiceID = i.InvoiceID.ToString("00000"),
                               i.OrdNoID,
                               CstnoID = i.CstnoID.ToString("0000"),
                               c.CstName,
                               InvoiceDate = i.InvoiceDate.ToShortDateString(),
                               Sales = String.Format("{0:C}", i.Sales),
                               SalesTax = String.Format("{0:C}", i.SalesTax),
                               Gross = String.Format("{0:C}", i.Gross),
                               TotalDiscount = String.Format("{0:C}", i.TotalDiscount),
                               PayedByCustomer = String.Format("{0:C}", i.PayedByCustomer),
                               Pending = String.Format("{0:C}", i.Pending),
                           });

                Classes.FillDataGridcs.DataGridName = "Sales Report: " + Year + " | From Month: " + FirstMonth.ToString("00") + " to Month: " + LastMonth.ToString("00");
                foreach (var item in qry)
                {
                    Classes.FillDataGridcs.AddElements(item);
                }

            }

           
            dt.Dispose(); dt2.Dispose(); adpt.Dispose(); adpt2.Dispose();
            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();

        }


        //Report to DataGrid - Sales Report Invoice Details; Developer: Dennis Vélez ; Date: 07-11-18
        public static void SalesReportInvoiceDetails(int Year, int FirstMonth, int LastMonth, int CstNo)
        {

            DataModel.IISM_Dataset.InvoiceDataTable dt = new DataModel.IISM_Dataset.InvoiceDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter adpt = new DataModel.IISM_DatasetTableAdapters.InvoiceTableAdapter();
            adpt.Fill(dt);


            DataModel.IISM_Dataset.CustomersDataTable dt2 = new DataModel.IISM_Dataset.CustomersDataTable();
            DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter adpt2 = new DataModel.IISM_DatasetTableAdapters.CustomersTableAdapter();
            adpt2.Fill(dt2);

            DataModel.IISM_Dataset.WarehouseDataTable dt3 = new DataModel.IISM_Dataset.WarehouseDataTable();
            DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter adpt3 = new DataModel.IISM_DatasetTableAdapters.WarehouseTableAdapter();
            adpt3.Fill(dt3);

            DataModel.IISM_Dataset.ProductsDataTable dt4 = new DataModel.IISM_Dataset.ProductsDataTable();
            DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter adpt4 = new DataModel.IISM_DatasetTableAdapters.ProductsTableAdapter();
            adpt4.Fill(dt4);

            DataModel.IISM_Dataset.InvoiceDetDataTable dt5 = new DataModel.IISM_Dataset.InvoiceDetDataTable();
            DataModel.IISM_DatasetTableAdapters.InvoiceDetTableAdapter adpt5 = new DataModel.IISM_DatasetTableAdapters.InvoiceDetTableAdapter();
            adpt5.Fill(dt5);

            Classes.FillDataGridcs._lst.Clear();
            if (!(CstNo == 0))
            {
                //Filtered by Customer
                var qry = (from id in dt5
                           join i in dt on id.InvoiceID equals i.InvoiceID
                           join c in dt2 on i.CstnoID equals c.CstnoID
                           join p in dt4 on id.ProdNoID equals p.ProdNoID
                           join wh in dt3 on id.WhID equals wh.WhID
                           
                           where i.Year == Year && i.CstnoID == CstNo &&
                           (i.Period >= FirstMonth && i.Period <= LastMonth)
                           orderby i.InvoiceDate
                           select new
                           {
                               InvoiceID = i.InvoiceID.ToString("00000"),
                               i.OrdNoID,
                               CstnoID = i.CstnoID.ToString("0000"),
                               c.CstName,
                               ProdNoId = id.ProdNoID.ToString("000"),
                               p.ProdName,
                               WhId = id.WhID.ToString("00"),
                               wh.WhDesc,
                               InvoiceDate = i.InvoiceDate.ToShortDateString(),
                               Units = (decimal) id.Units,
                               Sales = String.Format("{0:C}", id.Sales),
                               UnitsWithOff = (decimal)id.UnitsWithOffer,
                               SalesWithOff = String.Format("{0:C}", id.SalesWithOffer),
                               Discount = String.Format("{0:C}", id.Discount),
                               TotalUnits = (decimal)id.TotalUnits,
                               TotalSales = String.Format("{0:C}", id.TotalSales),
                               SalesTax = String.Format("{0:C}", id.SalesTax),
                               Gross = String.Format("{0:C}", id.Gross)

                           });

                Classes.FillDataGridcs.DataGridName = "Sales Report by Customer (Invoices Details): " + Year + "| From Month: " + FirstMonth.ToString("00") + " to Month: " + LastMonth.ToString("00");
                foreach (var item in qry)
                {
                    Classes.FillDataGridcs.AddElements(item);
                }

            }
            else
            {
                //  All Customers

                var qry = (from id in dt5
                           join i in dt on id.InvoiceID equals i.InvoiceID
                           join c in dt2 on i.CstnoID equals c.CstnoID
                           join p in dt4 on id.ProdNoID equals p.ProdNoID
                           join wh in dt3 on id.WhID equals wh.WhID
                           where i.Year == Year &&
                           (i.Period >= FirstMonth && i.Period <= LastMonth)
                           orderby i.InvoiceDate
                           select new
                           {
                               InvoiceID = i.InvoiceID.ToString("00000"),
                               i.OrdNoID,
                               CstnoID = i.CstnoID.ToString("0000"),
                               c.CstName,
                               ProdNoId = id.ProdNoID.ToString("000"),
                               p.ProdName,
                               WhId = id.WhID.ToString("00"),
                               wh.WhDesc,
                               InvoiceDate = i.InvoiceDate.ToShortDateString(),
                               Units = (decimal)id.Units,
                               Sales = String.Format("{0:C}", id.Sales),
                               UnitsWithOff = (decimal)id.UnitsWithOffer,
                               SalesWithOff = String.Format("{0:C}", id.SalesWithOffer),
                               Discount = String.Format("{0:C}", id.Discount),
                               TotalUnits = (decimal)id.TotalUnits,
                               TotalSales = String.Format("{0:C}", id.TotalSales),
                               SalesTax = String.Format("{0:C}", id.SalesTax),
                               Gross = String.Format("{0:C}", id.Gross)
                           });

                Classes.FillDataGridcs.DataGridName = "Sales Report (Invoices Details): " + Year + " | From Month: " + FirstMonth.ToString("00") + " to Month: " + LastMonth.ToString("00");
                foreach (var item in qry)
                {
                    Classes.FillDataGridcs.AddElements(item);
                }

            }


            dt.Dispose(); dt2.Dispose(); adpt.Dispose(); adpt2.Dispose(); dt3.Dispose(); adpt3.Dispose(); dt4.Dispose();adpt4.Dispose(); dt5.Dispose(); adpt5.Dispose();
            IISM.DataGrid.TemplateDataGrid OpenW = new IISM.DataGrid.TemplateDataGrid();
            OpenW.Show();

        }
        ~InvoiceClss()
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
