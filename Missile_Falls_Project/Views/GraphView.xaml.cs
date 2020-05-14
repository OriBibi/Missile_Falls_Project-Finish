using Missile_Falls_Project.ViewModels;
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
using System.ComponentModel;

namespace Missile_Falls_Project
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : UserControl
    {
        public GraphView()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty GraphVmProperty = DependencyProperty.Register(
    "GraphVm", typeof(GraphVM), typeof(GraphView), new PropertyMetadata(default(GraphVM)));

        public GraphVM GraphVm
        {
            get { return (GraphVM)GetValue(GraphVmProperty); }
            set
            {
                SetValue(GraphVmProperty, value);
                value.PropertyChanged += Value_PropertyChanged;
                DataContext = GraphVm;
            }
        }

        private void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DataContext = GraphVm;
        }
    }
}
