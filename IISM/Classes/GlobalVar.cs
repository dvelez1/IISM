using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISM.Classes
{
    public static class GlobalVar
    {
        static int _globalvalue;
        static bool _CreateMF;
        static long _InvNoId; // Invoice Number to Edit
        public static int GlobalValue
        {

            get
            {
                return _globalvalue;
            }
            set
            {
                _globalvalue = value;
            }

        }

        public static long InvNoId
        {

            get
            {
                return _InvNoId;
            }
            set
            {
                _InvNoId = value;
            }

        }


        public static bool CreateMFAction
        {
            get
            {
                return _CreateMF;
            }
            set
            {
                _CreateMF = value;
            }
        }


        public static void CallEditForm()
        {
            IISM.MF_Forms.MF_WH OpenW = new IISM.MF_Forms.MF_WH();
            OpenW.Show();
            
        }


    }
}
