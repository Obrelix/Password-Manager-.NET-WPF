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
        public page frameState;
        pgMain pgDataGrid;
        public static string savePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Secure_Log";
        public static string saveFile = savePath + "\\report.json";
        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(@savePath))
                Directory.CreateDirectory(@savePath);
            pgDataGrid = new pgMain(this);
            frame.Navigate(pgDataGrid);
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
                pgDataGrid.Focus();
                switch (page)
                {
                    case page.logOn:
                        pgDataGrid.txtPassword.Focus();
                        break;
                    case page.AddFiles:
                        //frame.Navigate(addfile); frameState = page.AddFiles;
                        break;
                    case page.Main:
                        pgDataGrid.dataGrid.Focus();
                        break;
                    default: break;
                }

                frameState = page;
                disableMenuItems();
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
                    mnuClear.Visibility = Visibility.Collapsed;
                    mnuLoadFile.Visibility = Visibility.Collapsed;
                    mnuSaveFile.Visibility = Visibility.Collapsed;
                    mnuGenerate.Visibility = Visibility.Collapsed;
                    pgDataGrid.pnlPassword.Visibility = Visibility.Visible;
                    pgDataGrid.dataGrid.Visibility = Visibility.Collapsed;
                    break;
                case page.Main:
                    mnuClear.Visibility = Visibility.Visible;
                    mnuLoadFile.Visibility = Visibility.Visible;
                    mnuSaveFile.Visibility = Visibility.Visible;
                    mnuGenerate.Visibility = Visibility.Visible;
                    pgDataGrid.pnlPassword.Visibility = Visibility.Collapsed;
                    pgDataGrid.dataGrid.Visibility = Visibility.Visible;
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
                    case "mnuPassword":
                        passVisibilityChange();
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
         private void passVisibilityChange()
        {
            try
            {
                if (pgDataGrid.pnlPassword.Visibility == Visibility.Collapsed) pgDataGrid.pnlPassword.Visibility = Visibility.Visible;
                else pgDataGrid.pnlPassword.Visibility = Visibility.Collapsed;
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
                    case Key.P:
                        if (Keyboard.IsKeyDown(Key.LeftCtrl)) passVisibilityChange();
                        break;
                    case Key.Enter:
                        pgDataGrid.btnLogON_Click(this, new RoutedEventArgs());
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
