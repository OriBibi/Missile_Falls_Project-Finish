using BE;
using Missile_Falls_Project.Annotations;
using Missile_Falls_Project.Models;
using Missile_Falls_Project.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Missile_Falls_Project.ViewModels
{
    public class ChooseExplosionsVM : INotifyPropertyChanged
    {
        public ChooseExplosionsModel chooseExplosionsModel { get; set; }
        public ObservableCollection<Explosion> ExplosionsFromEvent { get; set; }
        public Event Event { get; set; }
        public Explosion _Explosion { get; set; }

        public string approxLon
        {
            get { return _Explosion!=null? Convert.ToString(_Explosion.ApproxLongitude): "0"; }
        }
        public string approxLat
        {
            get { return _Explosion != null ? Convert.ToString(_Explosion.ApproxLatitude) : "0"; }
        }

        public string realLon
        {
            get { return _Explosion != null ? Convert.ToString(_Explosion.RealLongitude) : "0"; }
            set { }
        }
        public string realLat
        {
            get { return _Explosion != null ? Convert.ToString(_Explosion.RealLatitude) : "0"; }
            set { }
        }

        public List<string> Events
        {
            get
            {
                var eventsStartTime = new List<string>();
                if (chooseExplosionsModel.Events.Count > 0)
                {
                    foreach (var _event in chooseExplosionsModel.Events)
                    {
                        eventsStartTime.Add(_event.StartTime.ToString());
                    }
                }
                return eventsStartTime;
            }
        }

        public List<Explosion> Explosions
        {
            get
            {
                if (Event != null && Event.Explosions.Count > 0)
                {
                    return (List<Explosion>)Event.Explosions;
                }
                return new List<Explosion>();
            }
        }

        public RelayCommand<string> SelectedEventsComand { get; set; }
        public string Title { get; private set; }

        public ChooseExplosionsVM()
        {
            chooseExplosionsModel = new ChooseExplosionsModel();
            chooseExplosionsModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Events")
                {
                    App.Current.Dispatcher.Invoke(updateExplosionsFromEvent);
                    App.Current.Dispatcher.Invoke(() => OnPropertyChanged("Events"));
                }
            };
            ExplosionsFromEvent = new ObservableCollection<Explosion>();
            ExplosionsFromEvent.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("ExplosionsFromEvent");
            };

            updateExplosionsFromEvent();
        }

        public Event getEventByStartTime(string start_time)
        {
            Event _event = new Event();
            _event = chooseExplosionsModel.Events.Find(s => s.StartTime.ToString().Equals(start_time));
            return _event;
        }

        private void updateExplosionsFromEvent()
        {
            if (ExplosionsFromEvent.Count > 0)
            {
                ExplosionsFromEvent = new ObservableCollection<Explosion>(Explosions);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
