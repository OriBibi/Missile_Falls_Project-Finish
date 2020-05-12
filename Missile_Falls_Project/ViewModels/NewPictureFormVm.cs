using Missile_Falls_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using BE;
using Microsoft.Maps.MapControl.WPF;
using Missile_Falls_Project.Annotations;
using Missile_Falls_Project.Utils;

namespace Missile_Falls_Project.ViewModels
{
    public class NewPictureFormVm : INotifyPropertyChanged
    {
        public NewPictureFormModel NewPictureFormModel { get; set; }
        public ObservableCollection<Event> Events
        {
            get { return new ObservableCollection<Event>(NewPictureFormModel.Events); }
            set { NewPictureFormModel.Events = new List<Event>(value); }
        }
        public NewPictureFormVm()
        {
            NewPictureFormModel = new NewPictureFormModel();
            NewPictureFormModel.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Events));
            };
            

            Events.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Events));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
