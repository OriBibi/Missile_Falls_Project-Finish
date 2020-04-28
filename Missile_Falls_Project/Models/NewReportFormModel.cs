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

            //var res = _bl.addreport(report);
            //var message = res != null ?
            //  $"the report: {res.id}\nfrom: {res.reportername}\non: {res.time} saved successfully!" :
            //  "something went wrong when trying to add report!";
            //messagebox.show(message);
        }

    }
}
