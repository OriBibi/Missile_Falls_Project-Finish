using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BE;
using BL;
using Missile_Falls_Project.Utils;

namespace Missile_Falls_Project.Models
{
    public class NewReportFormModel
    {
        private readonly IBl _bl = new BlImp();

        public Report Report { get; set; } = new Report();

        public async void AddReport()
        {
            _bl.AddReports(Report)
            var res =_bl.GetReport(Report);
            var message = res != null ?
                $"The Report: {res.Id}\nFrom: {res.Name}\nOn: {res.Time} Saved Successfully!" :
                "Something went wrong when trying to add report!";
            MessageBox.Show(message);
        }

    }
}
