using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{

    public class DalImp : IDal
    {

        #region Event methods
        public void AddEvent(Event _event)
        {
            if (_event.Id != 0 && GetEvent(_event.Id) != null)
            {
                throw new Exception("The event already exist");
            }
            
            using (var ctx = new ProjectContext())
            {
                ctx.Events.Add(_event);
                ctx.SaveChangesAsync();
            }
        }

        public void RemoveEvent(int id)
        {
            var eventToRemove = GetEvent(id);
            if (eventToRemove == null) return;
            
            using (var ctx = new ProjectContext())
            {
                ctx.Events.Remove(eventToRemove);
                ctx.SaveChangesAsync();
            }
        }

        public void UpdateEvent(Event _event)
        {
            if (GetEvent(_event.Id) == null)
                throw new Exception("the event to update not found");
            
            using (var ctx = new ProjectContext())
            {
                //ctx.Entry(_event);
                ctx.Events.AddOrUpdate(_event);
                ctx.SaveChangesAsync();
            }
        }

        public List<Event> GetEvents(Expression<Func<Event, bool>> predicate = null)
        {
            List<Event> events;
            using (var ctx = new ProjectContext())
            {
                if (predicate != null)
                    events = ctx.Events.Where(predicate).ToList();       //select(e => e)
                else
                    events = ctx.Events.ToList();
            }

            return events;
        }

        public async Task<List<Event>> GetEventsAsync(Expression<Func<Event, bool>> predicate = null)
        {
            List<Event> events;
            using (var ctx = new ProjectContext())
            {
                if (predicate != null)
                    events = await ctx.Events.Where(predicate).ToListAsync();
                else
                    events = await ctx.Events.ToListAsync();
            }
            return events;
        }

        public Event GetEvent(int id)
        {
            Event _event;
            using (var ctx = new ProjectContext())
            {
                _event = ctx.Events.SingleOrDefault(e => e.Id == id);
            }
            return _event;
        }

        #endregion

        #region Report methods
        public async Task<Report> AddReportAsync(Report report)
        {
            if (report.Id != 0 && GetReport(report.Id) != null)
                throw new Exception("The report already exist");

            try
            {
                Report resReport = new Report();
                using (var ctx = new ProjectContext())
                {
                    resReport = ctx.Reports.Add(report);
                    if (report.Event.Id != 0)
                    {
                        resReport.Event.Reports.Add(resReport);
                        ctx.Events.Attach(resReport.Event);
                    }
                    await ctx.SaveChangesAsync();
                }
                if (resReport.Id != 0)
                    return resReport;
                else
                    throw new Exception("ID not updated when the report saved");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void RemoveReport(int id)
        {
            var reportToRemove = GetReport(id);
            if (reportToRemove == null)
                throw new Exception("the report doesn't exist");

            try
            {
                using (var ctx = new ProjectContext())
                {
                    ctx.Reports.Remove(reportToRemove);
                    ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateReport(Report report)
        {
            if (GetReport(report.Id) == null)
                throw new Exception("the report to update not found");
            
            using (var ctx = new ProjectContext())
            {
                //ctx.Entry(report); 
                ctx.Reports.AddOrUpdate(report);
                ctx.SaveChangesAsync();
            }
        }

        public List<Report> GetReports(Expression<Func<Report, bool>> predicate = null)
        {
            List<Report> reports;
            using (var ctx = new ProjectContext())
            {
                if (predicate != null)
                    reports = ctx.Reports.Where(predicate).ToList();
                else
                    reports = ctx.Reports.ToList();
            }
            return reports;
        }

        public async Task<List<Report>> GetReportsAsync(Expression<Func<Report, bool>> predicate = null)
        {
            List<Report> reports;
            using (var ctx = new ProjectContext())
            {
                if (predicate != null)
                    reports = await ctx.Reports.Where(predicate).ToListAsync();
                else
                    reports = await ctx.Reports.ToListAsync();
            }
            return reports;
        }

        public Report GetReport(int id)
        {
            Report report;
            using (var ctx = new ProjectContext())
            {
                report = ctx.Reports.SingleOrDefault(r => r.Id == id);
            }
            return report;
        }

        #endregion

        #region Hit methods

        public async Task<Hit> AddHitAsync(Hit hit)
        {
            if (hit.Id != 0 && GetHit(hit.Id) != null)
                throw new Exception("The hit already exist");

            try
            {
                using (var ctx = new ProjectContext())
                {
                    if (hit.Event.Id != 0)
                    {
                        hit.Event.Hits = null;
                        hit.Event.Reports = null;
                        ctx.Hits.Add(hit);
                        ctx.Events.Attach(hit.Event);
                    }
                    else
                    {
                        throw new Exception("Hit has no event");
                    }
                    await ctx.SaveChangesAsync();
                }
                if (hit.Id != 0) 
                    return hit;
                throw new Exception("id not updated when the Hit saved");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + $"in Dalimp AddHit with the Hit: {hit}");
            }
        }

        public void RemoveHit(int HitId)
        {
            using (var ctx = new ProjectContext())
            {
                var HitToRemove = ctx.Hits.Where(h => h.Id == HitId).ToList();
                if (HitToRemove.Count == 0) return;
                
                ctx.Hits.Remove(HitToRemove.First());
                ctx.SaveChangesAsync();
            }
        }

        public void UpdateHit(Hit hit)
        {
            var hitToUpdate = GetHits(h => h.Id == hit.Id);
            if (hitToUpdate == null || hitToUpdate.Count == 0)
                throw new Exception("the Hit to update not found");
           
            using (var ctx = new ProjectContext())
            {
                //ctx.Entry(hit);
                ctx.Hits.AddOrUpdate(hit);
                ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Hit>> GetHitsAsync(Expression<Func<Hit, bool>> predicate = null)
        {
            List<Hit> Hits;
            using (var ctx = new ProjectContext())
            {
                if (predicate != null)
                    Hits = await ctx.Hits.Where(predicate).ToListAsync();
                else
                    Hits = await ctx.Hits.ToListAsync();
            }
            return Hits;
        }

        public List<Hit> GetHits(Expression<Func<Hit, bool>> predicate = null)
        {
            List<Hit> Hits;
            using (var ctx = new ProjectContext())
            {
                if (predicate != null)
                    Hits = ctx.Hits.Where(predicate).ToList();
                else
                    Hits = ctx.Hits.ToList();
            }
            return Hits;
        }

        public Hit GetHit(int id)
        {
            Hit hit;
            using (var ctx = new ProjectContext())
            {
                hit = ctx.Hits.SingleOrDefault(h => h.Id == id);
            }
            return hit;
        }

        public Hit GetHitByEventId(int eventId)
        {
            Hit hit;
            using (var ctx = new ProjectContext())
            {
                hit = ctx.Hits.SingleOrDefault(h => h.Event.Id == eventId);
            }
            return hit;
        }

        #endregion
    }
}