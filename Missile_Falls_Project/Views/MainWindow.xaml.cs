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
        public MainViewModel MainViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            newReport.ReportFormVm = MainViewModel.NewReportFormVm;
            MapView.MapVm = MainViewModel.MapVm;
            newPicture.NewPictureFormVM = MainViewModel.PictureFormVm;
            newGraph.GraphVm = MainViewModel.GraphVm;
            DataContext = MainViewModel;
            
            
        }
       

            
        

        public void SelectedTabChange(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            if (index == 0)
            {
                newPicture.Visibility = Visibility.Visible;
                newReport.Visibility = Visibility.Collapsed;
                MapView.Visibility = Visibility.Collapsed;

                // MapView.Visibility = Visibility.Visible;
                newGraph.Visibility = Visibility.Collapsed;
                // ChooseHitsView.Visibility = Visibility.Collapsed;
            }
            else if (index == 1)
            {
                newPicture.Visibility = Visibility.Collapsed;

                // GraphView.Visibility = Visibility.Visible;
                 newGraph.Visibility = Visibility.Collapsed;            
                newReport.Visibility = Visibility.Visible;
                MapView.Visibility = Visibility.Visible;

            }
            else if (index == 2)
            {
                newPicture.Visibility = Visibility.Collapsed;
                newReport.Visibility = Visibility.Collapsed;
                MapView.Visibility = Visibility.Collapsed;
                newGraph.Visibility = Visibility.Visible;

            }
        }

        
    }
}
