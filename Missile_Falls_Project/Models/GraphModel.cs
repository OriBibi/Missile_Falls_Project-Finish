using BE;
using BL;
using Missile_Falls_Project.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Missile_Falls_Project.Models
{
    public class GraphModel : INotifyPropertyChanged
    {
        private readonly IBl _bl = new BlImp();

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
        private List<Hit> _Hits = new List<Hit>();
        public List<Hit> Hits
        {
            get { return _Hits; }
            set
            {
                _Hits = value;
                OnPropertyChanged();
            }
        }
        public GraphModel()
        {
            GetEvents();
            GetHits();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += checkNewEvents;
            worker.RunWorkerAsync();
        }

        private void checkNewEvents(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            while (true)
            {
                GetEvents();
                GetHits();
                Thread.Sleep(5000);
            }
        }

        private void GetHits()
        {
            Hits = _bl.GetHits();
        }

        public void GetEvents()
        {
            Events = _bl.GetEvents();
        }

        public async Task<IEnumerable<Report>> GetReports(int eventId)
        {
            return await _bl.GetReportsAsync(r => r.Event.Id == eventId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
