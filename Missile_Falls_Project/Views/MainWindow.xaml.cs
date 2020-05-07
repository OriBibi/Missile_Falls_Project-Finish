using BE;
using BL;
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
using System.Reflection;
using Missile_Falls_Project.ViewModels;
using log4net;

namespace Missile_Falls_Project.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MainViewModel MainViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            newReport.ReportFormVm = MainViewModel.NewReportFormVm;
            MapView.MapVm = MainViewModel.MapVm;
            DataContext = MainViewModel;
            
            Closing += MainView_Closing;

        }
        private void MainView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                /*
                    if (((MainViewModel)(this.DataContext)).Data.IsModified)
                    if (!((MainViewModel)(this.DataContext)).PromptSaveBeforeExit())
                    {
                        e.Cancel = true;
                        return;
                    }
                */
                Log.Info("Closing App");
            }

            
        

        public void SelectedTabChange(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            PictureView picture = new PictureView();

            if (index == 0)
            {
                picture.Visibility = Visibility.Visible;
                newReport.Visibility = Visibility.Collapsed;
                MapView.Visibility = Visibility.Collapsed;

                // MapView.Visibility = Visibility.Visible;
                // GraphView.Visibility = Visibility.Collapsed;
                // ChooseExplosionsView.Visibility = Visibility.Collapsed;
            }
            else if (index == 1)
            {
                picture.Visibility = Visibility.Collapsed;

                // GraphView.Visibility = Visibility.Visible;
                // ChooseExplosionsView.Visibility = Visibility.Visible;            
                newReport.Visibility = Visibility.Visible;
                MapView.Visibility = Visibility.Visible;

            }
            else if (index == 2)
            {
                picture.Visibility = Visibility.Collapsed;
                newReport.Visibility = Visibility.Collapsed;
                MapView.Visibility = Visibility.Collapsed;

            }
        }

        
    }
}
