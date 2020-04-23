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

namespace Missile_Falls_Project.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectedTabChange(object sender, RoutedEventArgs e)
        {
            NewReport newReport = new NewReport();
            int index = int.Parse(((Button)e.Source).Uid);

           // GridCursor.Margin = new Thickness((150 * index), 0, 0, 0);

            if (index == 0)
            {

                newReport.Visibility = Visibility.Visible;
               // MapView.Visibility = Visibility.Visible;
               // GraphView.Visibility = Visibility.Collapsed;
               // ChooseExplosionsView.Visibility = Visibility.Collapsed;
            }
            else if (index == 1)
            {
               // GraphView.Visibility = Visibility.Visible;
               // ChooseExplosionsView.Visibility = Visibility.Visible;            
               // ReportFormView.Visibility = Visibility.Collapsed;
                //MapView.Visibility = Visibility.Collapsed;
            }
        
    }
    }
}
