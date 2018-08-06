using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IISM.Classes;
using System.Windows;
using System.Windows.Controls;
using IISM.DataGrid;
using System.Windows.Forms;

namespace IISM.Classes
{
    public static class FillDataGridcs
    {

        static string _NameOfTheDataGrid;
        public static List<object> _lst = new List<object>() ;

        

        public static string DataGridName
        {
            get
            {
                return _NameOfTheDataGrid;
            }
            set
            {
                _NameOfTheDataGrid = value;
            }
            
        }

        //Add elements to the List. Dennis Vélez / 07-09-18
        public static void  AddElements (object a)
        {
            _lst.Add(a);
        }



    }
}
