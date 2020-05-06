using System;
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
    public class NewPictureFormVM : INotifyPropertyChanged
    {
        public NewPictureFormVM()
        {
            NewPictureFormModel formModel = new NewPictureFormModel();
            formModel.AddPicture();
            // PictureModel = FormModel.Report.Clone() as Report;
            // AddPictureCommand = new RelayCommand<NewPictureFormModel>(formModel =>
            // {
            //     if (PictureModel.PictureerName == "" ||
            //     PictureModel.Adress == null

            //)
            //         return;
            //     formModel.Picture = PictureModel.Clone() as Picture;
            //     Picture = new Picture();
            //     formModel.AddPictureAsync();
            // },
            //     //if have more condition to add Picture 
            //     Picture =>
            //     {
            //         return Picture != null;
            //     });
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
