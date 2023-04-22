using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shell;

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    internal class DetailsWriteOffIngredientViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public DetailsWriteOffIngredientViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        public static List<OutputWriteOff> GetIngredientList()
        {
            OutputWriteOff wrOffResult = WriteOffsUCViewModel.GetChoosenWriteOffItem();
            List<OutputWriteOff> dataList = new List<OutputWriteOff>();
            dataList.Add(wrOffResult);
            return dataList;
        }


        private RelayCommand _LoadEditWriteOffCommand;
        public RelayCommand LoadEditWriteOffCommand
        {
            get
            {
                return _LoadEditWriteOffCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditWriteOffIngredient);
                });
            }
        }

        /// Переход ко WriteOff вьюшке
        ///
        private RelayCommand _LoadWriteOffUCCommand;
        public RelayCommand LoadWriteOffUCCommand
        {
            get
            {
                return _LoadWriteOffUCCommand ?? new RelayCommand(obj =>
                {
                    //_Count = "";
                    //_NewSum = 0;
                    //_NewPrimeCost = "";
                    _MainCodeBehind.LoadView(ViewType.WriteOffs);
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
