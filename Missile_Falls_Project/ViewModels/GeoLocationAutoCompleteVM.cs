using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Missile_Falls_Project.Annotations;
using Missile_Falls_Project.Models;
using QuickType;

namespace Missile_Falls_Project.ViewModels
{
   public class GeoLocationAutoCompleteVM : INotifyPropertyChanged
    {
        private List<Result> _locationList;

        public List<Result> LocatioList
        {
            get { return _locationList; }
            set
            {
                _locationList = value;
                OnPropertyChanged();
            }
        }

        private Result _selectedResult;

        public Result SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                OnPropertyChanged();
            }
        }

        public async Task AutoComp(AutoCompleteBox autoComplete)
        {
            var addres = autoComplete.Text;
            var autocomplete = new GeoLocationAutoCompleteModel();
            var resolts = await autocomplete.SearchLocation(addres);
            if (addres != autoComplete.Text) return;
            LocatioList = resolts;
            autoComplete.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
