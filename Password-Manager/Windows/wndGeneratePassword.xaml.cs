using System;
using Xceed.Wpf.Toolkit;
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
using System.Web.Security;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for wndGeneratePassword.xaml
    /// </summary>
    public partial class wndGeneratePassword : Window
    {
        public wndGeneratePassword()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string errorMsg = string.Empty;
                bool InputError = false;
                if(!((int)nmrSpecialChar.Value > 0 ) || !((int)nmrSpecialChar.Value <= (int)nmrPasswordLength.Value))
                {
                    errorMsg += "The Complexity number must be equal or smaller than the number of Digits and greater than 0." + Environment.NewLine;
                    InputError = true;
                }
                if (!((int)nmrPasswordLength.Value > 0))
                {
                    errorMsg += "The password length must be greater than 0 digits.";
                    InputError = true;
                }
                if (InputError)
                {
                    System.Windows.MessageBox.Show(errorMsg,"Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.None);
                    return;
                }
                txtPassword.Text = Membership.GeneratePassword((int)nmrPasswordLength.Value, (int)(nmrSpecialChar.Value));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtPassword.Text)) Clipboard.SetText(txtPassword.Text);
        }
    }
}
