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
        public List<string> getInformationsFromUsers = new List<string>();

        public Report report { get; set; } =  new Report();

        public async void AddReportAsync()
        {

            var res = await _bl.AddReportAsync(report);
            var message = res != null ?
              $"The Report: {res.Id}\nFrom: {res.ReporterName}\nOn: {res.Time} Saved Successfully!" :
              "Something went wrong when trying to add report!";
            getInformationsFromUsers.Add(message);
            MessageBox.Show(message);
        }

    }
}
