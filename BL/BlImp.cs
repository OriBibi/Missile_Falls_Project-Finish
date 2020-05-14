using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Device.Location;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BL
{
    public class BlImp :IBl
    {
        private IDal _dal = new DalImp();

        #region Events

        public void AddEvent(Event _event)
        {
            _dal.AddEvent(_event);
        }

        public void RemoveEvent(int id)
        {
            _dal.RemoveEvent(id);
        }

        public void UpdateEvent(Event _event)
        {
            _dal.UpdateEvent(_event);
        }

        public List<Event> GetEvents(Expression<Func<Event, bool>> predicate = null)
        {
             return _dal.GetEvents(predicate); 
        }

        public Task<List<Event>> GetEventsAsync(Expression<Func<Event, bool>> predicate = null)
        {
            return _dal.GetEventsAsync(predicate);
        }

        public Event GetEvent(int id)
        {
            return _dal.GetEvent(id);
        }
        private Event modifyEvent(Event e,Report report)
        {
            if (report.Time < e.StartTime)

               e.StartTime = report.Time;
               

            else if (report.Time > e.EndTime)

                e.EndTime = report.Time;
            
            return e;
        }
        #endregion

        #region Reports

        public async Task<Report> AddReportAsync(Report report)
        {
            List<Event> events = _dal.GetEvents(e => e.StartTime <= EntityFunctions.AddMinutes(report.Time,5).Value &&
                                  e.EndTime >= EntityFunctions.AddMinutes(report.Time, -5).Value).ToList();
               
                /*(from e in GetEvents()
                                  where e.StartTime <= report.Time.AddMinutes(10) &&
                                  e.EndTime >= report.Time.AddMinutes(-10)
                 select e).ToList();*/
            
            if (events.Count == 1)
            {
                Event e = modifyEvent(events[0], report);
                
                    UpdateEvent(e);
                    report.Event = e;
            }
            else if (events.Count > 1)
            {
                Event closestEvent = events[0]; // initialize pointer for compare later
                double tmpInterval, minInterval = Math.Abs((report.Time - closestEvent.StartTime).TotalMinutes);

                foreach (Event ev in events) // check closest start time
                {
                    tmpInterval = Math.Abs((report.Time - ev.StartTime).TotalMinutes);
                    if (tmpInterval < minInterval)
                    {
                        minInterval = tmpInterval;
                        closestEvent = ev;
                    }
                }

                foreach (Event ev in events) // check closest end time
                {
                    tmpInterval = Math.Abs((report.Time - ev.EndTime).TotalMinutes);
                    if (tmpInterval < minInterval)
                    {
                        minInterval = tmpInterval;
                        closestEvent = ev;
                    }
                }
                Event e = modifyEvent(closestEvent, report); //after check with all the end and the start events we found the closest event.
                UpdateEvent(e);
                report.Event = e;
            }
            else// event.count == 0
            {
                report.Event = new Event(report.Time) { StartTime = report.Time };
            }

            var res = await _dal.AddReportAsync(report);
            UpdateHits(report);
            return res;
        }

        
        public void RemoveReport(int id)
        {
            _dal.RemoveReport(id);
        }

        public void UpdateReport(Report report)
        {
            _dal.UpdateReport(report);
        }

        public List<Report> GetReports(Expression<Func<Report, bool>> predicate = null)
        {
            return _dal.GetReports(predicate);
        }

        public Task<List<Report>> GetReportsAsync(Expression<Func<Report, bool>> predicate = null)
        {
            return _dal.GetReportsAsync(predicate);
        }

        public Report GetReport(int id)
        {
            return _dal.GetReport(id);
        }
        private async void UpdateHits(Report newReport)
        {
            Event _event = newReport.Event;
            var hits = _event.Hits;
           // var hits = await _dal.GetHits(h => h.Event.Id == _event.Id);

            //chsck if have real location
           /* if (hits.Count > 0 && hits.All(h => h.RealLatitude > 0))
            {
                _event.Reports.Add(newReport);
                return;
            }*/
            //remove the last approximate hits
            foreach (var explotion in hits.ToArray())
            {
                _dal.RemoveHit(explotion.Id);
            }
            //_event.Reports = await _dal.GetReportsAsync((report => report.Event.Id == _event.Id));
            _event.Reports.Add(newReport);
            int averageHits = (int)_event.Reports.Average(r => r.NumOfHits);
            KMeans kMeans = new KMeans(_event.Reports, averageHits);
            List<GeoCoordinate> clusters = kMeans.K_Means();
            foreach (GeoCoordinate g in clusters)
            {
                Hit h = new Hit
                {
                    ApproxLatitude = g.Latitude,
                    ApproxLongitude = g.Longitude,
                    Event = _event
                };
                await _dal.AddHitAsync(h);
            }

        }
        #endregion

        #region Hits

        public Task<Hit> AddHit(Hit Hit)
        {
            return _dal.AddHitAsync(Hit);
        }

        public void RemoveHit(int id)
        {
            _dal.RemoveHit(id);
        }

        public void UpdateHit(Hit hit)
        {
            _dal.UpdateHit(hit);
        }

        public Task<List<Hit>> GetHits(Expression<Func<Hit, bool>> predicate = null)
        {
            return _dal.GetHitsAsync(predicate);
        }

        public List<Hit> GetHits()
        {
            return _dal.GetHits();
        }

        public Hit GetHit(int id)
        {
            return _dal.GetHit(id);
        }

        #endregion

    }
}