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
                lstCredentials.Clear();
                lstCredentials = JsonConvert.DeserializeObject<List<Credentials>>(System.IO.File.ReadAllText(path));
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
                string contentsToWriteToFile = JsonConvert.SerializeObject(lstCredentials.ToArray(), Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(path, contentsToWriteToFile);

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message + " Save User Error.\n");
            }
        }

        private void fillCredentialListFromDataGrid()
        {
            lstCredentials.Clear();
            foreach ( DataGridRow row in GetDataGridRows(dataGrid))
            {
                lstCredentials.Add((Credentials)row.DataContext);
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

        private void saveFunctionality()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.FileName = "test123";
                saveFileDialog.DefaultExt = ".json"; // Default file extension
                saveFileDialog.Filter = "Json Files (.json)|*.json";
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

        private void loadFunctionality()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.DefaultExt = ".json"; // Default file extension
                openFileDialog.Filter = "Json Files (.json)|*.json";
                if (openFileDialog.ShowDialog() == true)
                {
                    loadFormFile(openFileDialog.FileName);
                    dataGrid.DataContext = null;
                    dataGrid.DataContext = lstCredentials;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "Event Handlers"
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch(((MenuItem)sender).Name)
                {
                    case "mnuSaveFile":
                        saveFunctionality();
                        break;
                    case "mnuLoadFile":
                        loadFunctionality();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
