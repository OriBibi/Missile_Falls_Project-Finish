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
    /// Interaction logic for NewReport.xaml
    /// </summary>
    public partial class NewReport : UserControl, INotifyPropertyChanged
    {
        //Use Dependency Properties features and get automatically report feature changes.
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
            FileNameLabel.Content = " ";
            BitmapImage bitmap = new BitmapImage();
            
            ImageViewer1.Source = bitmap;
            //AddressTextBox.CompleteVM = new GeoLocationAutoCompleteVM();
        }

        private void SaveEnableCheck(object sender, RoutedEventArgs routedEventArgs)
        {
            int _;
            SaveButton.IsEnabled = AddressTextBox.SelectedLocation != null &&
                                   NameTextBox.Text != "" ;
        }

        private void AddressTextBox_OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is AutoCompleteBox) || e.AddedItems.Count == 0) return;
            (DataContext as NewReportFormVM).Report.Adress = ((Result)e.AddedItems[0]).Title;
            (DataContext as NewReportFormVM).Report.Latitude = ((Result)e.AddedItems[0])?.Position?[0] ?? 0;
            (DataContext as NewReportFormVM).Report.Longitude = ((Result)e.AddedItems[0])?.Position?[1] ?? 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/maps/@32.226743,34.747009,9z?hl=iw");
        }

        private void AddComments(object sender, EventArgs e)
        {
            Comments comments = new Comments();
            comments.Show();
        }

        private void AddPicture(object sender, EventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            var result = dlg.ShowDialog();
            if (result == true)
            {
                string selectedFileName = dlg.FileName;
                FileNameLabel.Content = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
        }

        private void ActionButton_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}