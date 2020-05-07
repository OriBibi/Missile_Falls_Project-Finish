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
            
        }
        public static readonly DependencyProperty PictureFormVmProperty = DependencyProperty.Register(
            "PictureFormVm", typeof(NewPictureFormVm), typeof(PictureView), new PropertyMetadata(default(NewPictureFormVm)));
        private readonly IBl _bl = new BlImp();
        private List<Event> Events = new List<Event>();

        public NewPictureFormVm PictureFormVm
        {
            get { return (NewPictureFormVm)GetValue(PictureFormVmProperty); }
            set { SetValue(PictureFormVmProperty, value);
                value.PropertyChanged += PropertyChanged;
                DataContext = PictureFormVm;
                Events = _bl.GetEvents();
                EventsCheckCB.DataContext = Events;
            }
        }
        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() => DataContext = PictureFormVm);
        }
        




        //*********************************************************************************8

       // public PictureView()
       // {
        //    InitializeComponent();
        //    DataContext = this;

      //  }
        private List<Event> events = new List<Event>();
        public List<string> getInformationsFromUsers = new List<string>();

        public string AddPicture(Report report)
        {
            var reportShow = " ";
            return reportShow;
        }
        

        //************************************************************************************8


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

        
    }

    public class MyPicture
        {
            public Uri Url { get; set; } // filename of image
            public string Title { get; set; }
        }
    }

