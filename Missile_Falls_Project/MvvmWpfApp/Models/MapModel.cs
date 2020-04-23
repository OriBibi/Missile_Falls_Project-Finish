using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Missile_Falls_Project.Annotations;

namespace Missile_Falls_Project.Models
{
    public class MapModel : INotifyPropertyChanged
    {
        private readonly IBl _bl = new FactoryBl().GetInstance();

        private List<Event> _events = new List<Event>();
        public List<Event> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged();
            }
        }

        public MapModel()
        {
            GetEvents();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += CheckNewEvents;
            worker.RunWorkerAsync();
        }

        private void CheckNewEvents(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            while (true)
            {
                GetEvents();
                Thread.Sleep(5000);
            }
        }

        public async void GetEvents()
        {
            if (Events.Count == 0)
            {
                Events = _bl.GetEvents();
            }
            else
            {
                var allEvents = await _bl.GetEventsAsync();
                Events.AddRange(allEvents.Where(e => !Events.Exists(_e => _e.Id == e.Id)));
                OnPropertyChanged(nameof(Events));
            }
        }

        public async Task<IEnumerable<Report>> GetReports(int eventId)
        {
            return await _bl.GetReportsAsync(r => r.Event.Id == eventId);
        }

        public async Task<IEnumerable<Explosion>> GetExplosions(int eventId)
        {
            return await _bl.GetExplosions(e => e.Event.Id == eventId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
