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
    /// Interaction logic for MainMasterFile.xaml
    /// </summary>
    public partial class MainMasterFile : Window
    {
        public MainMasterFile()
        {
            InitializeComponent();
            btmServiceClass.Visibility = Visibility.Collapsed;
            btmServiceClass.IsEnabled = false;
            btmOfferClass.Visibility = Visibility.Collapsed;
            btmOfferClass.IsEnabled = false;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

     

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {

            IISM.MainWindow OpenMainW= new IISM.MainWindow();
            OpenMainW.Show();
            this.Close();


        }

        private void btnWH_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 1;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void btmBussinesType_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 2;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void btmServiceClass_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 3;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void btmProdCategory_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 4;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void btmCorp_Click(object sender, RoutedEventArgs e)
        {

               
            GlobalVar.GlobalValue = 6;
            GlobalVar.CallEditForm();
            this.Close();

        }

        private void btmSuppliers_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 7;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 8;
            GlobalVar.CallEditForm();
            this.Close();
        }

        private void btmProducts_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalValue = 9;
            GlobalVar.CallEditForm();
            this.Close();
        }


    }
}
