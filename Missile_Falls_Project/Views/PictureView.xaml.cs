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
using BL;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Missile_Falls_Project.Controls;
using Missile_Falls_Project.ViewModels;

using QuickType;
using System.Drawing;
using System.IO;

namespace Missile_Falls_Project.Views
{
    /// <summary>
    /// Interaction logic for PictureView.xaml
    /// </summary>
    public partial class PictureView : UserControl
    {

        public PictureView()
        {
            InitializeComponent();
           
            DataContext = this;
        }
        //Use Dependency Properties features and get automatically report feature changes.
        public static readonly DependencyProperty NewPictureFormVmProperty = DependencyProperty.Register(
            "NewPictureFormVM", typeof(NewPictureFormVm), typeof(PictureView), new PropertyMetadata(default(NewPictureFormVm)));

        public NewPictureFormVm NewPictureFormVM
        {
            get { return (NewPictureFormVm)GetValue(NewPictureFormVmProperty); }
            set
            {
                SetValue(NewPictureFormVmProperty, value);
                value.PropertyChanged += PropertyChanged;

                DataContext = NewPictureFormVM;
                f();
            }
        }
        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            Dispatcher.Invoke(() => DataContext = NewPictureFormVM);
        }
        public ObservableCollection<MyPicture> MyPictures { get; }
            = new ObservableCollection<MyPicture>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Select picture(s)",
                Filter = "All supported graphics|" +
                        "*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|" +
                        "*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)" +
                        "|*.png",
                InitialDirectory = Environment.GetFolderPath(
                Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

            if (op.ShowDialog() == true)
            {
                paste(op.FileNames.Select(f => new MyPicture
                {
                    Url = new Uri(f, UriKind.Absolute),
                    Title = System.IO.Path.GetFileName(f)
                }));
            }
        }

            private void paste(IEnumerable<MyPicture> newPictures)
            {
                MyPictures.Clear();
                foreach (var item in newPictures)
                {
                    MyPictures.Add(item);
                }
            }

        private BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(filename));
        }


        private void f()
        {
            ObservableCollection<Report> reports = NewPictureFormVM.Events;
            MyItem[] items = new MyItem[reports.Count];

            Report r;
            for (int i = 0; i < reports.Count; i++)
            {
                r = reports[i];
                items[i] = new MyItem() { info = r.info, image = LoadImage(@"C:\Users\OriBibi\Desktop\imgs\" + r.Id + ".png") };
            }

 
            imageList.ItemsSource = items;
        }

    }

    public class MyItem
    {
        public string info { get; set; }

        public BitmapImage image { get; set;}//hbjkbknjkjn
    }

    public class MyPicture
    {
        public Uri Url { get; set; }
        public string Title { get; set; }
    }
}
