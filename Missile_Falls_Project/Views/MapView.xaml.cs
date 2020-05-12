using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.Maps;
using Missile_Falls_Project.Annotations;
using Missile_Falls_Project.ViewModels;
using Microsoft.Maps.MapControl.WPF;

namespace Missile_Falls_Project
{
    /// <summary>
    /// Interaction logic for MapUC.xaml
    /// </summary>
    public partial class MapView : UserControl
    {

        public MapView()
        {
            InitializeComponent();
            BingMap.Height = SystemParameters.PrimaryScreenHeight * 0.80;
            BingMap.Width = SystemParameters.PrimaryScreenWidth * 0.70;
        }

        public static readonly DependencyProperty MapVmProperty = DependencyProperty.Register(
            "MapVm", typeof(MapVM), typeof(MapView), new PropertyMetadata(default(MapVM)));

        public MapVM MapVm
        {
            get { return (MapVM)GetValue(MapVmProperty); }
            set
            {
                SetValue(MapVmProperty, value);
                value.PropertyChanged += PropertyChanged;
                DataContext = MapVm;
            }
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() => DataContext = MapVm);
        }
    }
}
