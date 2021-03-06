﻿using MvvmDialogs;
using log4net;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Linq;
using Missile_Falls_Project.Views;
using Missile_Falls_Project.Utils;

namespace Missile_Falls_Project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {



        private NewReportFormVM _newReportFormVm;
        public NewReportFormVM NewReportFormVm
        {
            get { return _newReportFormVm; }
            set
            {
                _newReportFormVm = value;
                NotifyPropertyChanged();
            }
        }
        private MapVM _mapVm;
        public MapVM MapVm
        {
            get { return _mapVm; }
            set
            {
                _mapVm = value;
                NotifyPropertyChanged();
            }
        }
        private GraphVM _GraphVm;
        public GraphVM GraphVm
        {
            get { return _GraphVm; }
            set
            {
                _GraphVm = value;
                NotifyPropertyChanged();
            }
        }
        private NewPictureFormVm _PictureFormVm;
        public NewPictureFormVm PictureFormVm
        {
            get { return _PictureFormVm; }
            set
            {
                _PictureFormVm = value;
                NotifyPropertyChanged();
            }
        }
        #region Parameters
        private readonly IDialogService DialogService;

        /// <summary>
        /// Title of the application, as displayed in the top bar of the window
        /// </summary>
        public string Title
        {
            get { return "Missile_Falls_Project"; }
        }
        #endregion

        public MainViewModel()
        {
            // DialogService is used to handle dialogs
            this.DialogService = new MvvmDialogs.DialogService();
            PictureFormVm = new NewPictureFormVm();
            MapVm = new MapVM();
            GraphVm = new GraphVM();
            NewReportFormVm = new NewReportFormVM();

        }



        public RelayCommand<object> SampleCmdWithArgument { get { return new RelayCommand<object>(OnSampleCmdWithArgument); } }

        public ICommand SaveAsCmd { get { return new RelayCommand(OnSaveAsTest, AlwaysFalse); } }
        public ICommand SaveCmd { get { return new RelayCommand(OnSaveTest, AlwaysFalse); } }
        public ICommand NewCmd { get { return new RelayCommand(OnNewTest, AlwaysFalse); } }
        public ICommand OpenCmd { get { return new RelayCommand(OnOpenTest, AlwaysFalse); } }
        public ICommand ShowAboutDialogCmd { get { return new RelayCommand(OnShowAboutDialog, AlwaysTrue); } }
        public ICommand ExitCmd { get { return new RelayCommand(OnExitApp, AlwaysTrue); } }

        private bool AlwaysTrue() { return true; }
        private bool AlwaysFalse() { return false; }

        private void OnSampleCmdWithArgument(object obj)
        {
            // TODO
        }

        private void OnSaveAsTest()
        {
            var settings = new SaveFileDialogSettings
            {
                Title = "Save As",
                Filter = "Sample (.xml)|*.xml",
                CheckFileExists = false,
                OverwritePrompt = true
            };

            bool? success = DialogService.ShowSaveFileDialog(this, settings);
            if (success == true)
            {
                // Do something
                Log.Info("Saving file: " + settings.FileName);
            }
        }
        private void OnSaveTest()
        {
            // TODO
        }
        private void OnNewTest()
        {
            // TODO
        }
        private void OnOpenTest()
        {
            var settings = new OpenFileDialogSettings
            {
                Title = "Open",
                Filter = "Sample (.xml)|*.xml",
                CheckFileExists = false
            };

            bool? success = DialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                // Do something
                Log.Info("Opening file: " + settings.FileName);
            }
        }
        private void OnShowAboutDialog()
        {
            Log.Info("Opening About dialog");
            AboutViewModel dialog = new AboutViewModel();
            var result = DialogService.ShowDialog<About>(this, dialog);
        }
        private void OnExitApp()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }
       
    }
}
