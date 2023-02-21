using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model;
using StoreHouse.Model.Models;
using System.Runtime.CompilerServices;

namespace StoreHouse.ViewModels
{
    internal class RemainsUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public RemainsUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }


        void GetAllRemains()
        {

        }
        ////ComboBox TypeOfRemains
        //private List<string> typeOfRemainsComboBox = new List<string>() { "Інгредієнт", "Виробництво", "Страва" };

        //public List<string> TypeOfRemainsComboBox
        //{
        //    get => typeOfRemainsComboBox;
        //    set
        //    {
        //        typeOfRemainsComboBox = value;
        //        OnPropertyChanged();
        //    }
        //}
        //Commands
        private RelayCommand _ShowMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.ShowMessage("RemainsUC");
                });
            }
        }
        

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
