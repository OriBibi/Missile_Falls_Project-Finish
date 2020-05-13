using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Report
    {
        public int Id { get; set; }
        public string ReporterName { get; set; }
        public string EventLocation { get; set; }
        public string Adress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }
        public int NoiseIntensity { get; set; }
        public int NumOfExplosions { get; set; }
        public Event Event { get; set; }
        public int ClusterId { get; set; }

        public Report()
        {
            Time = DateTime.Now;
        }
        public GeoCoordinate GetCoordinate()
        {
            return new GeoCoordinate(Latitude, Longitude);
        }
        public override string ToString()
        {
            return "Report number "+ Id + ":    Name: " + ReporterName+ "    In location: "+ Adress + "    Saved Successfully!";
        }
        public object Clone()
        {
            return new Report()
            {
                Id = Id,
                ReporterName = ReporterName,
                EventLocation = EventLocation,
                Adress = Adress,
                Latitude = Latitude,
                Longitude = Longitude,
                Time = new DateTime(Time.Ticks),
                NoiseIntensity = NoiseIntensity,
                NumOfExplosions = NumOfExplosions,
                Event = Event?.Clone() as Event,
                ClusterId = ClusterId,
            };
        }
    }
    

}