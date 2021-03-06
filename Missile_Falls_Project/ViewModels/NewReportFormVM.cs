﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BE;
using BL;
using Missile_Falls_Project.Annotations;
using Missile_Falls_Project.Models;
using Missile_Falls_Project.Utils;
using QuickType;

namespace Missile_Falls_Project.ViewModels
{
    public class NewReportFormVM : INotifyPropertyChanged
    {
       public NewReportFormVM()
        {
            FormModel = new NewReportFormModel();
            reportModel = FormModel.report.Clone() as Report;
            AddReportCommand = new RelayCommand<NewReportFormModel>(formModel =>
            {
                if (reportModel.ReporterName == "" || reportModel.Adress == null)
                    return;
                
                formModel.report = reportModel.Clone() as Report;
                Report = new Report();
                formModel.AddReportAsync();
            },
                //if have more condition to add report 
                report =>
                {
                    return report != null;
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NewReportFormModel FormModel { get; set; }

        private RelayCommand<NewReportFormModel> _addReportCommand;
        public RelayCommand<NewReportFormModel> AddReportCommand
        {
            get { return _addReportCommand; }
            set
            {
                _addReportCommand = value;
                OnPropertyChanged();
            }
        }


        private Report reportModel;
        public Report Report
        {
            get { return reportModel; }
            set
            {
                OnPropertyChanged();
                reportModel = value;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
