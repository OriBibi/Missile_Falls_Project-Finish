using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BE
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Hit> Hits { get; set; } = new List<Hit>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();

        public Event(DateTime startTime)
        {
            StartTime = new DateTime(startTime.Ticks);
            EndTime = StartTime.AddMinutes(10);
        }

        public Event() { }


        public override string ToString()
        {
            return Id + ": " + StartTime;
        }
        public Object Clone()
        {
            return new Event(StartTime)
            {
                Id = Id,
                EndTime = new DateTime(EndTime.Ticks),
                Hits = (from Hit in Hits select Hit?.Clone() as Hit).ToList(),
                Reports = (from report in Reports select report?.Clone() as Report).ToList(),
            };
        }
    }
}

