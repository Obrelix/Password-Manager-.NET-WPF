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
            if (txtPassword.Password.Length >= 8)
            {
                password = Gtools.hashFromString(Gtools.encodeMix(txtPassword.Password, txtPassword.Password));

                parentWindow.changePage(page.AddText);
                       
            }
            else MessageBox.Show("The Username must contain at least 5 characters " + Environment.NewLine +
                                    "The Password must contain at least 8 characters",
                                    "Log in Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

    }
}
