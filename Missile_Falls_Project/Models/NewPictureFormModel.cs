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
    public class NewPictureFormModel : INotifyPropertyChanged
    {
        private readonly IBl _bl = new BlImp();

        private List<Report> _events = new List<Report>();
        public List<Report> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged();
            }
        }

        public NewPictureFormModel()
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
                Events = _bl.GetReports();
            }
            else
            {
                var allEvents = await _bl.GetReportsAsync();
                Events.AddRange(allEvents.Where(e => !Events.Exists(_e => _e.Id == e.Id)));
                OnPropertyChanged(nameof(Events));
            }
        }

        public async Task<IEnumerable<Report>> GetReports(int eventId)
        {
            return await _bl.GetReportsAsync(r => r.Event.Id == eventId);
        }

        public async Task<IEnumerable<Hit>> GetHits(int eventId)
        {
            return await _bl.GetHits(e => e.Event.Id == eventId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
