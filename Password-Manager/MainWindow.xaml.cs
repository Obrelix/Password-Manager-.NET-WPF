using System;
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
using Password_Manager.Pages;

namespace Password_Manager
{
    public enum page : byte
    {
        logOn = 1,
        AddFiles = 2,
        Main = 3
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        page frameState;
        pgLogin logOn;
        pgMain pgDataGrid;
        public static string savePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Secure_Log";
        public static string saveFile = savePath + "\\report.json";
        public static string password { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(@savePath))
                Directory.CreateDirectory(@savePath);
            logOn = new pgLogin(this);
            pgDataGrid = new pgMain(this);
            changePage(page.logOn);
        }
        #region General Declaretion


        #endregion


        #region Function / Procedures

        public void changeHeight(double height)
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new Action<double>(changeHeight), new object[] { height });
                    return;
                }
                this.Height = height;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void changeWidth(double width)
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new Action<double>(changeWidth), new object[] { width });
                    return;
                }
                this.Width = width;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void changePage(page page)
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new Action<page>(changePage), new object[] { page });
                    return;
                }
                switch (page)
                {
                    case page.logOn:
                        frame.Navigate(logOn); frameState = page.logOn;
                        break;
                    case page.AddFiles:
                        //frame.Navigate(addfile); frameState = page.AddFiles;
                        break;
                    case page.Main:
                        frame.Navigate(pgDataGrid); frameState = page.Main;
                        break;
                    default: break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //frame.Source = new Uri("/Hide-Your-Files-Inside-a-Picture;component/Pages/pgAddFiles.xaml", UriKind.Relative);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void mnuDownload_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Obrelix/Password-Manager-.NET-WPF");
        }

        private void mnuLogIn_Click(object sender, RoutedEventArgs e)
        {
            changePage(page.logOn);
        }

        private void mnuReport_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Obrelix/Password-Manager-.NET-WPF/issues");
        }

        private void frame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void disableMenuItems()
        {
            switch (frameState)
            {
                case page.logOn:
                    mnuClear.Visibility = Visibility.Hidden;
                    mnuLoadFile.Visibility = Visibility.Hidden;
                    mnuSaveFile.Visibility = Visibility.Hidden;
                    mnuGenerate.Visibility = Visibility.Hidden;
                    break;
                case page.Main:
                    mnuClear.Visibility = Visibility.Visible;
                    mnuLoadFile.Visibility = Visibility.Visible;
                    mnuSaveFile.Visibility = Visibility.Visible;
                    mnuGenerate.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            disableMenuItems();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (((MenuItem)sender).Name)
                {
                    case "mnuSaveFile":
                        pgDataGrid.saveFunctionality();
                        break;
                    case "mnuLoadFile":
                        pgDataGrid.loadFunctionality();
                        break;
                    case "mnuGenerate":
                        pgDataGrid.wndGeneratorShow();
                        break;
                    case "mnuLogIn":
                        pgDataGrid.clearFunctionality();
                        changePage(page.logOn);
                        break;
                    case "mnuClear":
                        pgDataGrid.clearFunctionality();
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

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.S:
                        if (Keyboard.IsKeyDown(Key.LeftCtrl)) pgDataGrid.saveFunctionality();
                        break;
                    case Key.F:
                        if (Keyboard.IsKeyDown(Key.LeftCtrl)) pgDataGrid.loadFunctionality();
                        break;
                    case Key.G:
                        if (Keyboard.IsKeyDown(Key.LeftCtrl)) pgDataGrid.wndGeneratorShow();
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
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mnuHelp.Margin = new Thickness((this.Width - 100), 0, 0, 0);
            mnuHelp.Margin = new Thickness(0);
            //mnuMain.Height = 25;
        }

        #endregion
        
    }


}
