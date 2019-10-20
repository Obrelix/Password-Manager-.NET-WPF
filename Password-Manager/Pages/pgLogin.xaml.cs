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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager.Pages
{
    /// <summary>
    /// Interaction logic for pgLogin.xaml
    /// </summary>
    public partial class pgLogin : Page
    {

        MainWindow parentWindow;
        private string password;

        public pgLogin(MainWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
        }

        #region "Event Handlers"

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPassword.Password.Length >= 8)
                {
                    password = Gtools.encodeMix(txtPassword.Password, txtPassword.Password);
                    parentWindow.changePage(page.Main);

                }
                else MessageBox.Show("The Password must contain at least 8 characters",
                                        "Log in Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter) btnLogON_Click(this, new RoutedEventArgs());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            password = string.Empty;
        }

        #endregion

    }
}
