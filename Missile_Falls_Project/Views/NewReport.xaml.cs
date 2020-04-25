using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
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
using BE;
using Missile_Falls_Project.Controls;
using Missile_Falls_Project.ViewModels;

using QuickType;

namespace Missile_Falls_Project.Views
{
    /// <summary>
    /// Interaction logic for NewReportFormView.xaml
    /// </summary>
    public partial class NewReport : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ReportFormVmProperty = DependencyProperty.Register(
            "ReportFormVm", typeof(NewReportFormVM), typeof(NewReport), new PropertyMetadata(default(NewReportFormVM)));

        public NewReportFormVM ReportFormVm
        {
            get { return (NewReportFormVM)GetValue(ReportFormVmProperty); }
            set { SetValue(ReportFormVmProperty, value); }
        }

        public NewReport()
        {
            InitializeComponent();
            InitForm();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetForm(object sender, EventArgs e)
        {
            InitForm();
        }

        private void InitForm()
        {
            ReportFormVm = new NewReportFormVM();
            DataContext = ReportFormVm;
            SaveButton.Command = ReportFormVm.AddReportCommand;
            SaveButton.CommandParameter = ReportFormVm.FormModel;
            SaveButton.IsEnabled = false;
            ReportFormVm.PropertyChanged += (sender, args) => InitForm();
            //AddressTextBox.CompleteVM = new GeoLocationAutoCompleteVM();
        }

        private void SaveEnableCheck(object sender, RoutedEventArgs routedEventArgs)
        {
            int _;
            SaveButton.IsEnabled = AddressTextBox.SelectedLocation != null &&
                                   NameTextBox.Text != "" &&
                                   !(NoiseIntensityTextBox.Text == "0" ||
                                   !int.TryParse(NoiseIntensityTextBox.Text, out _)) &&
                                   !(NumOfExplosionsTextBox.Text == "0" ||
                                   !int.TryParse(NumOfExplosionsTextBox.Text, out _));
        }

        private void AddressTextBox_OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is AutoCompleteBox) || e.AddedItems.Count == 0) return;
            (DataContext as NewReportFormVM).Report.EventLocation = ((Result)e.AddedItems[0]).Title;
            (DataContext as NewReportFormVM).Report.Latitude = ((Result)e.AddedItems[0])?.Position?[0] ?? 0;
            (DataContext as NewReportFormVM).Report.Longitude = ((Result)e.AddedItems[0])?.Position?[1] ?? 0;
        }
    }
}