using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using Missile_Falls_Project.Models;
using System.Collections.ObjectModel;
using Missile_Falls_Project.Utils;
using BE;
using System.ComponentModel;
using Missile_Falls_Project.Annotations;
using System.Runtime.CompilerServices;
using System.Device.Location;

namespace Missile_Falls_Project.ViewModels
{
    public class GraphVM : INotifyPropertyChanged
    {
        public GraphModel GraphModel { get; set; }
        public ObservableCollection<string> EventsId { get; set; }
        public ObservableCollection<Report> Reports { get; set; }
        public ObservableCollection<Explosion> Explosions { get; set; }

        public RelayCommand<string> SelectedEventsComand { get; set; }
        public string Title { get; private set; }

        public IList<DataPoint> Points
        {
            //get
            //{
            //    return (from explosion in GraphModel.Explosions
            //            select new DataPoint(GraphModel.Explosions.IndexOf(explosion),
            //                new GeoCoordinate(explosion.ApproxLatitude, explosion.ApproxLongitude)
            //                .GetDistanceTo(
            //                    new GeoCoordinate(explosion.RealLatitude, explosion.RealLongitude)
            //                    ))).ToList();
            //}
            get;
            set;
        }
        public GraphVM()
        {
            GraphModel = new GraphModel();
            GraphModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Events")
                {
                    App.Current.Dispatcher.Invoke(SetEventsIds);
                }
            };
            Reports = new ObservableCollection<Report>();
            EventsId = new ObservableCollection<string>();
            EventsId.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("EventsId");
            };

            SetEventsIds();
            Explosions = new ObservableCollection<Explosion>(GraphModel.Explosions);
            var points = new List<DataPoint>();
            foreach (var explosion in Explosions)
            {
                if (explosion.RealLatitude == 0)
                {
                    points.Add(new DataPoint(Explosions.IndexOf(explosion), 0));
                }
                else
                {
                    var d1 = new GeoCoordinate(explosion.ApproxLatitude, explosion.ApproxLongitude);
                    var d2 = new GeoCoordinate(explosion.RealLatitude, explosion.RealLongitude);
                    points.Add(new DataPoint(Explosions.IndexOf(explosion), d1.GetDistanceTo(d2)));
                }
            }
            Points = points;
            ///////////////////////////////////////////////
            //TODO: Change to real DataBinding:

            this.Title = "Analitics Graph";
            List<DataPoint> list = new List<DataPoint>();

            //Mock:
            //this.Points =
            //    new List<DataPoint>
            //                  {
            //                      new DataPoint(0, 4),
            //                      new DataPoint(10, 13),
            //                      new DataPoint(20, 15),
            //                      new DataPoint(30, 16),
            //                      new DataPoint(40, 12),
            //                      new DataPoint(50, 12)
            //                  };

            ///////////////////////////////////////////////
        }

        private void SetEventsIds()
        {
            EventsId.Clear();
            foreach (var _event in GraphModel.Events)
            {
                EventsId.Add(_event.Id + ": " + _event.StartTime);
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
