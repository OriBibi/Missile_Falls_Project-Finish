﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        public int NumOfHits { get; set; }
        public Event Event { get; set; }
        public int ClusterId { get; set; }
         
        public string info { get { return ToStringInfo(); } set {} }
        public byte[] image { get; set; }

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
            return "Report Id: "+Id+ ",  Reporter: " + ReporterName+ ",  location: "+ Adress+",  Time report: "+ Time;
        }
        public  string ToStringInfo()
        {
            return "Reporter: " + ReporterName + ", location: " + Adress;
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
                NumOfHits = NumOfHits,
                Event = Event?.Clone() as Event,
                ClusterId = ClusterId,
                image = image?.Clone() as byte[]
            };
        }
    }
    

}