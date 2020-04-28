using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {
        #region Event methods
        void AddEvent(Event _event);
        void RemoveEvent(int id);
        //void RemoveEventAsync(int id);
        void UpdateEvent(Event _event);
        List<Event> GetEvents(Expression<Func<Event,bool>> predicate = null);
        Task<List<Event>> GetEventsAsync(Expression<Func<Event, bool>> predicate = null);
        Event GetEvent(int id);
        #endregion

        #region Report methods
        Task<Report> AddReportAsync(Report report);
        //bool AddReport(Report report);
        void RemoveReport(int id);
        void UpdateReport(Report report);
        List<Report> GetReports(Expression<Func<Report, bool>> predicate = null);
        Task<List<Report>> GetReportsAsync(Expression<Func<Report, bool>> predicate = null);
        Report GetReport(int id);
        #endregion

        #region Hit methods
        //bool AddHit(Hit hit);
        Task<Hit> AddHitAsync(Hit hit);
        void RemoveHit(int hitId);
        void UpdateHit(Hit hit);
        Task<List<Hit>> GetHitsAsync(Expression<Func<Hit, bool>> predicate = null);
        List<Hit> GetHits(Expression<Func<Hit, bool>> predicate = null);
        Hit GetHit(int id);
        Hit GetHitByEventId(int eventId);
        //List<Hit> GetHitsAsync();
        #endregion
    }
}
