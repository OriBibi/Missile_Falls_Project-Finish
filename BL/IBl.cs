using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBl
    {
        #region Event methods
        void AddEvent(Event _event);
        void RemoveEvent(int id);
        void UpdateEvent(Event _event);
        List<Event> GetEvents(Expression<Func<Event, bool>> predicate = null);
        Task<List<Event>> GetEventsAsync(Expression<Func<Event, bool>> predicate = null);
        Event GetEvent(int id);
        #endregion

        #region Report methods
        Task<Report> AddReportAsync(Report report);
        void RemoveReport(int id);
        void UpdateReport(Report report);
        List<Report> GetReports(Expression<Func<Report, bool>> predicate = null);
        Task<List<Report>> GetReportsAsync(Expression<Func<Report, bool>> predicate = null);
        Report GetReport(int id);
        #endregion

        #region Explosion methods
        Task<Hit> AddHit(Hit explosion);
        void RemoveHit(int id);
        void UpdateHit(Hit explosion);
        Task<List<Hit>> GetHits(Expression<Func<Hit, bool>> predicate = null);
        List<Hit> GetHits();
        Hit GetHit(int id);
        #endregion
    }
}