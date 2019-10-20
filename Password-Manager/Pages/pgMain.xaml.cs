using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for pgMain.xaml
    /// </summary>
    public partial class pgMain : Page
    {
        MainWindow parentWindow;
        private string password;
        List<Credentials> lstCredentials = new List<Credentials>();
        public pgMain(MainWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
            dataGrid.DataContext = lstCredentials;
            txtPassword.Visibility = Visibility.Visible;
            txtDummyPassword.Visibility = Visibility.Collapsed;
        }

        #region "Methods"

        private void loadFormFile(string path)
        {
            try
            {
                clearFunctionality();
                dataGrid.BeginInit();
                lstCredentials.Clear();
                string strData = File.ReadAllText(path);
                strData = StringCipher.Decrypt(strData, password);
                lstCredentials = JsonConvert.DeserializeObject<List<Credentials>>(strData);
                dataGrid.DataContext = lstCredentials;
                dataGrid.EndInit();
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message + " Load User Error.\n");

            }
        }

        private void saveToFile(string path)
        {
            try
            {
                dataGrid.CommitEdit();
                string contentsToWriteToFile = JsonConvert.SerializeObject(lstCredentials.ToArray(), Newtonsoft.Json.Formatting.Indented);
                contentsToWriteToFile = StringCipher.Encrypt(contentsToWriteToFile, password);
                File.WriteAllText(path, contentsToWriteToFile);


            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message + " Save User Error.\n");
            }
        }

        public void clearFunctionality()
        {
            try
            {
                dataGrid.BeginInit();
                lstCredentials.Clear();
                dataGrid.DataContext = lstCredentials;
                dataGrid.EndInit();
            }
            catch(Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message + " Save User Error.\n");
            }
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            IEnumerable itemsSource = grid.ItemsSource ;
            if (itemsSource == null) yield return null;
            foreach (var item in itemsSource)
            {
                DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(item);
                if (null != row) yield return row;
            }
        }
        
        public void saveFunctionality()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.FileName = "data";
                saveFileDialog.DefaultExt = ".txt"; // Default file extension
                saveFileDialog.Filter = "Document Files (.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    saveToFile(saveFileDialog.FileName);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void loadFunctionality()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.DefaultExt = ".txt"; // Default file extension
                openFileDialog.Filter = "Document Files (.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    loadFormFile(openFileDialog.FileName);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void wndGeneratorShow()
        {
            try
            {
                wndGeneratePassword wnd = new wndGeneratePassword();
                wnd.Show();
                wnd.Focus();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region "Event Handlers"

        private void dataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                double width = 0;
                foreach (DataGridColumn column in dataGrid.Columns)
                {
                    width += column.ActualWidth;
                }
                if (width < 801) parentWindow.changeWidth(width + 50);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            pnlShow.Background = Brushes.Tomato;
            txtPassword.Visibility = Visibility.Collapsed;
            txtDummyPassword.Visibility = Visibility.Visible;
            txtDummyPassword.Text = txtPassword.Password;
        }

        private void imgShow_MouseLeave(object sender, MouseEventArgs e)
        {
            txtPassword.Visibility = Visibility.Visible;
            txtDummyPassword.Visibility = Visibility.Collapsed;
            txtDummyPassword.Text = string.Empty;
            pnlShow.Background = Brushes.Transparent;
        }

        public void btnLogON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPassword.Password.Length >= 8)
                {
                    password = Gtools.encodeMix(txtPassword.Password, txtPassword.Password);
                    pnlPassword.Visibility = Visibility.Collapsed;
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

        #endregion

    }
}
