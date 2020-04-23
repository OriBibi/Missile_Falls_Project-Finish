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
    public class ChooseExplosionsModel : INotifyPropertyChanged
    {
        private readonly IBl _bl = new FactoryBl().GetInstance();

        private List<Event> _Events = new List<Event>();
        public List<Event> Events
        {
            get { return _Events; }
            set
            {
                _Events = value;
                OnPropertyChanged();
            }
        }

        public ChooseExplosionsModel()
        {
            GetEvents();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += checkNewEvents;
            worker.RunWorkerAsync();
        }

        private void checkNewEvents(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            while (true)
            {
                GetEvents();
                Thread.Sleep(5000);
            }
        }


        ICollection<Explosion> getExplosionsFromEvent(Event _event)
        {
            return _event.Explosions;
        }

        public void GetEvents()
        {
            if (_bl.GetEvents().Count > 0)
            {
                Events = _bl.GetEvents();
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
