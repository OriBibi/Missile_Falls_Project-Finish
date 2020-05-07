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
        public ObservableCollection<Event> SelectedEvents { get; set; }

        public ObservableCollection<Pushpin> LocationList
        {
            get
            {
                var a = new ObservableCollection<Pushpin>(
                    Reports.Select(r => new Pushpin()
                    {
                        Location = new Location(r.Latitude, r.Longitude)
                    }));
                var b = new ObservableCollection<Pushpin>(Hits.Select(e => new Pushpin()
                {
                    Location = new Location(e.ApproxLatitude, e.ApproxLongitude),
                    Background = Brushes.Red
                }));
                var c = a.ToList();
                c.AddRange(b);
                return new ObservableCollection<Pushpin>(c);
            }
        }

        public ObservableCollection<Report> Reports { get; set; }

        public ObservableCollection<Hit> Hits { get; set; }

        public RelayCommand<Event> SelectedEventsComand { get; set; }

        public NewPictureFormVm()
        {
            NewPictureFormModel = new NewPictureFormModel();
            NewPictureFormModel.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Events));
            };
            SelectedEvents = new ObservableCollection<Event>();
            Reports = new ObservableCollection<Report>();
            Hits = new ObservableCollection<Hit>();

            Events.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Events));
            };

            LocationList.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(LocationList));
            };

            SelectedEventsComand = new RelayCommand<Event>(SelectedChanged);

        }


        private void SelectedChanged(object obj)
        {
            if (obj is Event)
            {
                Event @event = obj as Event;
                SelectedChangedAsync(@event);
            }
        }

        private async Task SelectedChangedAsync(Event _event)
        {
            if (SelectedEvents.All(e => e != _event))
            {
                foreach (var report in Reports.ToArray())
                {
                    if (report.Event.Id == _event.Id)
                        Reports.Remove(report);
                }
                foreach (var Hit in Hits.ToArray())
                {
                    if (Hit.Event.Id == _event.Id)
                    {
                        Hits.Remove(Hit);
                    }
                }
            }
            else
            {
                var reports = await NewPictureFormModel.GetReports(_event.Id);
                var hits = await NewPictureFormModel.GetHits(_event.Id);
                foreach (var report in reports)
                {
                    Reports.Add(report);
                }
                foreach (var hit in hits)
                {
                    Hits.Add(hit);
                }
            }
            OnPropertyChanged(nameof(LocationList));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
