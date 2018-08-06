using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IISM.Classes
{
    public class CheckControls
    {

        //Clear Text Box / Dennis Vélez / Date: March 2018
        public static void ClearTextBox(ref TextBox txt1)
        {
            txt1.Text = null;
        }
        //Clear Text Box / Dennis Vélez / Date: March 2018
        public static void ClearTextBox(ref TextBox txt1, ref TextBox txt2)
        {
            txt1.Text = null;
            txt2.Text = null;
        }
        //Clear Text Box / Dennis Vélez / Date: March 2018
        public static void ClearTextBox(ref TextBox txt1, ref TextBox txt2, ref TextBox txt3)
        {
            txt1.Text = null;
            txt2.Text = null;
            txt3.Text = null;
        }
        //Clear Text Box / Dennis Vélez / Date: March 2018
        public static void ClearTextBox(ref TextBox txt1, ref TextBox txt2, ref TextBox txt3, ref TextBox txt4)
        {
            txt1.Text = null;
            txt2.Text = null;
            txt3.Text = null;
            txt4.Text = null;
        }


        //Verify if Textbox IsNull / Dennis Vélez / March 2018
        public static bool VerifyIfTextboxIsNull(TextBox txt1)
        {

            if (string.IsNullOrEmpty(txt1.Text))
            {
                MessageBox.Show("Please, fill all Textboxes!", "Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }
        //Verify if Textbox IsNull / Dennis Vélez / March 2018
        public static bool VerifyIfTextboxIsNull(TextBox txt1, TextBox txt2)
        {

            if (string.IsNullOrEmpty(txt1.Text) || string.IsNullOrEmpty(txt2.Text))
            {
                MessageBox.Show("Please, fill all Textboxes!", "Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }
        //Verify if Textbox IsNull / Dennis Vélez / March 2018
        public static bool VerifyIfTextboxIsNull(TextBox txt1, TextBox txt2, TextBox txt3)
        {

            if (string.IsNullOrEmpty(txt1.Text) || string.IsNullOrEmpty(txt2.Text) || string.IsNullOrEmpty(txt3.Text))
            {
                MessageBox.Show("Please, fill all Textboxes!", "Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }
        //Verify if Textbox IsNull / Dennis Vélez / March 2018
        public static bool VerifyIfTextboxIsNull(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4)
        {

            if (string.IsNullOrEmpty(txt1.Text) || string.IsNullOrEmpty(txt2.Text) || string.IsNullOrEmpty(txt3.Text) || string.IsNullOrEmpty(txt4.Text))
            {
                MessageBox.Show("Please, fill all Textboxes!", "Atention", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }


        //Review if string is Numeric; Dennis Vélez ; Date: 06-11-18
        public static bool IsNumeric (string A)
        {
            bool r = float.TryParse(A, out var _);
            if (r)
            {
                return true;
            }
            MessageBox.Show("Please, fill a texbox with a numeric value!", "Atention", MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }
  

    }
}
