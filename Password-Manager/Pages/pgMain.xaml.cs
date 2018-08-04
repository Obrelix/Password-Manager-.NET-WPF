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
        List<Credentials> lstCredentials = new List<Credentials>();
        public pgMain(MainWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
            //crList.Add(new Credentials("Obrelix", "Ovelixg13", "Winbank", "Den exei lefta"));
            dataGrid.DataContext = lstCredentials;
        }

        #region "Methods"
        private void loadFormFile(string path)
        {
            try
            {
                dataGrid.BeginInit();
                lstCredentials.Clear();
                string strData = File.ReadAllText(path);
                strData = StringCipher.Decrypt(strData, MainWindow.password);
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
                contentsToWriteToFile = StringCipher.Encrypt(contentsToWriteToFile, MainWindow.password);
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



        #endregion

    }
}
